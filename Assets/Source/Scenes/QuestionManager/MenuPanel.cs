using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuPanel : MonoBehaviour
{
    [SerializeField]
    private Button addQuestionButton;

    [SerializeField]
    private Button moderateButton;

    void Start()
    {
        addQuestionButton.onClick.AddListener(() => OnAddQuestionClick());
        moderateButton.onClick.AddListener(() => OnModerateClick());
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnAddQuestionClick()
    {
        Hide();
        QuestionManagerUI.Instance.SubmitPanel.StartAddQuestion();
    }

    public void OnModerateClick()
    {
        Hide();
        QuestionManagerUI.Instance.ModeratePanel.StartModerate();
    }
}
