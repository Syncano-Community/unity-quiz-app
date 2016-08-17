using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FormPanel : MonoBehaviour
{
    [SerializeField]
    private Dropdown difficultyDropdown;

    public DifficultyType Difficulty
    {
        get { return (DifficultyType)difficultyDropdown.value; }
        set { difficultyDropdown.value = (int)value; }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
