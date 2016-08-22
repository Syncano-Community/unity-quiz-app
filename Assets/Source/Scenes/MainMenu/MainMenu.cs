using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Syncano;
using Syncano.Data;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingScreen;
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

    /* ui event */ public void OnAdministrateClick()
    {
        SceneManager.LoadScene(Constant.SCENE_QUESTION_FORM);
    }

    /* ui event */ public void OnModerateClick()
    {

    }

    private void ShowLoadingScreen()
    {
        loadingScreen.SetActive(true);
    }

    private void HideLoadingScreen()
    {
        loadingScreen.SetActive(false);
    }

    private void DownloadQuestions()
    {
        syncano.Please().CallScriptEndpoint("d019a1036c7ec1348713de2770385b728f050ed1", "get_questions", OnQuestionsDownloaded);
    }

    private void OnQuestionsDownloaded(ScriptEndpoint response)
    {
        if (response.IsSuccess)
        {
            Quiz quiz = Quiz.FromJson(response.stdout);
            if (quiz.IsValid())
            {
                StartGame(quiz);
            }
            else
            {
                HandleNotValidQuiz(quiz);
            }
        }
        else
        {
            HandleError(response);
        }
    }

    private void HandleError(ScriptEndpoint response)
    {
        Debug.Log("Error: " + response.webError);
        HideLoadingScreen();
    }

    private void HandleNotValidQuiz(Quiz quiz)
    {
        Debug.Log("Quiz not valid!");
        HideLoadingScreen();
    }

    private void StartGame(Quiz quiz)
    {
        Setup.SetQuiz(quiz);
        SceneManager.LoadScene(Constant.SCENE_GAMEPLAY);
    }
}
