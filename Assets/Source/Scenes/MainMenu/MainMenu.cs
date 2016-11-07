using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Syncano;
using Newtonsoft.Json;
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
		syncano.Please().RunScriptEndpointUrl(Constant.SCRIPT_ENDPOINT_GET_QUESTIONS_URL, OnQuestionsDownloaded);
    }

    private void OnQuestionsDownloaded(ScriptEndpoint response)
    {
        if (response.IsSuccess)
        {
			Quiz quiz = new Quiz();
			quiz.questions = JsonConvert.DeserializeObject<List<Question>>(response.stdout);

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
