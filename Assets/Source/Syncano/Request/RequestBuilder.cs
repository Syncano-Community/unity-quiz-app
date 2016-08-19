using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class RequestBuilder {
	
	public Coroutine Get<T>(Action<Response<T>> onSuccess, Action<Response<T>> onFailure = null) where T : SyncanoObject<T>, new() {
		return HttpClient.Instance.PostAsync<T>(default(T), onSuccess, onFailure, UnityEngine.Networking.UnityWebRequest.kHttpVerbGET);
	}

	public Coroutine Save<T>(T obj, Action<Response<T>> onSuccess, Action<Response<T>> onFailure = null) where T :SyncanoObject<T>, new()  {
		return HttpClient.Instance.PostAsync<T>(obj, onSuccess, onFailure);
	}

	public Coroutine Delete<T>(T obj, Action<Response<T>> onSuccess, Action<Response<T>> onFailure = null) where T :SyncanoObject<T>, new()  {
		return HttpClient.Instance.PostAsync<T>(obj, onSuccess, onFailure, UnityEngine.Networking.UnityWebRequest.kHttpVerbDELETE);
	}

	public Coroutine CallScriptEndpoint(string endpointId, string scriptName, System.Action<ScriptEndpoint> callback){ // where T : List<SyncanoObject<T>>, new() {
		return HttpClient.Instance.CallScriptEndpoint(endpointId, scriptName, callback);
	}
}