using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Gameplay : MonoBehaviour
{
    private GameState gameState;

    void Awake()
    {
        gameState = GetComponent<GameState>();
    }

	void Start ()
    {
        if (Setup.GetQuiz().IsValid())
        {
            gameState.Init(Setup.GetQuiz());
            gameState.StartGame();
        }
        else
        {
            Debug.Log("Unable to start. Quiz is not valid.");
        }
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
