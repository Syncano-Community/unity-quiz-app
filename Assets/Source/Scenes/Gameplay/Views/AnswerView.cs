using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Represents answer button.
/// </summary>
public class AnswerView : InputFieldView
{
    [SerializeField]
    private Sprite normal;

    [SerializeField]
    private Sprite correct;

    [SerializeField]
    private Sprite incorrect;

    [SerializeField]
    private Button button;

    /// <summary>
    /// Gets the on click. It allows assign from the outside.
    /// </summary>
    public Button.ButtonClickedEvent onClick
    {
        get { return button.onClick; }
    }

    /// <summary>
    /// Sets the button interactable.
    /// </summary>
    public void SetInteractable(bool interactable)
    {
        button.enabled = interactable;
    }

    /// <summary>
    /// In play mode we just display text.
    /// Button should not be interactable yet.
    /// </summary>
    public override void SetPlayMode ()
    {
        base.SetPlayMode ();
        SetInteractable(false);
    }

    /// <summary>
    /// In edit mode we display text, background and make it editable.
    /// Button will not be interactable.
    /// </summary>
    public override void SetEditMode ()
    {
        base.SetEditMode ();
        SetInteractable(false);
    }

    /// <summary>
    /// Highlights the button as correct answer.
    /// </summary>
    public void HighlightCorrect()
    {
        button.image.sprite = correct;
    }

    /// <summary>
    /// Highlights the button as incorrect answer.
    /// </summary>
    public void HiglightIncorrect()
    {
        button.image.sprite = incorrect;
    }

    /// <summary>
    /// Turn off higlight.
    /// </summary>
    public void HiglightOff()
    {
        button.image.sprite = normal;
    }
}
