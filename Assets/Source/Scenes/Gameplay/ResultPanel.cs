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
			Debug.Log("here1");
			FB.Init(InitCallback);
		} else {
			Debug.Log("here2");
			// Already initialized, signal an app activation App Event
			FB.ActivateApp();
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
		FB.LogInWithPublishPermissions(new List<string>(){"publish_actions"}, AuthCallback);	
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
		SyncanoClient.Instance.Please().CallScriptEndpoint(Constant.SCRIPT_ENDPOINT_GET_QUESTIONS_ID, Constant.SCRIPT_ENDPOINT_GET_QUESTIONS_NAME, OnQuestionsDownloaded);
    }

    private void OnQuestionsDownloaded(ScriptEndpoint response)
    {
        HideLoadingScreen();

        if (response.IsSuccess)
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


	#region facebook
	/// <summary>
	/// The callback method when initializing facebook sdk.
	/// </summary>
	private void InitCallback()
	{
		if (FB.IsInitialized) {

			FB.ActivateApp();
		} else {
			Debug.LogError("Failed to Initialize the Facebook SDK");
		}
	}

	/// <summary>
	/// The callback method when authenticating user.
	/// </summary>
	/// <param name="result">Result.</param>
	private void AuthCallback(ILoginResult result)
	{
		if(FB.IsLoggedIn)
		{
			StartCoroutine(TakeScreenshot());
		}
		else
		{
			Debug.Log("User cancelled login");
		}
	}

	private IEnumerator TakeScreenshot() 
	{
		yield return new WaitForEndOfFrame();

		var width = Screen.width;
		var height = Screen.height;
		var tex = new Texture2D(width, height, TextureFormat.RGB24, false);

		tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
		tex.Apply();
		byte[] screenshot = tex.EncodeToPNG();
	
		var wwwForm = new WWWForm();
		wwwForm.AddBinaryData("image", screenshot, "Screenshot.png");
		wwwForm.AddField("name", "According to Syncano Developer Quiz my salary should be " + reward.value + "$ !! \n" + "Read more how to create a similar application on https://www.syncano.io");

		FB.API("me/photos", Facebook.Unity.HttpMethod.POST,null, wwwForm);
	}
	#endregion facebook
}
