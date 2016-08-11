using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class QuestionFormUI : Singleton<QuestionFormUI>
{
    public QuestionPanel QuestionPanel { get; private set; }
    public FoxPanel FoxPanel { get; private set; }

    void Awake ()
    {
        QuestionPanel = transform.GetComponentInChildren<QuestionPanel>(true);
        FoxPanel = transform.GetComponentInChildren<FoxPanel>(true);
    }

    /* ui event */ public void OnExitClick()
    {
        SceneManager.LoadScene(Constant.SCENE_MENU);
    }
}
