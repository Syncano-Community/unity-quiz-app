using UnityEngine;
using System.Collections;

public class Question : ITaskData {
	/// <summary>
	/// Gets or sets the quesion's ID.
	/// </summary>
	/// <value>The ID.</value>
	public string ID { get; set; }

	/// <summary>
	/// Gets or sets the quesion's text.
	/// </summary>
	/// <value>The text.</value>
	public string Text { get; set; } 
}