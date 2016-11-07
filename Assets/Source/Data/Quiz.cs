using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Syncano.Data;
using Newtonsoft.Json;

/// <summary>
/// Quiz represents group of questions.
/// </summary>
public class Quiz
{
    public const int QUESTION_COUNT = 15;

    /// <summary>
    /// The questions.
    /// </summary>
	public List<Question> questions = new List<Question>(QUESTION_COUNT);

    /// <summary>
    /// Gets the question by index.
    /// </summary>
    public Question GetQuestion(int index)
    {
		if (questions != null && index >= 0 && index < questions.Count)
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
                
		return questions.Count;
    }

    /// <summary>
    /// Determines whether this quiz and all its questions are valid.
    /// </summary>
    public bool IsValid()
    {
		if (questions == null || questions.Count != QUESTION_COUNT)
        {
            Debug.LogWarning("Wrong number of questions.");
            return false;
        }

        foreach (var item in questions) {
            if (item == null || item.IsValid() == false)
            {
                Debug.LogWarning("Invalid question (id: " + item.Id + ")");
                return false;
            }
        }

        return true;
    }
}
