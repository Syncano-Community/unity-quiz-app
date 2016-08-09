using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudiencePanel : LifelinePanelBase
{
    [SerializeField]
    private ChartBarView[] chartBars = new ChartBarView[4];

    public override void Show (Question question)
    {
        base.Show (question);
        chartBars[0].SetPercentage(7);
        chartBars[1].SetPercentage(32);
        chartBars[2].SetPercentage(55);
        chartBars[3].SetPercentage(6);
    }
}
