using UnityEngine;
using System;
using System.Reflection;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.Networking;

public class HttpClient : SelfInstantiatingSingleton<HttpClient> {

	/// <summary>
	/// Everything is ok!
	/// </summary>
	public const int HTTP_CODE_SUCCESS = 200;

	/// <summary>
	/// The request was successful and a resource was created.
	/// </summary>
	public const int HTTP_CODE_CREATED = 201;

	///<summary>
	/// The request was successful but there is no data to
	/// return (usually after successful DELETE request).
	///</summary>
	public const int HTTP_CODE_NO_CONTENT = 204;

	///<summary>
	/// There was no new data to return.
	///</summary>
	public const int HTTP_CODE_NOT_MODIFIED = 304;

	///<summary>
	/// The request was invalid or cannot be otherwise served.
	/// An accompanying error message will explain further.
	///</summary>
	public const int HTTP_CODE_BAD_REUEST = 400;

	///<summary>
	/// Authentication credentials were missing or incorrect.
	///</summary>
	public const int HTTP_CODE_UNAUTHORIZED = 401;

	///<summary>
	/// The request is understood, but it has been refused or
	/// access is not allowed. An accompanying error message will explain why.
	///</summary>
	public const int HTTP_CODE_FORBIDDEN = 403;

	///<summary>
	/// The URI requested is invalid or the resource requested, such as a user, does not exists.
	/// Also returned when the requested format is not supported by the requested method.
	///</summary>
	public const int HTTP_CODE_NOT_FOUND = 404;

	///<summary>
	/// Requested method is not supported for this resource.
	///</summary>
	public const int HTTP_CODE_METHOD_NOT_ALLOWED = 405;

	///<summary>
	/// Something is broken. Please contact Syncano support.
	///</summary>
	public const int HTTP_CODE_INTERNAL_SERVER_ERROR = 500;

	///<summary>
	/// Syncano is down. Contact support.
	///</summary>
	public const int HTTP_CODE_BAD_GATEWAY = 502;

	///<summary>
	/// Syncano request timeout.
	///</summary>
	public const int HTTP_CODE_GATEWAY_TIMEOUT = 504;

	///<summary>
	/// Status code when response is ok.
	///</summary>
	public const int CODE_SUCCESS = 0;

	///<summary>
	/// Status code when Http error appeared.
	///</summary>
	public const int CODE_HTTP_ERROR = 1;

	///<summary>
	/// Client Protocol Exception
	///</summary>
	public const int CODE_CLIENT_PROTOCOL_EXCEPTION = 2;

	///<summary>
	/// Illegal State Exception.
	///</summary>
	public const int CODE_ILLEGAL_STATE_EXCEPTION = 3;

	///<summary>
	/// IOException.
	///</summary>
	public const int CODE_ILLEGAL_IO_EXCEPTION = 4;

	///<summary>
	/// Parsing exception.
	///</summary>
	public const int CODE_PARSING_RESPONSE_EXCEPTION = 5;

	///<summary>
	/// Unknown
	///</summary>
	public const int CODE_UNKNOWN_ERROR = 6;

	public Coroutine PostAsync<T>(T obj, Action<Response<T>> onSuccess, Action<Response<T>> onFailure = null, string httpMethodOverride = null) where T :SyncanoObject<T> , new() {

		string serializedObject = obj != null ? obj.ToJson() : string.Empty;
		string id =  (obj != null && obj.id != 0) ? obj.id.ToString() : string.Empty;
		string url = UrlBuilder(id.ToString(), typeof(T));

		return StartCoroutine(SendRequest(url, serializedObject, onSuccess, onFailure, httpMethodOverride));
	}
		
	public Coroutine PostAsync<T>(long id, Action<Response<T>> onSuccess, Action<Response<T>> onFailure = null, string httpMethodOverride = null) where T : SyncanoObject<T>, new() {
		
		string url = UrlBuilder(id.ToString(), typeof(T));

		return StartCoroutine(SendRequest(url, string.Empty, onSuccess, onFailure, httpMethodOverride));
	}

	private IEnumerator SendRequest<T>(string url, string serializedObject, Action<Response<T>> onSuccess, Action<Response<T>> onFailure = null, string httpMethodOverride = null) where T :SyncanoObject<T>, new() {

		UnityWebRequest www = new UnityWebRequest(url);
		www.SetRequestHeader(Constants.HTTP_HEADER_API_KEY, Syncano.Instance.ApiKey);
		www.SetRequestHeader("Content-Type", "application/json");
		UTF8Encoding encoding = new System.Text.UTF8Encoding();

		www.downloadHandler = new DownloadHandlerBuffer();

		www.method = string.IsNullOrEmpty(httpMethodOverride) ? UnityWebRequest.kHttpVerbPOST : httpMethodOverride;

		if(string.IsNullOrEmpty(serializedObject) == false)
		{
			www.uploadHandler = new UploadHandlerRaw (encoding.GetBytes(serializedObject));
		}

		yield return www.Send();

		Response<T> response = new Response<T>();
		response.responseCode = www.responseCode;
		response.IsSuccess = !www.isError;
		response.webError = www.error;
		bool isSyncanoError = CheckIfResponseIfSuccessForMethod(httpMethodOverride, www.responseCode) != true;
		response.IsSyncanoError = isSyncanoError;

		if(www.isError || isSyncanoError)
		{
			if(isSyncanoError)
				response.syncanoError = www.downloadHandler.text;

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

	public Coroutine CallScriptEndpoint (string endpointId, string scriptName, System.Action<ScriptEndpoint> callback) { //where T : List<SyncanoObject<T>>, new() {
		return StartCoroutine(CallScriptEndpoint(endpointId, scriptName, callback, ""));
	}

	private IEnumerator CallScriptEndpoint (string endpointId, string scriptName, System.Action<ScriptEndpoint> callback, string optionalParameters) { //where T : List<SyncanoObject<T>>, new() {

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

	private bool CheckIfResponseIfSuccessForMethod(string method, long resultCode) {

		switch(method)
		{
			case UnityWebRequest.kHttpVerbGET:
				if(resultCode == HTTP_CODE_SUCCESS)
				{
					return true;
				}
				return false;
			case UnityWebRequest.kHttpVerbPOST:
				if(resultCode == HTTP_CODE_SUCCESS || resultCode == HTTP_CODE_NO_CONTENT || resultCode == HTTP_CODE_CREATED)
				{
					return true;
				}
				return false;
			case UnityWebRequest.kHttpVerbDELETE:
				if(resultCode == HTTP_CODE_NOT_FOUND || resultCode == HTTP_CODE_NO_CONTENT)
				{
					return true;
				}
				return false;
			default:
				return false;
		}

		return false;
	}
}
