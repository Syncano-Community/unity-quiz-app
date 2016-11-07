using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommunicationPanel : MonoBehaviour
{
    /// <summary>
    /// Use form and question panel data to fill question fields.
    /// </summary>
    protected void FillQuestion(Question question)
    {
        QuestionPanel questionPanel = QuestionManagerUI.Instance.QuestionPanel;
        question.Text = questionPanel.GetQuestionView().GetText();
        question.DifficultyType = QuestionManagerUI.Instance.FormPanel.Difficulty;

        if (question.Answers == null)
            question.Answers = new List<string>();
        else 
            question.Answers.Clear();

        question.Answers.Add(questionPanel.GetAnswerView(AnswerType.A).GetText());
        question.Answers.Add(questionPanel.GetAnswerView(AnswerType.B).GetText());
        question.Answers.Add(questionPanel.GetAnswerView(AnswerType.C).GetText());
        question.Answers.Add(questionPanel.GetAnswerView(AnswerType.D).GetText());
    }

    /// <summary>
    /// Use values for mquestion to fill form and question panel.
    /// </summary>
    protected void FillForm(Question question)
    {
        QuestionPanel questionPanel = QuestionManagerUI.Instance.QuestionPanel;
        questionPanel.FillQuestion(question);
        QuestionManagerUI.Instance.FormPanel.Difficulty = question.DifficultyType;
    }

    /// <summary>
    /// Clear form.
    /// </summary>
    protected void ClearForm()
    {
        QuestionPanel questionPanel = QuestionManagerUI.Instance.QuestionPanel;
        questionPanel.GetQuestionView().SetText(null);
        questionPanel.GetAnswerView(AnswerType.A).SetText(null);
        questionPanel.GetAnswerView(AnswerType.B).SetText(null);
        questionPanel.GetAnswerView(AnswerType.C).SetText(null);
        questionPanel.GetAnswerView(AnswerType.D).SetText(null);
        QuestionManagerUI.Instance.FormPanel.Difficulty = DifficultyType.EASY;
    }
}
