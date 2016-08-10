using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AudiencePanel : LifelinePanelBase
{
    [SerializeField]
    private ChartBarView[] chartBars = new ChartBarView[4];

    public override void Show (Question question)
    {
        base.Show (question);
        int[] percentage = GetPercentage(question);
        DisplayPercentage(question, percentage);
    }

    private int[] GetPercentage(Question question)
    {
        int count = 4;
        float totalWeight = 0;
        float[] weights = new float[count];
        bool pickCorrect = ShouldPickCorrect(question.difficultyType);

        for (int i = 0; i < count; i++)
        {
            bool isAvailable = question.AvailableAnswers.Contains((AnswerType)i);
            bool isCorrect = question.CorrectAnswer == (AnswerType)i;

            if (isAvailable == false)
            {
                weights[i] = 0;
            }
            else if (isCorrect && pickCorrect)
            {
                weights[i] = Random.Range(50, 100);
            }
            else
            {
                weights[i] = Random.Range(0, 50);
            }

            totalWeight += weights[i];
        }

        int[] percent = new int[count];
        float roundRatio = 0;

        for (int i = 0; i < count; i++)
        {
            roundRatio += (weights[i] / totalWeight * 100.001f); // Add 0.001f to make sure we gonna have 100 in total at the end.
            percent[i] = (int)roundRatio; // Round
            roundRatio -= percent[i];
        }

        return percent;
    }

    private void DisplayPercentage(Question question, int[] percentage)
    {
        for (int i = 0; i < 4; i++)
        {
            chartBars[i].SetPercentage(percentage[i]);
        }
    }
}
