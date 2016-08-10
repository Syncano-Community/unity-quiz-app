using UnityEngine;
using System.Collections;

public class QuestionManagerUI : Singleton<QuestionManagerUI>
{
    public QuestionPanel QuestionPanel { get; private set; }
    public FoxPanel FoxPanel { get; private set; }

    void Awake ()
    {
        QuestionPanel = transform.GetComponentInChildren<QuestionPanel>(true);
        FoxPanel = transform.GetComponentInChildren<FoxPanel>(true);
    }
}
