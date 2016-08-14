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

    public void Show(string text)
    {
        summaryText.text = text;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetSuccessColor()
    {
        summaryText.color = successColor;
    }

    public void SetErrorColor()
    {
        summaryText.color = errorColor;
    }
}
