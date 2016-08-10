using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PhonePanel : LifelinePanelBase
{
    [SerializeField]
    private Text suggestText;

    [SerializeField]
    private Text answerText;

    [SerializeField]
    private Image fillImage;

    public override void Show (Question question)
    {
        base.Show (question);

        AnswerType suggestion = GetSuggestedAnswer(question);
        answerText.text = suggestion.ToString();
        StartCoroutine(ShowLifeline(question));
    }

    public IEnumerator ShowLifeline(Question question)
    {
        // ======= Fill ======= //
        float fill = 0;
        while (true)
        {
            fill += 1 * Time.deltaTime;
            fillImage.fillAmount = Mathf.Min(fill, 1);

            if (fill >= 1)
                break;

            yield return null;
        }

        // ======= Alpha ======= //
        float alpha = GetAlpha(fillImage);
        while (true)
        {
            alpha -= 0.5f * Time.deltaTime;
            SetAlpha(fillImage, Mathf.Max(alpha, 0));

            if (alpha <= 0)
                break;

            yield return null;
        }

        suggestText.gameObject.SetActive(true);
        answerText.gameObject.SetActive(true);
    }

    private AnswerType GetSuggestedAnswer(Question question)
    {
        bool pickCorrect = ShouldPickCorrect(question.difficultyType);
        question.AvailableAnswers.Shuffle();

        if (pickCorrect)
        {
            for (int i = 0; i < question.AvailableAnswers.Count; i++)
            {
                if (question.AvailableAnswers[i] == question.CorrectAnswer)
                {
                    return question.AvailableAnswers[i];
                }
            }
        }
        else
        {
            for (int i = 0; i < question.AvailableAnswers.Count; i++)
            {
                if (question.AvailableAnswers[i] != question.CorrectAnswer)
                {
                    return question.AvailableAnswers[i];
                }
            }
        }
        
        return question.AvailableAnswers[0]; // Should not be here.
    }
}
