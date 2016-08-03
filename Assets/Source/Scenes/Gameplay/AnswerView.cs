using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnswerView : InputFieldView
{
    private Button button;

    protected override void Awake ()
    {
        base.Awake ();
        button = GetComponent<Button>();
    }

    public override void SetPlayMode ()
    {
        base.SetPlayMode ();
        button.interactable = true;
    }

    public override void SetEditMode ()
    {
        base.SetEditMode ();
        button.interactable = false;
    }
}
