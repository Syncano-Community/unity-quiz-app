using UnityEngine;
using System;
using System.Reflection;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.Networking;

public class HttpClient : SelfInstantiatingSingleton<HttpClient> {

	/// <summary>
	/// The base URL variable.
	/// </summary>
	private string baseUrl;

	public Coroutine GetAsync<T>(long id, Action<Response<T>> onSuccess, Action<Response<T>> onFailure = null) where T :SyncanoObject<T>, new() {

		return StartCoroutine(SendGetOneRequest(id, onSuccess, onFailure));
	}

	public Coroutine PostAsync<T>(T obj,  Action<Response<T>> onSuccess, Action<Response<T>> onFailure = null, string httpMethodOverride = null) where T :SyncanoObject<T> , new() {

		return StartCoroutine(SendPostRequest<T>(obj, onSuccess, onFailure, httpMethodOverride));
	}

	private IEnumerator SendPostRequest<T>(T obj,  Action<Response<T>> onSuccess, Action<Response<T>> onFailure = null, string httpMethodOverride = null) where T :SyncanoObject<T>, new() {

		UTF8Encoding encoding = new System.Text.UTF8Encoding();

		string serializedObject = obj != null ? obj.ToJson() : string.Empty;
		string id =  (obj != null && obj.id.HasValue) ? obj.id.Value.ToString() : string.Empty;
		string url = UrlBuilder(id.ToString(), typeof(T));

		UnityWebRequest www = new UnityWebRequest(url);
		www.SetRequestHeader(Constants.HTTP_HEADER_API_KEY, Syncano.Instance.ApiKey);
		www.SetRequestHeader("Content-Type", "application/json");

		www.downloadHandler = new DownloadHandlerBuffer();
	
		if(string.IsNullOrEmpty(httpMethodOverride) == false)
		{
			if(httpMethodOverride.Equals(UnityWebRequest.kHttpVerbDELETE))
			{
				www.method = httpMethodOverride;
			}
		}

		else
		{
			if(string.IsNullOrEmpty(serializedObject) == false)
			{
				www.uploadHandler = new UploadHandlerRaw (encoding.GetBytes(serializedObject));
			}
			www.method = UnityWebRequest.kHttpVerbPOST;
		}

		yield return www.Send();

		Response<T> response = new Response<T>();

		if(www.isError)
		{
			response.IsSuccess = false;
			response.webError = www.error;
			//response.responseCode = GetResponseCode(www); TODO

			if(onFailure != null)
			{
				onFailure(response);
			}
		}

		else
		{
			if(onSuccess != null)
			{
				response.Data = Response<T>.FromJson(www.downloadHandler.text);
				onSuccess(response);
			}
		}
	}

	private IEnumerator SendGetOneRequest<T>(long id, Action<Response<T>> onSuccess, Action<Response<T>> onFailure = null) where T :SyncanoObject<T>, new() {

		string url = UrlBuilder(id.ToString(), typeof(T));

		Dictionary<string, string> headers = new Dictionary<string, string>();
		headers.Add(Constants.HTTP_HEADER_API_KEY, Syncano.Instance.ApiKey);

		WWW www = new WWW(url, new byte[]{0}, headers);
		yield return www;

		Response<T> response = new Response<T>();
		response.Data = Response<T>.FromJson(www.text);

		if (string.IsNullOrEmpty (www.error) == false)
		{
			response.IsSuccess = false;
			response.webError = www.error;
			response.responseCode = GetResponseCode(www);

			if (onFailure != null)
			{
				onFailure(response);
			}
		}

		else
		{
			if (onSuccess != null)
			{
				onSuccess(response);
			}
		}
	}


	public Coroutine CallScriptEndpoint (string endpointId, string scriptName, System.Action<ScriptEndpoint> callback) { //where T : List<SyncanoObject<T>>, new() {
		return StartCoroutine(CallScriptEndpoint(endpointId, scriptName, callback, ""));
	}

	private  IEnumerator CallScriptEndpoint (string endpointId, string scriptName, System.Action<ScriptEndpoint> callback, string optionalParameters) { //where T : List<SyncanoObject<T>>, new() {

		StringBuilder sb = new StringBuilder(string.Format("https://api.syncano.io/v1.1/instances/{0}/", Syncano.Instance.InstanceName)); // TODO

		sb.Append("endpoints/scripts/p/");
		sb.Append(endpointId);
		sb.Append("/");
		sb.Append(scriptName);
		sb.Append("/");

		UnityWebRequest webRequest = UnityWebRequest.Get(sb.ToString());

		yield return webRequest.Send();

		ScriptEndpoint response = ScriptEndpoint.FromJson(webRequest.downloadHandler.text);

		callback(response);
	}

	private string UrlBuilder(string id, Type classType) {
		
		StringBuilder sb = new StringBuilder(Constants.PRODUCTION_SERVER_URL);
		sb.Append(string.Format(Constants.OBJECTS_DETAIL_URL, Syncano.Instance.InstanceName, classType.ToString(), id.ToString()));

		return sb.ToString();
	}

	public static int GetResponseCode(WWW request) {
		int ret = 0;
		if (request.responseHeaders == null) {
			Debug.LogError("no response headers.");
		}
		else {
			if (!request.responseHeaders.ContainsKey("STATUS")) {
				Debug.LogError("response headers has no STATUS.");
			}
			else {
				ret = ParseResponseCode(request.responseHeaders["STATUS"]);
			}
		}

		return ret;
	}

	public static int ParseResponseCode(string statusLine) {
		int ret = 0;

		string[] components = statusLine.Split(' ');
		if (components.Length < 3) {
			Debug.LogError("invalid response status: " + statusLine);
		}
		else {
			if (!int.TryParse(components[1], out ret)) {
				Debug.LogError("invalid response code: " + components[1]);
			}
		}

		return ret;
	}
}
