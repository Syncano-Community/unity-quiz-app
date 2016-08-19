using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ModeratePanel : CommunicationPanel
{
    [SerializeField]
    private Button acceptButton;

    [SerializeField]
    private Button rejectButton;

    [SerializeField]
    private Button backButton;

    private bool isDownloading;
    private Question question;

    void Start()
    {
        acceptButton.onClick.AddListener(() => OnAcceptClick());
        rejectButton.onClick.AddListener(() => OnRejectClick());
        backButton.onClick.AddListener(() => OnBackClick());
    }

    public void StartModerate()
    {
        question = null;
        QuestionManagerUI.Instance.QuestionPanel.SetPlayMode();
        QuestionManagerUI.Instance.QuestionPanel.ClearViews();
        QuestionManagerUI.Instance.FormPanel.Hide();
        Show();
        DownloadQuestion();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnAcceptClick()
    {
        FillQuestion(question);

        if (question.IsValid())
            AcceptQuestion();
    }

    public void OnRejectClick()
    {
        RejectQuestion();
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

    private void DownloadQuestion()
    {
        if (isDownloading)
            return;

        isDownloading = true;
        QuestionManagerUI.Instance.LoadingPanel.Show("Loading question...");
        Syncano.Instance.Please().CallScriptEndpoint("6349c3ec1208c0be5ade53b154427d4eb5cb1628", "get_question_to_moderate", OnQuestionDownloaded);
    }

    private void OnQuestionDownloaded(ScriptEndpoint endpoint)
    {
        isDownloading = false;
        QuestionManagerUI.Instance.LoadingPanel.Hide();

        bool error = false;
        if (error)
        {
            Hide();
            SummaryPanel summary = QuestionManagerUI.Instance.SummaryPanel;
            summary.Show("Failed to download question.");
            summary.SetErrorColor();
            summary.SetBackButton("Back", OnBackClick);
            summary.SetCustomButton("Try again", OnTryDownloadAgain);
        }
        else
        {
            question = Question.FromJson(endpoint.stdout);
            FillForm(question);
            ShowEditView();
        }

        isDownloading = false;
    }

    private void AcceptQuestion()
    {
        if (isDownloading)
            return;

        isDownloading = true;
        QuestionManagerUI.Instance.LoadingPanel.Show("Updating question...");
        ShowBlockedView();
        question.isModerated = true; // Accept question.
        Syncano.Instance.Please().Save(question, OnQuestionAccepted);
    }

    private void OnQuestionAccepted(Response<Question> response)
    {
        isDownloading = false;
        QuestionManagerUI.Instance.LoadingPanel.Hide();
        Hide();

        if (response.IsSuccess)
        {
            SummaryPanel summary = QuestionManagerUI.Instance.SummaryPanel;
            summary.Show("Success!\nQuestion accepted.");
            summary.SetSuccessColor();
            summary.SetBackButton("Back", OnBackClick);
            summary.SetCustomButton("Next", StartModerate);
        }
        else
        {
            SummaryPanel summary = QuestionManagerUI.Instance.SummaryPanel;
            summary.Show("Failed to accept question.\n" + response.webError);
            summary.SetErrorColor();
            summary.SetBackButton("Edit", OnEdit);
            summary.SetCustomButton("Try again", OnTryAcceptAgain);
        }
    }

    private void RejectQuestion()
    {
        if (isDownloading)
            return;

        isDownloading = true;
        QuestionManagerUI.Instance.LoadingPanel.Show("Deleting question...");
        ShowBlockedView();
        Syncano.Instance.Please().Delete(question, OnQuestionRejected);
    }

    private void OnQuestionRejected(Response<Question> response)
    {
        isDownloading = false;
        QuestionManagerUI.Instance.LoadingPanel.Hide();
        Hide();

        if (response.IsSuccess)
        {
            SummaryPanel summary = QuestionManagerUI.Instance.SummaryPanel;
            summary.Show("Success!\nQuestion deleted.");
            summary.SetSuccessColor();
            summary.SetBackButton("Back", OnBackClick);
            summary.SetCustomButton("Next", StartModerate);
        }
        else
        {
            SummaryPanel summary = QuestionManagerUI.Instance.SummaryPanel;
            summary.Show("Failed to delete question.\n" + response.webError);
            summary.SetErrorColor();
            summary.SetBackButton("Edit", OnEdit);
            summary.SetCustomButton("Try again", OnTryRejectAgain);
        }
    }

    private void OnTryDownloadAgain()
    {
        Show();
        DownloadQuestion();
    }

    private void OnTryAcceptAgain()
    {
        Show();
        AcceptQuestion();
    }

    private void OnTryRejectAgain()
    {
        Show();
        RejectQuestion();
    }

    private void OnEdit()
    {
        Show();
        ShowEditView();
    }
}
