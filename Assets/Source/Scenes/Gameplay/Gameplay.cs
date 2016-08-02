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
        gameState.Init(Setup.GetQuiz());
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
