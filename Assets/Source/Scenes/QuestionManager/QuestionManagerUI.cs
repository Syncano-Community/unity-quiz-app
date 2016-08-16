using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class QuestionManagerUI : Singleton<QuestionManagerUI>
{
    public QuestionPanel QuestionPanel { get; private set; }
    public FoxPanel FoxPanel { get; private set; }
    public MenuPanel MenuPanel { get; private set; }
    public SubmitPanel SubmitPanel { get; private set; }
    public ModeratePanel ModeratePanel { get; private set; }
    public FormPanel FormPanel { get; private set; }
    public LoadingPanel LoadingPanel { get; private set; }
    public SummaryPanel SummaryPanel { get; private set; }

    void Awake ()
    {
        QuestionPanel = transform.GetComponentInChildren<QuestionPanel>(true);
        FoxPanel = transform.GetComponentInChildren<FoxPanel>(true);
        MenuPanel = transform.GetComponentInChildren<MenuPanel>(true);
        SubmitPanel = transform.GetComponentInChildren<SubmitPanel>(true);
        ModeratePanel = transform.GetComponentInChildren<ModeratePanel>(true);
        FormPanel = transform.GetComponentInChildren<FormPanel>(true);
        LoadingPanel = transform.GetComponentInChildren<LoadingPanel>(true);
        SummaryPanel = transform.GetComponentInChildren<SummaryPanel>(true);
    }

    /* ui event */ public void OnExitClick()
    {
        SceneManager.LoadScene(Constant.SCENE_MENU);
    }
}
