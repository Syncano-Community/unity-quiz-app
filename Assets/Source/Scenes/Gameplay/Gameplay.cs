using UnityEngine;
using System.Collections;

public class Gameplay : Singleton<Gameplay>
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
        gameState.FinishGame(true);
    }
}
