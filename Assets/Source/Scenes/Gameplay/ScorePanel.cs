using UnityEngine;
using System.Collections;

public class ScorePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject scoreRowViewPrefab;

    [SerializeField]
    private Transform rowContainer;

    private ScoreRowView[] rows;

	void Start ()
    {
        rows = new ScoreRowView[ScoreTable.GetRowCount()];

        for (int i = ScoreTable.GetRowCount() - 1; i >= 0; i--)
        {
            GameObject go = (GameObject) Instantiate(scoreRowViewPrefab, rowContainer, false);
            ScoreRowView row = go.GetComponent<ScoreRowView>();
            row.Init(ScoreTable.GetRow(i), i);
            rows[i] = row;
        }

        rows[0].SetHighlighted(true);
	}
}
