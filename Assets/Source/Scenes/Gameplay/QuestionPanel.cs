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
    private Question currentQuestion;

    void Awake()
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
        answers[(int)AnswerType.A].HighlightCorrect();
    } 

    public void ClearViews()
    {
        questionView.SetText(null);
        foreach (var item in answers)
        {
            item.SetText(null);
            answers[(int)AnswerType.A].HiglightOff();
        }
    }

    public IEnumerator ShowQuestion(Question question)
    {
        currentQuestion = question;
        ClearViews();
        questionView.SetText(question.text);
        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < answers.Length; i++)
        {
            answers[i].SetText(question.answers[i]);
            yield return new WaitForSeconds(0.5f);
        }

        SetInteractable(true);
    }

    public IEnumerator ShowAnswer(AnswerType selected, AnswerType correct)
    {
        SetInteractable(false);

        bool incorrect = (selected != correct);
        AnswerView selectedView = GetAnswerView(selected);
        AnswerView correctView = GetAnswerView(correct);



        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.3f);

            // Highlight on
            correctView.HighlightCorrect();
            if (incorrect)
                selectedView.HiglightIncorrect();
            
            yield return new WaitForSeconds(0.3f);

            // Highlight off
            correctView.HiglightOff();
            if (incorrect)
                selectedView.HiglightOff();
        }
    }

    public void RefreshAvailableAnswers()
    {
        for (int i = 0; i < answers.Length; i++)
        {
            if (currentQuestion.AvailableAnswers.Contains((AnswerType) i))
            {
                answers[i].SetInteractable(true);
                answers[i].SetText(currentQuestion.answers[i]);
            }
            else
            {
                answers[i].SetInteractable(false);
                answers[i].SetText(null);
            }
        }
    }

    private void SetInteractable(bool interactable)
    {
        foreach (var item in answers)
        {
            item.SetInteractable(interactable);    
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

    public QuestionView GetQuestionView()
    {
        return questionView;
    }

    public AnswerView GetAnswerView(AnswerType answer)
    {
        return answers[(int)answer];
    }
}
