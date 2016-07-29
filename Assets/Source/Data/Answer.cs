using UnityEngine;
using System.Collections;

public class Answer : ITaskData {
	/// <summary>
	/// Gets or sets the answer's ID.
	/// </summary>
	/// <value>The ID.</value>
	public string ID { get; set; }

	/// <summary>
	/// Gets or sets the answer's text.
	/// </summary>
	/// <value>The text.</value>
	public string Text { get; set; } 
}