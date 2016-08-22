using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SubmitPanel : CommunicationPanel
{
    [SerializeField]
    private Button submitButton;

    private bool isDownloading;
    private Question question;

    void Start()
    {
        submitButton.onClick.AddListener(() => OnSubmitClick());
    }

    public void StartAddQuestion()
    {
        question = new Question();
        question.isModerated = false;

        ShowEditView();
        QuestionManagerUI.Instance.QuestionPanel.ClearViews();
        Show();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
	

    private void OnSubmitClick()
    {
        FillQuestion(question);

        if (question.IsValid())
        {
            SubmitQuestion();
        }
    }

    public void OnBackClick()
    {
        Hide();
        ShowBlockedView();
        QuestionManagerUI.Instance.QuestionPanel.ClearViews();
        QuestionManagerUI.Instance.MenuPanel.Show();
    }

    private void ShowEditView()
    {
        QuestionManagerUI.Instance.QuestionPanel.SetEditMode();
        QuestionManagerUI.Instance.FormPanel.Show();
    }

    private void ShowBlockedView()
    {
        QuestionManagerUI.Instance.QuestionPanel.SetPlayMode();
        QuestionManagerUI.Instance.FormPanel.Hide();
    }

    private void SubmitQuestion()
    {
        if (isDownloading)
            return;

        isDownloading = true;
        QuestionManagerUI.Instance.LoadingPanel.Show("Loading question...");
        ShowBlockedView();
        Syncano.Instance.Please().Save(question, OnSubmitSuccess, OnSubmitFail);
    }

    private void OnSubmitSuccess(Response<Question> response)
    {
        isDownloading = false;
        QuestionManagerUI.Instance.LoadingPanel.Hide();
        Hide();

        SummaryPanel summary = QuestionManagerUI.Instance.SummaryPanel;
        summary.Show("Success!\nQuestion submitted.");
        summary.SetSuccessColor();
        summary.SetBackButton("Back", OnBackClick);
        summary.SetCustomButton("Add Next", StartAddQuestion);
    }

    private void OnSubmitFail(Response<Question> response)
    {
        isDownloading = false;
        QuestionManagerUI.Instance.LoadingPanel.Hide();
        Hide();

        SummaryPanel summary = QuestionManagerUI.Instance.SummaryPanel;
        summary.Show("Failed to submit question.\n" + response.webError);
        summary.SetErrorColor();
        summary.SetBackButton("Edit", OnEdit);
        summary.SetCustomButton("Try again", OnTryAgain);
    }

    private void OnTryAgain()
    {
        Show();
        SubmitQuestion();
    }

    private void OnEdit()
    {
        Show();
        ShowEditView();
    }
}
