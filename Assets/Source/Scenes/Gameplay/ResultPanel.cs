using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ResultPanel : MonoBehaviour
{
    [SerializeField]
    private Text rewardText;

    private ScoreRow reward;

    public void ShowReward(ScoreRow reward)
    {
        this.reward = reward;
        gameObject.SetActive(true);
        rewardText.text = string.Format(Constant.STR_REWARD, reward.label);
    }

    /* ui event */ public void OnShareClick()
    {
        // Share your "salary" on FB.
    }

    /* ui event */ public void OnMenuClick()
    {
    	SceneManager.LoadScene(Constant.SCENE_MENU);
    }
}
