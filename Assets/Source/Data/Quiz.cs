using UnityEngine;
using System.Collections;
using Syncano.Data;

/// <summary>
/// Quiz represents group of questions.
/// </summary>
public class Quiz
{
    public const int QUESTION_COUNT = 15;

    /// <summary>
    /// The questions.
    /// </summary>
    public Question[] questions = new Question[QUESTION_COUNT];

    /// <summary>
    /// Gets the question by index.
    /// </summary>
    public Question GetQuestion(int index)
    {
        if (questions != null && index >= 0 && index < questions.Length)
            return questions[index];

        return null;
    }

    /// <summary>
    /// Sets the question for given index.
    /// </summary>
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

    /// <summary>
    /// Gets the question count.
    /// </summary>
    public int GetQuestionCount()
    {
        if (questions == null)
            return 0;
                
        return questions.Length;
    }

    /// <summary>
    /// Determines whether this quiz and all its questions are valid.
    /// </summary>
    public bool IsValid()
    {
        if (questions == null || questions.Length != QUESTION_COUNT)
        {
            Debug.LogWarning("Wrong number of questions.");
            return false;
        }

        foreach (var item in questions) {
            if (item == null || item.IsValid() == false)
            {
                Debug.LogWarning("Invalid question (id: " + item.id + ")");
                return false;
            }
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
