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

    public bool ShouldPickCorrect(DifficultyType difficulty)
    {
        int correctChance = 100;

        switch (difficulty)
        {
        case DifficultyType.EASY:
            correctChance -= 10;
            break;
        case DifficultyType.MEDIUM:
            correctChance -= 20;
            break;
        case DifficultyType.HARD:
            correctChance -= 30;
            break;
        }

        return (Random.Range(0, 100) < correctChance);
    }
}
