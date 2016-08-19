using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{
    /// <summary>
    /// Current Quiz.
    /// </summary>
    private Quiz quiz;

    #region Current question

    /// <summary>
    /// Current question index.
    /// </summary>
    private int questionIndex;

    /// <summary>
    /// Index of the last player correct answer.
    /// Used to find reward.
    /// </summary>
    private int lastCorrectAnswerIndex;

    /// <summary>
    /// The current question.
    /// </summary>
    private Question currentQuestion; 
    #endregion Current question

    #region Views
    /// <summary>
    /// Place where we can access game UI.
    /// </summary>
    private GameUI gameUI;
    #endregion Views

    /// <summary>
    /// Start this MonoBehaviour.
    /// </summary>
    void Start ()
    {
        if (Setup.GetQuiz().IsValid())
        {
            Init(Setup.GetQuiz());
            StartGame();
        }
        else
        {
            Debug.Log("Unable to start. Quiz is not valid.");
        }
    }

    /// <summary>
    /// Raises the exit click event.
    /// </summary>
    /* ui event */ public void OnExitClick()
    {
        FinishGame(true);
    }

    /// <summary>
    /// Init the GameState for given quiz.
    /// </summary>
    public void Init(Quiz quiz)
    {
        this.quiz = quiz;
        this.questionIndex = 0;
        this.lastCorrectAnswerIndex = -1;
        this.currentQuestion = null;

        gameUI = GameUI.Instance;
        gameUI.QuestionPanel.SetOnAnswerSelectedListener(OnAnswerSelected);
        gameUI.ScorePanel.SetOnLifelineSelectedListener(OnLifelineSelected);
    }

    /// <summary>
    /// Run game start routine.
    /// </summary>
    public void StartGame()
    {
        StartCoroutine(StartRoutine());
    }

    /// <summary>
    /// Finishs the game.
    /// </summary>
    public void FinishGame(bool giveUp)
    {
        ScoreRow score = ScoreTable.GetScoreForIndex(lastCorrectAnswerIndex, giveUp);
        gameUI.ResultPanel.ShowReward(score);
    }

    /// <summary>
    /// Routine for game start.
    /// </summary>
    private IEnumerator StartRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(gameUI.ScorePanel.ShowOff());
        yield return StartCoroutine(ShowQuestionRoutine());
    }

    /// <summary>
    /// Switchs to next question and start change routine.
    /// </summary>
    private void SwitchToNextQuestion()
    {
        questionIndex += 1;
        StartCoroutine(ShowQuestionRoutine());
    }

    /// <summary>
    /// Routine for showing question using questionIndex.
    /// </summary>
    private IEnumerator ShowQuestionRoutine()
    {
        gameUI.ScorePanel.SetLevel(questionIndex);
        Question newQuestion = quiz.GetQuestion(questionIndex);
        newQuestion.Shuffle(); // Change answer order.
        yield return StartCoroutine(gameUI.QuestionPanel.ShowQuestion(newQuestion));
        currentQuestion = newQuestion; // Set current question - it will allow interactions.
    }

    /// <summary>
    /// Callback for answer select.
    /// </summary>
    public void OnAnswerSelected(AnswerType answer)
    {
        if (currentQuestion == null)
            return;

        Question question = currentQuestion;
        currentQuestion = null; // Don't allow interactions untill new question is set.

        bool correct = (question.CorrectAnswer == answer);

        if (correct)
        {
            lastCorrectAnswerIndex = questionIndex;
            gameUI.FoxPanel.PlayCorrect();
        }
        else
        {
            gameUI.FoxPanel.PlayIncorrect();
        }

        gameUI.LifelinesPanel.HideLifelines();
        StartCoroutine(ShowAnswerRoutine(IsLastQuestion(), correct, answer, question.CorrectAnswer));
    }

    /// <summary>
    /// Routine for showing blinking answer and finish game or show next question.
    /// </summary>
    private IEnumerator ShowAnswerRoutine(bool isLast, bool isCorrect, AnswerType selected, AnswerType correct)
    {
        yield return StartCoroutine(gameUI.QuestionPanel.ShowAnswer(selected, correct));

        if (isLast || isCorrect == false)
        {
            FinishGame(false);
        }
        else
        {
            SwitchToNextQuestion();
        }
    }

    /// <summary>
    /// Callback for lifeline selected.
    /// </summary>
    private void OnLifelineSelected(LifelineType lifeline)
    {
        if (currentQuestion == null)
            return;

        gameUI.ScorePanel.SetLifelineInteractable(lifeline, false);
        gameUI.LifelinesPanel.ShowLifeline(lifeline, currentQuestion);
    }

    /// <summary>
    /// True if current question is last one.
    /// </summary>
    private bool IsLastQuestion()
    {
        return questionIndex == quiz.GetQuestionCount() - 1;
    }
}
