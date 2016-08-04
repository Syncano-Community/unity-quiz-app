using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Question
{
    /// <summary>
	/// Quesion's ID.
	/// </summary>
    [SerializeField]
    private long id;

	/// <summary>
	/// Quesion's text.
	/// </summary>
    [SerializeField]
    private string text;

    /// <summary>
    /// Question difficulty.
    /// </summary>
    [SerializeField]
    private DifficultyType difficulty;

    /// <summary>
    /// Collection of answer strings.
    /// </summary>
    [SerializeField]
    private List<string> answers;

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

    public long Id
    {
        get { return id; }
        set { id = value; }
    }

    public string Text
    {
        get { return text; }
        set { text = value; }
    }

    public DifficultyType Difficulty
    {
        get { return difficulty; }
        set { difficulty = value; }
    }

    public List<string> Answers
    {
        get { return answers; }
        set { answers = value; }
    }

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