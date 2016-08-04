using UnityEngine;
using System.Collections;

public class GameUI : Singleton<GameUI>
{
    public QuestionPanel QuestionPanel { get; private set; }
    public ScorePanel ScorePanel { get; private set; }
    public ResultPanel ResultPanel { get; private set; }

    void Awake ()
    {
        QuestionPanel = transform.GetComponentInChildren<QuestionPanel>(true);
        ScorePanel = transform.GetComponentInChildren<ScorePanel>(true);
        ResultPanel = transform.GetComponentInChildren<ResultPanel>(true);
    }
}
