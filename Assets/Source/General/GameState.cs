using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{
    private Quiz quiz;

    #region Current question
    private int questionIndex;
    private int lastCorrectAnswerIndex;
    private Question currentQuestion; 
    #endregion Current question

    #region Views
    private GameUI gameUI;
    #endregion Views

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

    public void StartGame()
    {
        StartCoroutine(StartRoutine());
    }

    public void FinishGame(bool giveUp)
    {
        ScoreRow score = ScoreTable.GetScoreForIndex(lastCorrectAnswerIndex, giveUp);
        gameUI.ResultPanel.ShowReward(score);
    }

    private IEnumerator StartRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(gameUI.ScorePanel.ShowOff());
        yield return StartCoroutine(ShowQuestionRoutine());
    }

    private void SwitchToNextQuestion()
    {
        questionIndex += 1;
        StartCoroutine(ShowQuestionRoutine());
    }

    private IEnumerator ShowQuestionRoutine()
    {
        gameUI.ScorePanel.SetLevel(questionIndex);
        Question newQuestion = quiz.GetQuestion(questionIndex);
        newQuestion.Shuffle();
        yield return StartCoroutine(gameUI.QuestionPanel.ShowQuestion(newQuestion));
        currentQuestion = newQuestion;
    }

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

    private void OnLifelineSelected(LifelineType lifeline)
    {
        if (currentQuestion == null)
            return;

        gameUI.ScorePanel.SetLifelineInteractable(lifeline, false);
        gameUI.LifelinesPanel.ShowLifeline(lifeline, currentQuestion);
    }

    private bool IsLastQuestion()
    {
        return questionIndex == quiz.GetQuestionCount() - 1;
    }
}
