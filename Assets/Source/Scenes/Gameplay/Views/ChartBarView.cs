using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChartBarView : MonoBehaviour
{
    [SerializeField]
    private Text percentageText;

    [SerializeField]
    private Image fillImage;

    /// <summary>
    /// Sets the percentage (0 - 100).
    /// </summary>
    public void SetPercentage(int percent)
    {
        StartCoroutine(AnimatePercentage(percent));
    }

    public IEnumerator AnimatePercentage(int percent)
    {
        float value = 0;

        while (true)
        {
            value += 40f * Time.deltaTime;
            fillImage.fillAmount = (float)value / 100.0f;
            percentageText.text = ((int)value).ToString() + "%";

            if (value >= percent)
                break;

            yield return null;
        }
    }
}
