using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SubmitPanel : MonoBehaviour
{
    private enum State
    {
        EDIT,
        SUBMITTING,
        SUMMARY_SUCCESS,
        SUMMARY_ERROR
    }

    [SerializeField]
    private GameObject formGameObject;

    [SerializeField]
    private Button submitButton;

    [SerializeField]
    private Dropdown difficultyDropdown;

    [SerializeField]
    private GameObject loadingImage;

    [SerializeField]
    private Text summaryText;

    [SerializeField]
    private Color successColor;

    [SerializeField]
    private Color errorColor;

    private QuestionPanel questionPanel;
    private Question question;
    private State currentState;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

	void Start ()
    {
        questionPanel = QuestionManagerUI.Instance.QuestionPanel;
        question = new Question();
        SetState(State.EDIT);
	}
	
    /* ui event */ public void OnSubmitClick()
    {
        switch (currentState)
        {
        case State.EDIT:
            TrySubmit();
            break;

        case State.SUMMARY_SUCCESS:
            ClearForm();
            SetState(State.EDIT);
            break;

        case State.SUMMARY_ERROR:
            SetState(State.EDIT);
            break;
        }
    }

    private void TrySubmit()
    {
        question.text = questionPanel.GetQuestionView().GetText();
        question.difficultyType = (DifficultyType)difficultyDropdown.value;

        if (question.answers == null)
            question.answers = new List<string>();
        else 
            question.answers.Clear();

        question.answers.Add(questionPanel.GetAnswerView(AnswerType.A).GetText());
        question.answers.Add(questionPanel.GetAnswerView(AnswerType.B).GetText());
        question.answers.Add(questionPanel.GetAnswerView(AnswerType.C).GetText());
        question.answers.Add(questionPanel.GetAnswerView(AnswerType.D).GetText());

        if (question.IsValid())
        {
            SubmitQuestion(question);
        }
    }

    private void SetState(State state)
    {
        currentState = state;

        switch (state)
        {
        case State.EDIT:
            questionPanel.SetEditMode();
            submitButton.interactable = true;
            submitButton.GetComponentInChildren<Text>().text = "Submit";
            loadingImage.gameObject.SetActive(false);
            formGameObject.SetActive(true);
            summaryText.gameObject.SetActive(false);
            break;
        case State.SUBMITTING:
            questionPanel.SetPlayMode();
            submitButton.interactable = false;
            submitButton.GetComponentInChildren<Text>().text = "Wait...";
            formGameObject.SetActive(false);
            loadingImage.gameObject.SetActive(true);
            summaryText.gameObject.SetActive(false);
            break;
        
        case State.SUMMARY_SUCCESS:
            questionPanel.SetPlayMode();
            submitButton.interactable = true;
            submitButton.GetComponentInChildren<Text>().text = "Add next";
            formGameObject.SetActive(false);
            loadingImage.gameObject.SetActive(false);
            summaryText.gameObject.SetActive(true);
            summaryText.color = successColor;
            break;

        case State.SUMMARY_ERROR:
            questionPanel.SetPlayMode();
            submitButton.interactable = true;
            submitButton.GetComponentInChildren<Text>().text = "Edit";
            formGameObject.SetActive(false);
            loadingImage.gameObject.SetActive(false);
            summaryText.gameObject.SetActive(true);
            summaryText.color = errorColor;
            break;
        }
    }

    private void ClearForm()
    {
        questionPanel.GetQuestionView().SetText(null);
        questionPanel.GetAnswerView(AnswerType.A).SetText(null);
        questionPanel.GetAnswerView(AnswerType.B).SetText(null);
        questionPanel.GetAnswerView(AnswerType.C).SetText(null);
        questionPanel.GetAnswerView(AnswerType.D).SetText(null);
        difficultyDropdown.value = 0;
    }

    private void SubmitQuestion(Question question)
    {
        SetState(State.SUBMITTING);
        StartCoroutine(SyncanoMock(OnQuestionSubmitted));
    }

    private IEnumerator SyncanoMock(System.Action<Response> callback)
    {
        yield return new WaitForSeconds(1.5f);
        callback.Invoke(null);
    }

    private void OnQuestionSubmitted(Response response)
    {
        bool error = true;

        if (error)
        {
            summaryText.text = "Error!\nCommunication not implemented.";
            SetState(State.SUMMARY_ERROR);
        }
        else
        {
            summaryText.text = "Success!\nNew question added.";
            SetState(State.SUMMARY_SUCCESS);
        }
    }
}
