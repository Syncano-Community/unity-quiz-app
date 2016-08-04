using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ResultPanel : MonoBehaviour
{
    public void ShowReward()
    {
        gameObject.SetActive(true);
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
