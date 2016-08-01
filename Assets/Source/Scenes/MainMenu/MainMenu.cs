using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingScreen;

	void Start ()
    {
        loadingScreen.SetActive(false);
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
        StartCoroutine(MockDownload());
    }

    private IEnumerator MockDownload()
    {
        yield return new WaitForSeconds(2);
        OnQuestionsDownloaded(null);
    }

    private void OnQuestionsDownloaded(string json)
    {
        Quiz quiz = new Quiz();
        for (int i = 0; i < Quiz.QUESTION_COUNT; i++) {
            Question question = new Question();
            question.Text = "Question " + (i + 1);
            quiz.SetQuestion(i, question);
        }
        StartGame(quiz);
    }

    private void StartGame(Quiz quiz)
    {
        Setup.quiz = quiz;
        SceneManager.LoadScene(Constant.SCENE_GAMEPLAY);
    }
}
