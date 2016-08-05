using UnityEngine;
using System.Collections;

public class Quiz
{
    public const int QUESTION_COUNT = 15;

    /// <summary>
    /// The questions.
    /// </summary>
    public Question[] questions = new Question[QUESTION_COUNT];

    public Question GetQuestion(int index)
    {
        if (questions != null && index >= 0 && index < questions.Length)
            return questions[index];

        return null;
    }

    public void SetQuestion(int index, Question question)
    {
        if (index >= 0 && index < QUESTION_COUNT)
        {
            questions[index] = question;
        }
        else
        {
            throw new UnityException("Wrong index.");
        }
    }

    public int GetQuestionCount()
    {
        if (questions == null)
            return 0;
                
        return questions.Length;
    }

    public bool IsValid()
    {
        if (questions == null || questions.Length != QUESTION_COUNT)
            return false;

        foreach (var item in questions) {
            if (item == null || item.IsValid() == false)
                return false;
        }

        return true;
    }

	public static Quiz FromJson(string json)
	{
		if (string.IsNullOrEmpty(json))
			return default(Quiz); // Return null for generic.

		Quiz instance = new Quiz();
		instance.questions = JsonHelper.GetJsonArray<Question>(json);
		return instance;
	}
}
