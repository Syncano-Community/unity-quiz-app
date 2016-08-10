using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SubmitPanel : MonoBehaviour
{
    [SerializeField]
    private Button submitButton;

    [SerializeField]
    private Dropdown difficultyDropdown;

    [SerializeField]
    private GameObject loadingImage;

    private QuestionPanel questionPanel;
    private Question question;

	void Start ()
    {
        loadingImage.gameObject.SetActive(false);
        questionPanel = QuestionManagerUI.Instance.QuestionPanel;
        questionPanel.SetEditMode();
        question = new Question();
	}
	
    /* ui event */ public void OnSubmitClick()
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

    private void SubmitQuestion(Question question)
    {
        submitButton.interactable = false;
        loadingImage.gameObject.SetActive(true);
        questionPanel.SetPlayMode();
        StartCoroutine(SyncanoMock(OnQuestionSubmitted));
    }

    private IEnumerator SyncanoMock(System.Action<Response> callback)
    {
        yield return new WaitForSeconds(1.5f);
        callback.Invoke(null);
    }

    private void OnQuestionSubmitted(Response response)
    {
        submitButton.interactable = true;
        loadingImage.gameObject.SetActive(false);
    }
}
