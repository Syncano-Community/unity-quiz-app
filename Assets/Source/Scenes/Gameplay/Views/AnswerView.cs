﻿using UnityEngine;
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

    public Button.ButtonClickedEvent onClick
    {
        get { return button.onClick; }
    }

    public void SetInteractable(bool interactable)
    {
        button.enabled = interactable;
    }

    public override void SetPlayMode ()
    {
        base.SetPlayMode ();
        SetInteractable(false);
    }

    public override void SetEditMode ()
    {
        base.SetEditMode ();
        SetInteractable(false);
    }

    public void HighlightCorrect()
    {
        button.image.sprite = correct;
    }

    public void HiglightIncorrect()
    {
        button.image.sprite = incorrect;
    }

    public void HiglightOff()
    {
        button.image.sprite = normal;
    }
}
