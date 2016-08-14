using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FormPanel : MonoBehaviour
{
    [SerializeField]
    private Dropdown difficultyDropdown;

    public void Show(Question question)
    {
        difficultyDropdown.value = (int)question.difficultyType;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
