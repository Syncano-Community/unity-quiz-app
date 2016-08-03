using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ScorePanel : Singleton<ScorePanel>
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

    private Action<Lifeline> onLifelineSelected;

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

        fiftyFiftyButton.onClick.AddListener(()=> LifelineClicked(Lifeline.FIFTY_FIFTY));
        phoneButton.onClick.AddListener(()=> LifelineClicked(Lifeline.PHONE));
        audienceButton.onClick.AddListener(()=> LifelineClicked(Lifeline.AUDIENCE));
	}

    public void SetOnLifelineSelectedListener(Action<Lifeline> action)
    {
        onLifelineSelected = action;
    }

    public void SetLifelineInteractable(Lifeline lifeline, bool interactable)
    {
        Button button = GetLifelineButton(lifeline);
        button.interactable = interactable;
    }

    public IEnumerator ShowOff()
    {
        for (int i = 0; i < ScoreTable.GetRowCount(); i++)
        {
            rows[i].SetHighlighted(true);
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

    private void SetScale(Transform transform, float scale)
    {
        transform.localScale = new Vector3(scale, scale, scale);
    }

    private void LifelineClicked(Lifeline lifeline)
    {
        if (onLifelineSelected != null)
            onLifelineSelected.Invoke(lifeline);
    }

    private Button GetLifelineButton(Lifeline lifeline)
    {
        switch (lifeline) {
        case Lifeline.AUDIENCE:
            return audienceButton;
        case Lifeline.FIFTY_FIFTY:
            return fiftyFiftyButton;
        case Lifeline.PHONE:
            return phoneButton;
        default:
            return null;
        }
    }
}
