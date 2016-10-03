using UnityEngine;
using System.Collections;

public class Constant
{
    public const string SCENE_GAMEPLAY = "Gameplay";
    public const string SCENE_MENU = "MainMenu";
    public const string SCENE_QUESTION_FORM= "QuestionForm";

    public const string STR_REWARD = "Your estimated salary: <b>{0}</b>\n{1}";
    public const string STR_REWARD_LOW = "You could do better...";
    public const string STR_REWARD_MEDIUM = "Looks like you have some skills!";
    public const string STR_REWARD_HIGH = "Bravo, the million is in reach!";
    public const string STR_REWARD_MILLION = "Congrats! Your dev knowledge is amazing.";

	#region variables to set
	public const string API_KEY = "7aa2f6396632efd9e93c02cf6aaec401f77a481b";
	public const string INSTANCE_NAME = "unity-quiz-app";
	public const string SCRIPT_ENDPOINT_GET_QUESTIONS_URL = "https://api.syncano.io/v1.1/instances/unity-quiz-app/endpoints/scripts/p/d019a1036c7ec1348713de2770385b728f050ed1/get_questions/";
	public const string SCRIPT_ENDPOINT_GET_QUESTION_TO_MODERATE_URL = "https://api.syncano.io/v1.1/instances/unity-quiz-app/endpoints/scripts/p/6349c3ec1208c0be5ade53b154427d4eb5cb1628/get_question_to_moderate/";
	#endregion variables to set
}
