using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ResultPanel : MonoBehaviour
{
    [SerializeField]
    private Text rewardText;

    [SerializeField]
    private Image foxImage;

    [SerializeField]
    private Sprite lowRewardImage;

    [SerializeField]
    private Sprite mediumRewardImage;

    [SerializeField]
    private Sprite highRewardImage;

    [SerializeField]
    private Sprite millionRewardImage;

    [SerializeField]
    private GameObject loadingScreen;

    private ScoreRow reward;

    public void ShowReward(ScoreRow reward)
    {
        this.reward = reward;
        gameObject.SetActive(true);

        if (reward.value <= 1000)
        {
            rewardText.text = string.Format(Constant.STR_REWARD, reward.label, Constant.STR_REWARD_LOW);
            foxImage.sprite = lowRewardImage;
        }
        else if (reward.value <= 32000)
        {
            rewardText.text = string.Format(Constant.STR_REWARD, reward.label, Constant.STR_REWARD_MEDIUM);
            foxImage.sprite = mediumRewardImage;
        }
        else if (reward.value < 1000000)
        {
            rewardText.text = string.Format(Constant.STR_REWARD, reward.label, Constant.STR_REWARD_HIGH);
            foxImage.sprite = highRewardImage;
        }
        else if (reward.value == 1000000)
        {
            rewardText.text = string.Format(Constant.STR_REWARD, reward.label, Constant.STR_REWARD_MILLION);
            foxImage.sprite = millionRewardImage;
        }
    }

    /* ui event */ public void OnShareClick()
    {
        Debug.Log("Your reward is: " + reward.label + " (Raw value: " + reward.value + ")");
        // Share your "salary" on FB.
    }

    /* ui event */ public void OnMenuClick()
    {
    	SceneManager.LoadScene(Constant.SCENE_MENU);
    }

    /* ui event */ public void OnTryAgainClick()
    {
        DownloadQuestions();
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
        ShowLoadingScreen();
        Syncano.Instance.Please().CallScriptEndpoint("d019a1036c7ec1348713de2770385b728f050ed1", "get_questions", OnQuestionsDownloaded);
    }

    private void OnQuestionsDownloaded(ScriptEndpoint response)
    {
        HideLoadingScreen();

        if (response != null && response.result != null && string.IsNullOrEmpty(response.webError))
        {
            Quiz quiz = Quiz.FromJson(response.stdout);
            if (quiz.IsValid())
            {
                StartGame(quiz);
            }
        }
    }

    private void StartGame(Quiz quiz)
    {
        Setup.SetQuiz(quiz);
        SceneManager.LoadScene(Constant.SCENE_GAMEPLAY);
    }
}
