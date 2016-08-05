using UnityEngine;
using System.Collections;

public class Response : JsonData<Response>
{
	/// <summary>
	/// The response status. Returns success or fail string.
	/// </summary>
	public string status;

	/// <summary>
	/// The duration of processing request.
	/// </summary>
	public int duration;

	/// <summary>
	/// The serialized result.
	/// </summary>
	public Result result;

	/// <summary>
	/// The error code.
	/// </summary>
	[System.NonSerialized]
	public long responseCode;

	[System.NonSerialized]
	public string webError;


	public string stderr { get { return result.stderr; } }

	/// <summary>
	/// Shortcut to get JSON response.
	/// </summary>
	public string stdout { get { return result.stdout; } }

	/// <summary>
	/// Class for holding error and output string.
	/// </summary>
	[System.Serializable]
	public class Result
	{
		/// <summary>
		/// Error message.
		/// </summary>
		public string stderr;

		/// <summary>
		/// Serialized Output in JSON.
		/// </summary>
		public string stdout;

	}
}