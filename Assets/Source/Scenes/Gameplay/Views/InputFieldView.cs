using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputFieldView : MonoBehaviour
{
    private InputField inputField;
    private Image inputFieldBackgroud;

	protected virtual void Awake ()
    {
        inputField = GetComponentInChildren<InputField>();
        inputFieldBackgroud = inputField.GetComponent<Image>();
	}

    public virtual void SetPlayMode()
    {
        inputFieldBackgroud.enabled = false;
        inputField.interactable = false;
        inputField.placeholder.gameObject.SetActive(false);
        inputField.textComponent.raycastTarget = false;
    }

    public virtual void SetEditMode()
    {
        inputFieldBackgroud.enabled = true;
        inputField.interactable = true;
        inputField.placeholder.gameObject.SetActive(true);
        inputField.textComponent.raycastTarget = true;
    }

    public void SetText(string text)
    {
        inputField.text = text;
    }
}
