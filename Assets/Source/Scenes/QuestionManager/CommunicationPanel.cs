﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommunicationPanel : MonoBehaviour
{
    protected void FillQuestion(Question question)
    {
        QuestionPanel questionPanel = QuestionManagerUI.Instance.QuestionPanel;
        question.text = questionPanel.GetQuestionView().GetText();
        question.difficultyType = QuestionManagerUI.Instance.FormPanel.Difficulty;

        if (question.answers == null)
            question.answers = new List<string>();
        else 
            question.answers.Clear();

        question.answers.Add(questionPanel.GetAnswerView(AnswerType.A).GetText());
        question.answers.Add(questionPanel.GetAnswerView(AnswerType.B).GetText());
        question.answers.Add(questionPanel.GetAnswerView(AnswerType.C).GetText());
        question.answers.Add(questionPanel.GetAnswerView(AnswerType.D).GetText());
    }

    protected void FillForm(Question question)
    {
        QuestionPanel questionPanel = QuestionManagerUI.Instance.QuestionPanel;
        questionPanel.FillQuestion(question);
        QuestionManagerUI.Instance.FormPanel.Difficulty = question.difficultyType;
    }

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