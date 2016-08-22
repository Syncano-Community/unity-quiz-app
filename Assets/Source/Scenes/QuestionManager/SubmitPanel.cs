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
        // Prepare onClick callback.
        submitButton.onClick.AddListener(() => OnSubmitClick());
    }

    /// <summary>
    /// Show add question panel and start edit mode.
    /// </summary>
    public void StartAddQuestion()
    {
        question = new Question();
        question.isModerated = false;

        ShowEditView();
        QuestionManagerUI.Instance.QuestionPanel.ClearViews();
        Show();
    }

    /// <summary>
    /// Show add question panel.
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Hide add question panel.
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }
	
    /// <summary>
    /// On submit click callback.
    /// </summary>
    private void OnSubmitClick()
    {
        FillQuestion(question);

        if (question.IsValid())
        {
            SubmitQuestion();
        }
    }

    /// <summary>
    /// On back click callback.
    /// </summary>
    public void OnBackClick()
    {
        Hide();
        ShowBlockedView();
        QuestionManagerUI.Instance.QuestionPanel.ClearViews();
        QuestionManagerUI.Instance.MenuPanel.Show();
    }

    /// <summary>
    /// Enable edit mode.
    /// </summary>
    private void ShowEditView()
    {
        QuestionManagerUI.Instance.QuestionPanel.SetEditMode();
        QuestionManagerUI.Instance.FormPanel.Show();
    }

    /// <summary>
    /// Disable edit mode.
    /// </summary>
    private void ShowBlockedView()
    {
        QuestionManagerUI.Instance.QuestionPanel.SetPlayMode();
        QuestionManagerUI.Instance.FormPanel.Hide();
    }

    /// <summary>
    /// Start question submit and show loading screen.
    /// </summary>
    private void SubmitQuestion()
    {
        if (isDownloading)
            return;

        isDownloading = true;
        QuestionManagerUI.Instance.LoadingPanel.Show("Loading question...");
        ShowBlockedView();
        Syncano.Instance.Please().Save(question, OnSubmitSuccess, OnSubmitFail);
    }

    /// <summary>
    /// Add question success.
    /// </summary>
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

    /// <summary>
    /// Add question fail.
    /// </summary>
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

    /// <summary>
    /// Try submit again.
    /// </summary>
    private void OnTryAgain()
    {
        Show();
        SubmitQuestion();
    }

    /// <summary>
    /// Switch back to edit mode.
    /// </summary>
    private void OnEdit()
    {
        Show();
        ShowEditView();
    }
}
