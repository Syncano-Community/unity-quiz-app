using UnityEngine;
using System.Collections;

public class Task {
	/// <summary>
	/// Gets or sets the ID.
	/// </summary>
	/// <value>The ID.</value>
	public string ID { get { return id; } set { id = value; } }

	/// <summary>
	/// Gets or sets the correct question ID.
	/// </summary>
	/// <value>The correct question ID.</value>
	public string CorrectQuestionID { get; set; }

	/// <summary>
	/// Gets or sets the question level.
	/// </summary>
	/// <value>The question level.</value>
	public QuestionLevel QuestionLevel { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether this instance is valid.
	/// </summary>
	/// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
	public bool IsValid { get { return isValid; } set { isValid = value; } }

	/// <summary>
	/// Gets or sets the question.
	/// </summary>
	/// <value>The question.</value>
	public Question Question { get { return question; } set { question = value; } }

	/// <summary>
	/// Gets or sets the answers.
	/// </summary>
	/// <value>The answers to this task.</value>
	public Answer[] Answers { get { return answers; } set { answers = value; } }

	/// <summary>
	/// Gets the correct answer.
	/// </summary>
	/// <returns>The correct answer.</returns>
	public Answer GetCorrectAnswer()
	{
		if (answers == null || answers.Length < 4)
		{
			throw new UnityException ("Task with id= " + id + " has no or less than four answers.");
		}

		for (int i = 0; i < answers.Length; i++)
		{
			if (answers[i].ID.Equals (CorrectQuestionID))
			{
				return answers[i];
			}
		}

		return null;
	}

	/// <summary>
	/// Reutrns true if given answer is the correct one.
	/// </summary>
	/// <returns><c>true</c> if given answer is the correct one; otherwise, <c>false</c>.</returns>
	/// <param name="answer">Answer.</param>
	public bool IsCorrectAnswer (Answer answer)
	{
		for (int i = 0; i < answers.Length; i++)
		{
			if (answers[i].ID.Equals (CorrectQuestionID))
			{
				return true;
			}
		}

		return false;
	}

	/// <summary>
	/// Determines whether this instance is correct answer the specified answerID.
	/// </summary>
	/// <returns><c>true</c> if this instance is correct answer the specified answerID; otherwise, <c>false</c>.</returns>
	/// <param name="answerID">Answer I.</param>
	public bool IsCorrectAnswer (string answerID)
	{
		for (int i = 0; i < answers.Length; i++)
		{
			if (answerID.Equals (CorrectQuestionID))
			{
				return true;
			}
		}

		return false;
	}


	private Question question;
	private Answer[] answers;
	private bool isValid;
	private string id;
}