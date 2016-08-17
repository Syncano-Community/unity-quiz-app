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
        StartCoroutine(SyncanoMock(OnQuestionDownloaded));
    }

    private IEnumerator SyncanoMock(System.Action<Response> callback)
    {
        yield return new WaitForSeconds(1.5f);
        callback.Invoke(null);
    }

    private void OnQuestionDownloaded(Response response)
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
            question = new Question(); // Downloaded question mock.
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
        StartCoroutine(SyncanoMock(OnQuestionAccepted));
    }

    private void OnQuestionAccepted(Response response)
    {
        isDownloading = false;
        QuestionManagerUI.Instance.LoadingPanel.Hide();
        Hide();

        bool error = false;
        if (error)
        {
            SummaryPanel summary = QuestionManagerUI.Instance.SummaryPanel;
            summary.Show("Failed to accept question.");
            summary.SetErrorColor();
            summary.SetBackButton("Edit", OnEdit);
            summary.SetCustomButton("Try again", OnTryAcceptAgain);
        }
        else
        {
            SummaryPanel summary = QuestionManagerUI.Instance.SummaryPanel;
            summary.Show("Success!\nQuestion accepted.");
            summary.SetSuccessColor();
            summary.SetBackButton("Back", OnBackClick);
            summary.SetCustomButton("Next", StartModerate);
        }
    }

    private void RejectQuestion()
    {
        if (isDownloading)
            return;

        isDownloading = true;
        QuestionManagerUI.Instance.LoadingPanel.Show("Deleting question...");
        ShowBlockedView();
        StartCoroutine(SyncanoMock(OnQuestionRejected));
    }

    private void OnQuestionRejected(Response response)
    {
        isDownloading = false;
        QuestionManagerUI.Instance.LoadingPanel.Hide();
        Hide();

        bool error = true;
        if (error)
        {
            SummaryPanel summary = QuestionManagerUI.Instance.SummaryPanel;
            summary.Show("Failed to delete question.");
            summary.SetErrorColor();
            summary.SetBackButton("Edit", OnEdit);
            summary.SetCustomButton("Try again", OnTryRejectAgain);
        }
        else
        {
            SummaryPanel summary = QuestionManagerUI.Instance.SummaryPanel;
            summary.Show("Success!\nQuestion deleted.");
            summary.SetSuccessColor();
            summary.SetBackButton("Back", OnBackClick);
            summary.SetCustomButton("Next", StartModerate);
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
