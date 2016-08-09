using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FiftyFiftyPanel : LifelinePanelBase
{
    [SerializeField]
    private Image fillImage;

    public override void Show (Question question)
    {
        base.Show (question);
        fillImage.fillAmount = 0;
        StartCoroutine(ShowLifeline(question));
    }

    public IEnumerator ShowLifeline(Question question)
    {
        float fill = 0;

        while (true)
        {
            fill += 1 * Time.deltaTime;
            fillImage.fillAmount = Mathf.Min(fill, 1);

            if (fill >= 1)
                break;
            
            yield return null;
        }

        OnLifelineShown(question);
    }

    private void OnLifelineShown(Question question)
    {
        RemoveTwoIncorrect(question);
        GameUI.Instance.QuestionPanel.RefreshAvailableAnswers();
    }

    private void RemoveTwoIncorrect(Question question)
    {
        question.AvailableAnswers.Shuffle();

        while (question.AvailableAnswers.Count > 2)
        {
            for (int i = 0; i < question.AvailableAnswers.Count; i++)
            {
                if (question.AvailableAnswers[i] != question.CorrectAnswer)
                {
                    question.AvailableAnswers.RemoveAt(i);
                    break; // Look for next - index has moved.
                }
            }
        }
    }
}