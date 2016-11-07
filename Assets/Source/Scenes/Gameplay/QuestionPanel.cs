using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Represents container for question and answers.
/// Can be set to Play and Edit modes.
/// </summary>
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

    /// <summary>
    /// Sets the play mode - text edit is dissabled.
    /// </summary>
    public void SetPlayMode()
    {
        questionView.SetPlayMode();
        foreach (var item in answers)
        {
            item.SetPlayMode();
            item.HiglightOff();
        }
    }

    /// <summary>
    /// Sets the edit mode - text edit is enabled.
    /// Answer A is highlighted - it's always correct answer in edit mode.
    /// </summary>
    public void SetEditMode()
    {
        questionView.SetEditMode();
        foreach (var item in answers)
        {
            item.SetEditMode();
            item.HiglightOff();
        }
        answers[(int)AnswerType.A].HighlightCorrect();
    } 

    /// <summary>
    /// Clear question and ansters text and disable highlights.
    /// </summary>
    public void ClearViews()
    {
        questionView.SetText(null);
        foreach (var item in answers)
        {
            item.SetText(null);
            item.HiglightOff();
        }
    }

    /// <summary>
    /// Fills the question panel with data.
    /// </summary>
    public void FillQuestion(Question question)
    {
        currentQuestion = question;
        ClearViews();
        questionView.SetText(question.Text);

        for (int i = 0; i < answers.Length; i++)
        {
            answers[i].SetText(question.Answers[i]);
        }
    }

    /// <summary>
    /// Animated fill question form.
    /// </summary>
    public IEnumerator ShowQuestion(Question question)
    {
        currentQuestion = question;
        ClearViews();
        questionView.SetText(question.Text);
        yield return new WaitForSeconds(0.8f);

        for (int i = 0; i < answers.Length; i++)
        {
            answers[i].SetText(question.Answers[i]);
            yield return new WaitForSeconds(0.4f);
        }

        SetInteractable(true);
    }

    /// <summary>
    /// Animated show correct/incorrect answer.
    /// </summary>
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

    /// <summary>
    /// Refreshs the available answers.
    /// Should be called after lifeline is used.
    /// </summary>
    public void RefreshAvailableAnswers()
    {
        for (int i = 0; i < answers.Length; i++)
        {
            if (currentQuestion.AvailableAnswers.Contains((AnswerType) i))
            {
                answers[i].SetInteractable(true);
                answers[i].SetText(currentQuestion.Answers[i]);
            }
            else
            {
                answers[i].SetInteractable(false);
                answers[i].SetText(null);
            }
        }
    }

    /// <summary>
    /// Make answers interactable.
    /// </summary>
    private void SetInteractable(bool interactable)
    {
        foreach (var item in answers)
        {
            item.SetInteractable(interactable);    
        }
    }

    /// <summary>
    /// Answers the clicked event.
    /// </summary>
    private void AnswerClicked(AnswerType answer)
    {
        if (onAnswerSelected != null)
            onAnswerSelected.Invoke(answer);
    }

    /// <summary>
    /// Sets the on answer selected listener.
    /// Event will be rised after user select answer.
    /// </summary>
    public void SetOnAnswerSelectedListener(Action<AnswerType> action)
    {
        onAnswerSelected = action;
    }

    /// <summary>
    /// Gets the question view.
    /// </summary>
    public QuestionView GetQuestionView()
    {
        return questionView;
    }

    /// <summary>
    /// Gets the answer view.
    /// </summary>
    public AnswerView GetAnswerView(AnswerType answer)
    {
        return answers[(int)answer];
    }
}
