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
        StartCoroutine(SyncanoMock(OnQuestionSubmitted));
    }

    private IEnumerator SyncanoMock(System.Action<Response> callback)
    {
        yield return new WaitForSeconds(1.5f);
        callback.Invoke(null);
    }

    private void OnQuestionSubmitted(Response response)
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
            summary.SetCustomButton("Try again", OnTryAgain);
        }
        else
        {
            SummaryPanel summary = QuestionManagerUI.Instance.SummaryPanel;
            summary.Show("Success!\nQuestion accepted.");
            summary.SetSuccessColor();
            summary.SetBackButton("Back", OnBackClick);
            summary.SetCustomButton("Add Next", StartAddQuestion);
        }
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
