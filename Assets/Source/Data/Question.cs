using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Question
{
    /// <summary>
	/// Gets or sets the quesion's ID.
	/// </summary>
	/// <value>The Id.</value>
	public string Id { get; set; }

	/// <summary>
	/// Gets or sets the quesion's text.
	/// </summary>
	/// <value>The text.</value>
	public string Text { get; set; }

    /// <summary>
    /// Gets or sets the question difficulty.
    /// </summary>
    /// <value>The difficulty.</value>
    public QuestionLevel Difficulty { get; set; }

    /// <summary>
    /// Gets or sets the answers.
    /// </summary>
    /// <value>The answers.</value>
    public List<string> Answers { get; set; }

    /// <summary>
    /// Gets or sets the index of the correct answer in Answers.
    /// </summary>
    /// <value>The index of the correct answer.</value>
    public int CorrectAnswerIndex { get; set; }

    /// <summary>
    /// Modify answer order and correct index.
    /// </summary>
    public void Shuffle()
    {
        string correct = Answers[CorrectAnswerIndex];
        Answers.Shuffle();
        CorrectAnswerIndex = Answers.IndexOf(correct);
    }
}