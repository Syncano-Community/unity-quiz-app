using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using System;
using Syncano;
using Syncano.Data;

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

	void Awake()
	{
		if (!FB.IsInitialized) {
			// Initialize the Facebook SDK
			FB.Init(InitCallback);
		} else {
			// Already initialized, signal an app activation App Event
			FB.ActivateApp();
		}
	}

	private void InitCallback ()
	{
		if (FB.IsInitialized) {
			
			FB.ActivateApp();
		} else {
			Debug.Log("Failed to Initialize the Facebook SDK");
		}
	}

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

		var perms = new List<string>(){"publish_actions"};
		FB.LogInWithPublishPermissions (perms, AuthCallback);	
    }

	private void ShareCallback (IShareResult result) {
		if (result.Cancelled || !string.IsNullOrEmpty(result.Error)) {
			Debug.Log("ShareLink Error: "+result.Error);
		} else if (!string.IsNullOrEmpty(result.PostId)) {
			// Print post identifier of the shared content
			Debug.Log(result.PostId);
		} else {
			// Share succeeded without postID
			Debug.Log("ShareLink success!");
		}
	}

    /* ui event */ public void OnMenuClick()
    {
    	SceneManager.LoadScene(Constant.SCENE_MENU);
    }

    /* ui event */ public void OnTryAgainClick()
    {
        DownloadQuestions();
    }

	private void AuthCallback(ILoginResult result)
	{
		if (FB.IsLoggedIn)
		{
			// AccessToken class will have session details
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
			// Print current access token's User ID
			Debug.Log(aToken.UserId);
			// Print current access token's granted permissions
			foreach (string perm in aToken.Permissions)
			{
				Debug.Log(perm);
			}
		}
		else
		{
			Debug.Log("User cancelled login");
		}
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
        SyncanoClient.Instance.Please().CallScriptEndpoint("d019a1036c7ec1348713de2770385b728f050ed1", "get_questions", OnQuestionsDownloaded);
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
