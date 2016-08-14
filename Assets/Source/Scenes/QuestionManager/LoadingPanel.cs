using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingPanel : MonoBehaviour
{
    [SerializeField]
    private Text loadingText;

    public void Show(string text)
    {
        loadingText.text = text;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
