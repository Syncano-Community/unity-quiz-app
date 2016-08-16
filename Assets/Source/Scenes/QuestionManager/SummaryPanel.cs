using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SummaryPanel : MonoBehaviour
{
    [SerializeField]
    private Button customButton;

    [SerializeField]
    private Button backButton;

    [SerializeField]
    private Text summaryText;

    [SerializeField]
    private Color successColor;

    [SerializeField]
    private Color errorColor;

    private System.Action customAction;
    private System.Action backAction;

    void Start()
    {
        customButton.onClick.AddListener(() => OnCustomClick());
        backButton.onClick.AddListener(() => OnBackClick());
    }

    public void Show(string text)
    {
        summaryText.text = text;
        customButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
        customAction = null;
        backAction = null;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetCustomButton(string text, System.Action action)
    {
        customButton.gameObject.SetActive(true);
        customAction = action;
        customButton.GetComponentInChildren<Text>().text = text;
    }

    public void SetBackButton(string text, System.Action action)
    {
        backButton.gameObject.SetActive(true);
        backAction = action;
        backButton.GetComponentInChildren<Text>().text = text;
    }

    public void SetSuccessColor()
    {
        summaryText.color = successColor;
    }

    public void SetErrorColor()
    {
        summaryText.color = errorColor;
    }

    private void OnCustomClick()
    {
        if (customAction != null)
        {
            Hide();
            customAction.Invoke();
        }
    }

    private void OnBackClick()
    {
        if (backAction != null)
        {
            Hide();
            backAction.Invoke();
        }
    }
}
