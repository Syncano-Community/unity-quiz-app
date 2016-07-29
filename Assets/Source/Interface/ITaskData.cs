using UnityEngine;
using System.Collections;

public interface ITaskData {
	/// <summary>
	/// Gets or sets the ID.
	/// </summary>
	/// <value>The ID.</value>
	string ID { get; set; }

	/// <summary>
	/// Gets or sets the answer's text.
	/// </summary>
	/// <value>The text.</value>
	string Text { get; set; } 
}
