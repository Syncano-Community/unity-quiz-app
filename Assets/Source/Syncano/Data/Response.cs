using UnityEngine;
using System.Collections;
using Syncano.Request;

namespace Syncano.Data {
/// <summary>
/// Wrapper class for getting response from Syncano.
/// </summary>
	public class Response<T> : SyncanoWebRequest {//where T : SyncanoObject<T>, new() {
	
	/// <summary>
	/// Deserialized data.
	/// </summary>
	public T Data { set; get; }
}
}