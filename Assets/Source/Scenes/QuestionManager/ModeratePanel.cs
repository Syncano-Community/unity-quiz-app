using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ModeratePanel : MonoBehaviour
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

        QuestionManagerUI.Instance.QuestionPanel.SetPlayMode();
        QuestionManagerUI.Instance.FormPanel.Hide();
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
    }

    public void OnRejectClick()
    {
    }

    public void OnBackClick()
    {
        Hide();
        QuestionManagerUI.Instance.QuestionPanel.SetPlayMode();
        QuestionManagerUI.Instance.FormPanel.Hide();
        QuestionManagerUI.Instance.MenuPanel.Show();
    }

    private void DownloadQuestion()
    {
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
        QuestionManagerUI.Instance.LoadingPanel.Hide();

        bool error = false;
        if (error)
        {
            Hide();
            SummaryPanel summary = QuestionManagerUI.Instance.SummaryPanel;
            summary.Show("Failed to download question.");
            summary.SetErrorColor();
            summary.SetBackButton("Back", OnBackClick);
            summary.SetCustomButton("Try again", OnTryAgain);
        }
        else
        {
            question = new Question(); // Downloaded question mock.
            QuestionManagerUI.Instance.QuestionPanel.SetEditMode();
            QuestionManagerUI.Instance.FormPanel.Show();
        }

        isDownloading = false;
    }

    private void OnTryAgain()
    {
        Show();
        DownloadQuestion();
    }
}
