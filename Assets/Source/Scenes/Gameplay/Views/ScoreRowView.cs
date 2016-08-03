using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreRowView : MonoBehaviour
{
    [SerializeField]
    private Text indexText;

    [SerializeField]
    private Image separatorImage;

    [SerializeField]
    private Image highlightImage;

    [SerializeField]
    private Text labelText;

    private ScoreRow scoreRow;

    public void Init(ScoreRow scoreRow, int rowIndex)
    {
        this.scoreRow = scoreRow;
        indexText.text = (rowIndex + 1).ToString();
        separatorImage.enabled = true;
        labelText.text = scoreRow.label;
        SetHighlighted(false);
    }

    public void SetHighlighted(bool highlighted)
    {
        highlightImage.enabled = highlighted;

        if (highlighted)
        {
            indexText.color = Colors.CONTRAST_COLOR;
            labelText.color = Colors.CONTRAST_COLOR;
        }
        else
        {
            if (scoreRow.isGuaranteed)
            {
                indexText.color = Colors.GUARANTEED_LABEL_COLOR;
                labelText.color = Colors.GUARANTEED_LABEL_COLOR;
            }
            else
            {
                indexText.color = Colors.MAIN_COLOR;
                labelText.color = Colors.MAIN_COLOR;
            }
        }
    }
}
