using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour
{
    private Quiz quiz;
    private int questionIndex;

    #region Lifelines
    private bool fiftyFifty;
    private bool phoneCall;
    private bool audience;
    #endregion Lifelines

    void Awake()
    {

    }

    public void Init(Quiz quiz)
    {
        this.quiz = quiz;
        this.questionIndex = 0;
        this.fiftyFifty = true;
        this.phoneCall = true;
        this.audience = true;
    }

    public void StartGame()
    {
        StartCoroutine(StartRoutine());
    }

    private IEnumerator StartRoutine()
    {

        yield return StartCoroutine(ShowQuestion());
    }

    private IEnumerator ShowQuestion()
    {
        return null;
    }

    private void SwitchToNextQuestion()
    {
        questionIndex += 1;
    }
}
