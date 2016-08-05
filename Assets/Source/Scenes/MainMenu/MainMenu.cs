using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingScreen;
    private SyncanoClient syncano; 

	void Start ()
    {
        loadingScreen.SetActive(false);
        syncano = SyncanoClient.Instance;
		syncano.Init(Constant.API_KEY, Constant.INSTANCE_NAME);
	}

    /* ui event */ public void OnPlayClick()
    {
        ShowLoadingScreen();
        DownloadQuestions();
    }

    /* ui event */ public void OnAddQuestionClick()
    {

    }

    /* ui event */ public void OnModerateClick()
    {

    }

    private void ShowLoadingScreen()
    {
        loadingScreen.SetActive(true);
    }

    private void DownloadQuestions()
    {
        StartCoroutine(syncano.CallScriptEndpoint("d019a1036c7ec1348713de2770385b728f050ed1", "get_questions", OnQuestionsDownloaded));
    }

    private void OnQuestionsDownloaded(Response response)
    {
        Debug.LogWarning("Add error string and response code to Response.");
        //if (response.error != null)
		Quiz quiz = Quiz.FromJson(response.stdout);
        StartGame(quiz);
    }

    private void StartGame(Quiz quiz)
    {
        Setup.SetQuiz(quiz);
        SceneManager.LoadScene(Constant.SCENE_GAMEPLAY);
    }
}
