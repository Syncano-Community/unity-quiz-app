using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FiftyFiftyPanel : LifelinePanelBase
{
    [SerializeField]
    private Image fillImage;

    public override void Show (Question question)
    {
        base.Show (question);
        fillImage.fillAmount = 0;
        StartCoroutine(ShowLifeline());
    }

    public IEnumerator ShowLifeline()
    {
        float fill = 0;

        while (true)
        {
            fillImage.fillAmount = fill;
            fill += 1 * Time.deltaTime;

            if (fill >= 1)
            {
                fill = 1;
                break;
            }
            yield return null;
        }

        fillImage.fillAmount = fill;
        OnLifelineShown();
    }

    private void OnLifelineShown()
    {
        //Debug.Log("Shown");
        // remove two options
    }
}