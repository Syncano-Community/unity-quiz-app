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
            fillImage.fillAmount = value / 100.0f;
            percentageText.text = ((int)value).ToString() + "%";

            if (value >= percent)
                break;

            value = Mathf.Min(100, value + (40f * Time.deltaTime));
            yield return null;
        }
    }
}
