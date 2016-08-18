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
        // Share your "salary" on FB.
    }

    /* ui event */ public void OnMenuClick()
    {
    	SceneManager.LoadScene(Constant.SCENE_MENU);
    }
}
