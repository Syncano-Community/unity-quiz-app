using UnityEngine;
using System.Collections;
using System;

public class QuestionPanel : MonoBehaviour
{
    [SerializeField]
    private QuestionView questionView;

    [SerializeField]
    private AnswerView[] answers = new AnswerView[4];

    private Action<AnswerType> onAnswerSelected;

    void Start()
    {
        SetPlayMode();
        ClearViews();

        answers[(int)AnswerType.A].onClick.AddListener(()=> AnswerClicked(AnswerType.A));
        answers[(int)AnswerType.B].onClick.AddListener(()=> AnswerClicked(AnswerType.B));
        answers[(int)AnswerType.C].onClick.AddListener(()=> AnswerClicked(AnswerType.C));
        answers[(int)AnswerType.D].onClick.AddListener(()=> AnswerClicked(AnswerType.D));
    }

    public void SetPlayMode()
    {
        questionView.SetPlayMode();
        foreach (var item in answers)
        {
            item.SetPlayMode();
        }
    }

    public void SetEditMode()
    {
        questionView.SetEditMode();
        foreach (var item in answers)
        {
            item.SetEditMode();
        }
    } 

    public void ClearViews()
    {
        questionView.SetText(null);
        foreach (var item in answers)
        {
            item.SetText(null);
        }
    }

    public IEnumerator ShowQuestion(Question question)
    {
        ClearViews();
        questionView.SetText(question.text);
        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < answers.Length; i++)
        {
            answers[i].SetText(question.answers[i]);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public IEnumerator ShowAnswer(AnswerType correctAnswer, AnswerType selectedAnswer)
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.2f);
            // Highlight on
            yield return new WaitForSeconds(0.2f);
            // Highlight off
        }
    }

    private void AnswerClicked(AnswerType answer)
    {
        if (onAnswerSelected != null)
            onAnswerSelected.Invoke(answer);
    }

    public void SetOnAnswerSelectedListener(Action<AnswerType> action)
    {
        onAnswerSelected = action;
    }

    private AnswerView GetAnswerView(AnswerType answer)
    {
        return answers[(int)answer];
    }
}
