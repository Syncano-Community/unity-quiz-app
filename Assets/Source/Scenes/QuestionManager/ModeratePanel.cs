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

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
