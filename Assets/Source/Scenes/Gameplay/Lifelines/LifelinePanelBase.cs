using UnityEngine;
using UnityEngine.UI;
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

    protected float GetAlpha(Image image)
    {
        return image.color.a;
    }

    protected void SetAlpha(Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}
