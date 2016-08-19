using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ScorePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject scoreRowViewPrefab;

    [SerializeField]
    private Transform rowContainer;

    [SerializeField]
    private Button fiftyFiftyButton;

    [SerializeField]
    private Button phoneButton;

    [SerializeField]
    private Button audienceButton;

    private ScoreRowView[] rows;

    private Action<LifelineType> onLifelineSelected;

	void Start ()
    {
        rows = new ScoreRowView[ScoreTable.GetRowCount()];

        // Create table rows.
        for (int i = ScoreTable.GetRowCount() - 1; i >= 0; i--)
        {
            GameObject go = (GameObject) Instantiate(scoreRowViewPrefab, rowContainer, false);
            ScoreRowView row = go.GetComponent<ScoreRowView>();
            row.Init(ScoreTable.GetRow(i), i);
            rows[i] = row;
        }

        // Assign on click listeners.
        fiftyFiftyButton.onClick.AddListener(()=> LifelineClicked(LifelineType.FIFTY_FIFTY));
        phoneButton.onClick.AddListener(()=> LifelineClicked(LifelineType.PHONE));
        audienceButton.onClick.AddListener(()=> LifelineClicked(LifelineType.AUDIENCE));
	}

    /// <summary>
    /// Sets the on lifeline selected listener.
    /// Rised when lifeline is clicked.
    /// </summary>
    public void SetOnLifelineSelectedListener(Action<LifelineType> action)
    {
        onLifelineSelected = action;
    }

    /// <summary>
    /// Sets the lifeline interactable.
    /// </summary>
    public void SetLifelineInteractable(LifelineType lifeline, bool interactable)
    {
        Button button = GetLifelineButton(lifeline);
        button.interactable = interactable;
    }

    /// <summary>
    /// Animation to show score panel elements.
    /// </summary>
    public IEnumerator ShowOff()
    {
        for (int i = 0; i < ScoreTable.GetRowCount(); i++)
        {
            rows[i].SetHighlighted(true);
            rows[i].ShowAnsweredMarker(true);
            yield return new WaitForSeconds(0.15f);
            rows[i].SetHighlighted(false);
        }
        yield return new WaitForSeconds(0.35f);

        SetScale(fiftyFiftyButton.transform, 1.2f);
        yield return new WaitForSeconds(0.35f);
        SetScale(fiftyFiftyButton.transform, 1.0f);

        SetScale(phoneButton.transform, 1.2f);
        yield return new WaitForSeconds(0.35f);
        SetScale(phoneButton.transform, 1.0f);

        SetScale(audienceButton.transform, 1.2f);
        yield return new WaitForSeconds(0.35f);
        SetScale(audienceButton.transform, 1.0f);
    }

    /// <summary>
    /// Higlights higlights element with given index and all dots below.
    /// </summary>
    public void SetLevel(int questionIndex)
    {
        for (int i = 0; i < ScoreTable.GetRowCount(); i++)
        {
            rows[i].SetHighlighted(i == questionIndex);
            rows[i].ShowAnsweredMarker(i <= questionIndex);
        }
    }

    /// <summary>
    /// Utility method allowing change transform scale using one variable.
    /// </summary>
    private void SetScale(Transform transform, float scale)
    {
        transform.localScale = new Vector3(scale, scale, scale);
    }

    /// <summary>
    /// Rised when lifeline is clicked.
    /// </summary>
    private void LifelineClicked(LifelineType lifeline)
    {
        if (onLifelineSelected != null)
            onLifelineSelected.Invoke(lifeline);
    }

    /// <summary>
    /// Gets the lifeline button.
    /// </summary>
    private Button GetLifelineButton(LifelineType lifeline)
    {
        switch (lifeline) {
        case LifelineType.AUDIENCE:
            return audienceButton;
        case LifelineType.FIFTY_FIFTY:
            return fiftyFiftyButton;
        case LifelineType.PHONE:
            return phoneButton;
        default:
            return null;
        }
    }
}
