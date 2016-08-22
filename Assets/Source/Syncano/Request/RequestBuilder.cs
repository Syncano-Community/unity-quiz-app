using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Syncano.Client;
using Syncano.Data;

namespace Syncano.Request {
public class RequestBuilder {
	
	public Coroutine Get<T>(long id, Action<Response<T>> onSuccess, Action<Response<T>> onFailure = null) where T : SyncanoObject<T>, new() {
		return HttpClient.Instance.PostAsync<T>(id, onSuccess, onFailure, UnityEngine.Networking.UnityWebRequest.kHttpVerbGET);
	}

	public Coroutine Save<T>(T obj, Action<Response<T>> onSuccess, Action<Response<T>> onFailure = null) where T :SyncanoObject<T>, new()  {
		return HttpClient.Instance.PostAsync<T>(obj, onSuccess, onFailure);
	}

	public Coroutine Delete<T>(T obj, Action<Response<T>> onSuccess, Action<Response<T>> onFailure = null) where T :SyncanoObject<T>, new()  {
		return HttpClient.Instance.PostAsync<T>(obj, onSuccess, onFailure, UnityEngine.Networking.UnityWebRequest.kHttpVerbDELETE);
	}

	public Coroutine CallScriptEndpoint(string endpointId, string scriptName, System.Action<ScriptEndpoint> callback) {
		return HttpClient.Instance.CallScriptEndpoint(endpointId, scriptName, callback);
	}
}
}