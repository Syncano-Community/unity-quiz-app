using UnityEngine;
using System.Collections;

/// <summary>
/// Wrapper class for getting response from Syncano.
/// </summary>
public class Response<T> : SyncanoWebRequest where T : SyncanoObject<T>, new() {
	
	/// <summary>
	/// Deserialized data.
	/// </summary>
	public T Data { set; get; }
}