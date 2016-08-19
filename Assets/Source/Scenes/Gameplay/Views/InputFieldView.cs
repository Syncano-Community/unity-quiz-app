using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Represents field which can be editable or just display text.
/// </summary>
public class InputFieldView : MonoBehaviour
{
    [SerializeField]
    private InputField inputField;

    [SerializeField]
    private Image inputFieldBackgroud;

    /// <summary>
    /// In play mode we just display text.
    /// </summary>
    public virtual void SetPlayMode()
    {
        inputFieldBackgroud.enabled = false;
        inputField.interactable = false;
        inputField.placeholder.gameObject.SetActive(false);
        inputField.textComponent.raycastTarget = false;
    }

    /// <summary>
    /// In edit mode we display text, background and make it editable.
    /// </summary>
    public virtual void SetEditMode()
    {
        inputFieldBackgroud.enabled = true;
        inputField.interactable = true;
        inputField.placeholder.gameObject.SetActive(true);
        inputField.textComponent.raycastTarget = true;
    }

    /// <summary>
    /// Gets the text.
    /// </summary>
    public string GetText()
    {
        return inputField.text;
    }

    /// <summary>
    /// Sets the text.
    /// </summary>
    public void SetText(string text)
    {
        inputField.text = text;
    }
}
