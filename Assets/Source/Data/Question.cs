using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Question
{
    /// <summary>
	/// Quesion's ID.
	/// </summary>
	/// <value>The Id.</value>
    [SerializeField]
    private long id;

	/// <summary>
	/// Quesion's text.
	/// </summary>
	/// <value>The text.</value>
    [SerializeField]
    private string text;

    /// <summary>
    /// Question difficulty.
    /// </summary>
    /// <value>The difficulty.</value>
    [SerializeField]
    private QuestionLevel difficulty;

    /// <summary>
    /// Collection of answer strings.
    /// </summary>
    /// <value>The answers.</value>
    [SerializeField]
    private List<string> answers;

    /// <summary>
    /// The index of the correct answer.
    /// </summary>
    /// <value>The index of the correct answer.</value>
    [SerializeField]
    private int correctAnswerIndex;

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

    public QuestionLevel Difficulty
    {
        get { return difficulty; }
        set { difficulty = value; }
    }

    public List<string> Answers
    {
        get { return answers; }
        set { answers = value; }
    }

    public int CorrectAnswerIndex
    {
        get { return correctAnswerIndex; }
        set { correctAnswerIndex = value; }
    }

    /// <summary>
    /// Modify answer order and correct index.
    /// </summary>
    public void Shuffle()
    {
        string correct = answers[correctAnswerIndex];
        answers.Shuffle();
        CorrectAnswerIndex = answers.IndexOf(correct);
    }
}