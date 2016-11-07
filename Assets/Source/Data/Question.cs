using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Syncano;
using Newtonsoft.Json;

public class Question : SyncanoObject
{
	/// <summary>
	/// Quesion's text.
	/// </summary>
	[JsonProperty("text")]
	public string Text { get; set; }

    /// <summary>
    /// Question difficulty.
    /// </summary>
	[JsonProperty("difficultyType")]
	public DifficultyType DifficultyType { get; set;}

    /// <summary>
    /// Collection of answer strings.
    /// </summary>
	[JsonProperty("answers")]
	public List<string> Answers { get; set;}

    /// <summary>
    /// Determines if new question was moderated.
    /// </summary>
	[JsonProperty("isModerated")]
    public bool IsModerated;

    /// <summary>
    /// The index of the correct answer.
    /// Server always act like this value is 0. We use it only for shuffle.
    /// </summary>
    private AnswerType correctAnswer = AnswerType.A;

    /// <summary>
    /// List of available answers.
    /// This is information for lifelines.
    /// </summary>
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
        if (string.IsNullOrEmpty(Text))
        {
			Debug.LogWarning("Missing question text - id: " + Id);
            return false;
        }

        if (Answers == null || Answers.Count != 4)
        {
			Debug.LogWarning("Not eough answers - id: " + Id);   
            return false;
        }

        foreach (var item in Answers)
        {
            if (string.IsNullOrEmpty(item))
            {
				Debug.LogWarning("Missing answer - id: " + Id);
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
        string correct = Answers[(int)correctAnswer];
        Answers.Shuffle();
        correctAnswer = (AnswerType)Answers.IndexOf(correct);
    }
}