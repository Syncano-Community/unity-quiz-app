﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Wrapper class for getting response from Syncano.
/// </summary>
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
	/// The web response code.
	/// </summary>
	[System.NonSerialized]
	public long responseCode;

	/// <summary>
	/// The web error.
	/// </summary>
	[System.NonSerialized]
	public string webError;

	/// <summary>
	/// Shortcut for error message.
	/// </summary>
	public string stderr { get { return result.stderr; } }

	/// <summary>
	/// Shortcut for serialized Output in JSON.
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