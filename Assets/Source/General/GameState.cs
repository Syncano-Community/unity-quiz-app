using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour
{
    private Quiz quiz;
    private int questionIndex;

    #region Views
    private QuestionPanel questionPanel;
    private ScorePanel scorePanel;
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
        this.fiftyFifty = true;
        this.phoneCall = true;
        this.audience = true;

        questionPanel = QuestionPanel.Instance;
        scorePanel = ScorePanel.Instance;

        questionPanel.SetOnAnswerSelectedListener(OnAnswerSelected);
        scorePanel.SetOnLifelineSelectedListener(OnLifelineSelected);
    }

    public void StartGame()
    {
        StartCoroutine(StartRoutine());
    }

    private IEnumerator StartRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(scorePanel.ShowOff());
        yield return StartCoroutine(questionPanel.ShowQuestion(quiz.GetQuestion(questionIndex)));
    }

    private void SwitchToNextQuestion()
    {
        questionIndex += 1;
    }

    public void OnAnswerSelected(int answerIndex)
    {

    }

    public void OnLifelineSelected(Lifeline lifeline)
    {
        scorePanel.SetLifelineInteractable(lifeline, false);
    }
}
