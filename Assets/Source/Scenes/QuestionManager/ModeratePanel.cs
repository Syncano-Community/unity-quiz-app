using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Syncano.Data;
using Syncano;

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
        // Prepare onClick callbacks.
        acceptButton.onClick.AddListener(() => OnAcceptClick());
        rejectButton.onClick.AddListener(() => OnRejectClick());
        backButton.onClick.AddListener(() => OnBackClick());
    }

    /// <summary>
    /// Show moderation panel and start downloading question.
    /// </summary>
    public void StartModerate()
    {
        question = null;
        QuestionManagerUI.Instance.QuestionPanel.ClearViews();
        QuestionManagerUI.Instance.QuestionPanel.SetPlayMode();
        QuestionManagerUI.Instance.FormPanel.Hide();
        Show();
        DownloadQuestion();
    }

    /// <summary>
    /// Show moderation panel.
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Hide moderation panel.
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// On accept click callback.
    /// </summary>
    public void OnAcceptClick()
    {
        FillQuestion(question);

        if (question.IsValid())
            AcceptQuestion();
    }

    /// <summary>
    /// On reject click callback.
    /// </summary>
    public void OnRejectClick()
    {
        RejectQuestion();
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
    /// Enable edit.
    /// </summary>
    private void ShowEditView()
    {
        QuestionManagerUI.Instance.QuestionPanel.SetEditMode();
        QuestionManagerUI.Instance.FormPanel.Show();
    }

    /// <summary>
    /// Disable edit.
    /// </summary>
    private void ShowBlockedView()
    {
        QuestionManagerUI.Instance.QuestionPanel.SetPlayMode();
        QuestionManagerUI.Instance.FormPanel.Hide();
    }

    /// <summary>
    /// Start downloading question. Show loading panel.
    /// </summary>
    private void DownloadQuestion()
    {
        if (isDownloading)
            return;

        isDownloading = true;
        QuestionManagerUI.Instance.LoadingPanel.Show("Loading question...");
		SyncanoClient.Instance.Please().RunScriptEndpointUrl(Constant.SCRIPT_ENDPOINT_GET_QUESTION_TO_MODERATE_URL, OnQuestionDownloaded );
    }

    /// <summary>
    /// Question download finished.
    /// </summary>
    private void OnQuestionDownloaded(ScriptEndpoint endpoint)
    {
        isDownloading = false;
        QuestionManagerUI.Instance.LoadingPanel.Hide();

        if (endpoint.IsSuccess)
        {
            if (string.IsNullOrEmpty(endpoint.stdout) || "undefined".Equals(endpoint.stdout))
            {
                ShowDownloadFailSummary("No questions to moderate.");
                return;
            }
            else
            {
				question = JsonUtility.FromJson<Question>(endpoint.stdout);
                FillForm(question);
                ShowEditView();
                return;
            }
        }
        else
        {
            ShowDownloadFailSummary("Failed to download question.\n" + endpoint.webError);
            return;
        }
    }

    /// <summary>
    /// Shows the download fail summary.
    /// </summary>
    private void ShowDownloadFailSummary(string failMessage)
    {
        Hide();
        SummaryPanel summary = QuestionManagerUI.Instance.SummaryPanel;
        summary.Show(failMessage);
        summary.SetErrorColor();
        summary.SetBackButton("Back", OnBackClick);
        summary.SetCustomButton("Try again", OnTryDownloadAgain);
    }

    /// <summary>
    /// Mark question as moderated and send update.
    /// </summary>
    private void AcceptQuestion()
    {
        if (isDownloading)
            return;

        isDownloading = true;
        QuestionManagerUI.Instance.LoadingPanel.Show("Updating question...");
        ShowBlockedView();
        question.isModerated = true; // Accept question.
        SyncanoClient.Instance.Please().Save(question, OnAcceptSuccess, OnAcceptFail);
    }

    /// <summary>
    /// Question update success.
    /// </summary>
    private void OnAcceptSuccess(Response<Question> response)
    {
        isDownloading = false;
        QuestionManagerUI.Instance.LoadingPanel.Hide();
        Hide();

        SummaryPanel summary = QuestionManagerUI.Instance.SummaryPanel;
        summary.Show("Success!\nQuestion accepted.");
        summary.SetSuccessColor();
        summary.SetBackButton("Back", OnBackClick);
        summary.SetCustomButton("Next", StartModerate);
    }

    /// <summary>
    /// Question update fail.
    /// </summary>
    private void OnAcceptFail(Response<Question> response)
    {
        isDownloading = false;
        QuestionManagerUI.Instance.LoadingPanel.Hide();
        Hide();

        SummaryPanel summary = QuestionManagerUI.Instance.SummaryPanel;
        summary.Show("Failed to accept question.\n" + response.webError);
        summary.SetErrorColor();
        summary.SetBackButton("Edit", OnEdit);
        summary.SetCustomButton("Try again", OnTryAcceptAgain);
    }

    /// <summary>
    /// Delete pending question.
    /// </summary>
    private void RejectQuestion()
    {
        if (isDownloading)
            return;

        isDownloading = true;
        QuestionManagerUI.Instance.LoadingPanel.Show("Deleting question...");
        ShowBlockedView();
        SyncanoClient.Instance.Please().Delete(question, OnRejectSuccess, OnRejectFail);
    }

    /// <summary>
    /// Question delete success.
    /// </summary>
    private void OnRejectSuccess(Response<Question> response)
    {
        isDownloading = false;
        QuestionManagerUI.Instance.LoadingPanel.Hide();
        Hide();

        SummaryPanel summary = QuestionManagerUI.Instance.SummaryPanel;
        summary.Show("Success!\nQuestion deleted.");
        summary.SetSuccessColor();
        summary.SetBackButton("Back", OnBackClick);
        summary.SetCustomButton("Next", StartModerate);
    }

    /// <summary>
    /// Question delete fail.
    /// </summary>
    private void OnRejectFail(Response<Question> response)
    {
        isDownloading = false;
        QuestionManagerUI.Instance.LoadingPanel.Hide();
        Hide();

        SummaryPanel summary = QuestionManagerUI.Instance.SummaryPanel;
        summary.Show("Failed to delete question.\n" + response.webError);
        summary.SetErrorColor();
        summary.SetBackButton("Edit", OnEdit);
        summary.SetCustomButton("Try again", OnTryRejectAgain);
    }

    /// <summary>
    /// Try download again callback.
    /// </summary>
    private void OnTryDownloadAgain()
    {
        Show();
        DownloadQuestion();
    }

    /// <summary>
    /// Try accept again.
    /// </summary>
    private void OnTryAcceptAgain()
    {
        Show();
        AcceptQuestion();
    }

    /// <summary>
    /// Try reject again.
    /// </summary>
    private void OnTryRejectAgain()
    {
        Show();
        RejectQuestion();
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
