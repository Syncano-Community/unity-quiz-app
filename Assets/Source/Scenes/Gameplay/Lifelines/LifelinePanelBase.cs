using UnityEngine;
using System.Collections;

public class LifelinePanelBase : MonoBehaviour
{
    public bool IsShown { get; private set; }

    public virtual void Show(Question question)
    {
        IsShown = true;
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        IsShown = false;
        gameObject.SetActive(false);
    }
}
