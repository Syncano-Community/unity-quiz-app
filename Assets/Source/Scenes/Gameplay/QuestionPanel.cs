using UnityEngine;
using System.Collections;
using System;

public class QuestionPanel : Singleton<QuestionPanel>
{
    [SerializeField]
    private QuestionView questionView;

    [SerializeField]
    private AnswerView[] answers = new AnswerView[4];

    private Action<int> onAnswerSelected;

    void Start()
    {
        SetPlayMode();
        ClearViews();
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
        questionView.SetText(question.Text);
        yield break;
    }

    public void SetOnAnswerSelectedListener(Action<int> action)
    {
        onAnswerSelected = action;
    }
}
