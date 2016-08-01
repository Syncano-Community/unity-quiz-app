using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Gameplay : MonoBehaviour
{
	void Start ()
    {
	    
	}

    /* ui event */ public void OnExitClick()
    {
        GoToMainMenu();
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene(Constant.SCENE_MENU);
    }
}
