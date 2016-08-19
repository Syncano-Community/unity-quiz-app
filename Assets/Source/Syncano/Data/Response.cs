﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Wrapper class for getting response from Syncano.
/// </summary>
public class Response<T> : JsonData<T> where T : SyncanoObject<T>, new()
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
	/// The web error.
	/// </summary>
	[System.NonSerialized]
	public string syncanoError;

	/// <summary>
	/// Gets or sets a value indicating whether request to Syncano was successful or not.
	/// </summary>
	/// <value><c>true</c> if is success; otherwise, <c>false</c>.</value>
	public bool IsSuccess { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether this instance is syncano error.
	/// </summary>
	/// <value><c>true</c> if this instance is syncano error; otherwise, <c>false</c>.</value>
	public bool IsSyncanoError { get; set; }

	/// <summary>
	/// Deserialized data.
	/// </summary>
	public T Data { set; get; }
}