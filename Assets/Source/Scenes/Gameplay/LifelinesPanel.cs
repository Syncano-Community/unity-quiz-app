using UnityEngine;
using System.Collections;

public class LifelinesPanel : MonoBehaviour
{
    [SerializeField]
    private FiftyFiftyPanel fiftyFiftyPanel;

    [SerializeField]
    private PhonePanel phonePanel;

    [SerializeField]
    private AudiencePanel audiencePanel;

    private LifelinePanelBase activePanel;

    public void ShowLifeline(LifelineType lifeline, Question question)
    {
        switch (lifeline) {
        case LifelineType.AUDIENCE:
            Show(audiencePanel, question);
            break;
        case LifelineType.FIFTY_FIFTY:
            Show(fiftyFiftyPanel, question);
            break;
        case LifelineType.PHONE:
            Show(phonePanel, question);
            break;
        }
    }

    private void Show(LifelinePanelBase panel, Question question)
    {
        HideLifelines();
        gameObject.SetActive(true);
        activePanel = panel;
        panel.Show(question);
    }

    public void HideLifelines()
    {
        if (activePanel != null)
        {
            gameObject.SetActive(false);
            activePanel.Hide();
        }
    }
}
