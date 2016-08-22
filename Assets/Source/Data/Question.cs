using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Syncano;

[Serializable]
public class Question : SyncanoObject<Question>
{
	/// <summary>
	/// Quesion's text.
	/// </summary>
    public string text;

    /// <summary>
    /// Question difficulty.
    /// </summary>
	public DifficultyType difficultyType;

    /// <summary>
    /// Collection of answer strings.
    /// </summary>
	public List<string> answers;

    /// <summary>
    /// Determines if new question was moderated.
    /// </summary>
    public bool isModerated;

    /// <summary>
    /// The index of the correct answer.
    /// Server always act like this value is 0. We use it only for shuffle.
    /// </summary>
    [NonSerialized]
    private AnswerType correctAnswer = AnswerType.A;

    /// <summary>
    /// List of available answers.
    /// This is information for lifelines.
    /// </summary>
    [NonSerialized]
    private List<AnswerType> availableAnswers = new List<AnswerType> { AnswerType.A, AnswerType.B, AnswerType.C, AnswerType.D };

    public AnswerType CorrectAnswer
    {
        get { return correctAnswer; }
        set { correctAnswer = value; }
    }

    public List<AnswerType> AvailableAnswers
    {
        get { return availableAnswers; }
        set { availableAnswers = value; }
    }

    public bool IsValid()
    {
        if (string.IsNullOrEmpty(text))
        {
            Debug.LogWarning("Missing question text - id: " + id);
            return false;
        }

        if (answers == null || answers.Count != 4)
        {
            Debug.LogWarning("Not eough answers - id: " + id);   
            return false;
        }

        foreach (var item in answers)
        {
            if (string.IsNullOrEmpty(item))
            {
                Debug.LogWarning("Missing answer - id: " + id);
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Modify answer order and correct index.
    /// </summary>
    public void Shuffle()
    {
        string correct = answers[(int)correctAnswer];
        answers.Shuffle();
        correctAnswer = (AnswerType)answers.IndexOf(correct);
    }
}