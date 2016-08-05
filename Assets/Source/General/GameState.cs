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

    #region Lifelines
    private bool fiftyFifty;
    private bool phoneCall;
    private bool audience;
    #endregion Lifelines

    public void Init(Quiz quiz)
    {
        this.quiz = quiz;
        this.questionIndex = 0;
        this.lastCorrectAnswerIndex = -1;
        this.currentQuestion = null;
        this.fiftyFifty = true;
        this.phoneCall = true;
        this.audience = true;

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

        currentQuestion = quiz.GetQuestion(questionIndex);
        currentQuestion.Shuffle();
        yield return StartCoroutine(gameUI.QuestionPanel.ShowQuestion(currentQuestion));
    }

    private void SwitchToNextQuestion()
    {
        questionIndex += 1;
        StartCoroutine(NextQuestionRoutine());
    }

    private IEnumerator NextQuestionRoutine()
    {
        currentQuestion = quiz.GetQuestion(questionIndex);
        currentQuestion.Shuffle();
        yield return StartCoroutine(gameUI.QuestionPanel.ShowQuestion(currentQuestion));
    }

    public void OnAnswerSelected(AnswerType answer)
    {
        bool correct = (currentQuestion.CorrectAnswer == answer);

        if (correct)
            lastCorrectAnswerIndex = questionIndex;

        if (IsLastQuestion() || correct == false)
        {
            FinishGame(false);
        }
        else
        {
            SwitchToNextQuestion();
        }
    }

    public void OnLifelineSelected(LifelineType lifeline)
    {
        gameUI.ScorePanel.SetLifelineInteractable(lifeline, false);
    }

    private bool IsLastQuestion()
    {
        return questionIndex == quiz.GetQuestionCount() - 1;
    }
}
