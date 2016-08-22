using UnityEngine;
using System;
using System.Reflection;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using Syncano.Data;
using Syncano.Request;

namespace Syncano.Client {
/// <summary>
/// Client of Syncano using connection over Http.
/// Provides functionality of sending end geting http request to and from Syncano.
/// </summary>
public class SyncanoHttpClient : SelfInstantiatingSingleton<SyncanoHttpClient> {

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
	
	/// <summary>
		/// This method starts coroutine for SendRequest and returns it, so it cal be yielded or run asynchronously.
	/// </summary>
	/// <returns>The async.</returns>
	/// <param name="obj">Object.</param>
	/// <param name="onSuccess">On success.</param>
	/// <param name="onFailure">On failure.</param>
	/// <param name="httpMethodOverride">Http method override.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public Coroutine PostAsync<T>(T obj, Action<Response<T>> onSuccess, Action<Response<T>> onFailure = null, string httpMethodOverride = null) where T :SyncanoObject<T> , new() {

		string serializedObject = obj != null ? obj.ToJson() : string.Empty;
		string id =  (obj != null && obj.id != 0) ? obj.id.ToString() : string.Empty;
		string url = UrlBuilder(id.ToString(), typeof(T));

		return StartCoroutine(SendRequest(url, serializedObject, onSuccess, onFailure, httpMethodOverride));
	}
	
	/// <summary>
	/// This method starts coroutine for SendRequest and returns it, so it cal be yielded or run asynchronously.
	/// </summary>
	/// <returns>The async.</returns>
	/// <param name="id">Identifier.</param>
	/// <param name="onSuccess">On success.</param>
	/// <param name="onFailure">On failure.</param>
	/// <param name="httpMethodOverride">Http method override.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public Coroutine PostAsync<T>(long id, Action<Response<T>> onSuccess, Action<Response<T>> onFailure = null, string httpMethodOverride = null) where T : SyncanoObject<T>, new() {

		string url = UrlBuilder(id.ToString(), typeof(T));

		return StartCoroutine(SendRequest(url, string.Empty, onSuccess, onFailure, httpMethodOverride));
	}
	
	/// <summary>
	/// Sends the request to Syncano. Used for all methods like Save, Delete, Create etc.
	/// </summary>
	/// <returns>The request.</returns>
	/// <param name="url">URL.</param>
	/// <param name="serializedObject">Serialized object.</param>
	/// <param name="onSuccess">On success.</param>
	/// <param name="onFailure">On failure.</param>
	/// <param name="httpMethodOverride">Http method override.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	private IEnumerator SendRequest<T>(string url, string serializedObject, Action<Response<T>> onSuccess, Action<Response<T>> onFailure = null, string httpMethodOverride = null) where T : SyncanoObject<T>, new() {

		UnityWebRequest www = new UnityWebRequest(url);
		www.SetRequestHeader(Constants.HTTP_HEADER_API_KEY, SyncanoClient.Instance.ApiKey);
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
		ReadWebRequest<Response<T>>(response, www);

		if(response.IsSuccess == false)
		{
			if(onFailure != null)
			{
				onFailure(response);
			}
		}

		else
		{
			if(onSuccess != null)
			{
				response.Data = SyncanoObject<T>.FromJson(www.downloadHandler.text);
				onSuccess(response);
			}
		}
	}
	
	/// <summary>
	/// Calls the script endpoint.
	/// </summary>
	/// <returns>The script endpoint.</returns>
	/// <param name="endpointId">Endpoint identifier.</param>
	/// <param name="scriptName">Script name.</param>
	/// <param name="callback">Callback.</param>
	public Coroutine CallScriptEndpoint(string endpointId, string scriptName, System.Action<ScriptEndpoint> callback) {
		return StartCoroutine(RequestScriptEndPoint(endpointId, scriptName, callback));
	}
	
	/// <summary>
	/// Method used for calling script endpoints.
	/// </summary>
	/// <returns>The script end point.</returns>
	/// <param name="endpointId">Endpoint identifier.</param>
	/// <param name="scriptName">Script name.</param>
	/// <param name="callback">Callback.</param>
	private IEnumerator RequestScriptEndPoint(string endpointId, string scriptName, System.Action<ScriptEndpoint> callback) {

		StringBuilder sb = new StringBuilder(string.Format(Constants.SCRIPT_ENDPOINT_URL, SyncanoClient.Instance.InstanceName, endpointId, scriptName));
		UnityWebRequest www = UnityWebRequest.Get(sb.ToString());

		yield return www.Send();

		ScriptEndpoint response = JsonUtility.FromJson<ScriptEndpoint>(www.downloadHandler.text);
		ReadWebRequest<ScriptEndpoint>(response, www);

		callback(response);
	}

	/// <summary>
	/// This method reads UnityWebRequest and sets all response codes, errors etc. to a SyncanoWebRequest object.
	/// </summary>
	/// <param name="webRequest">Web request.</param>
	/// <param name="www">Www.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	private void ReadWebRequest<T>(SyncanoWebRequest webRequest, UnityWebRequest www) {
		
		webRequest.responseCode = www.responseCode;
		webRequest.webError = www.error;
		bool isSyncanoError = CheckIfResponseIfSuccessForMethod(www.method, www.responseCode) != true;
		webRequest.IsSyncanoError = isSyncanoError;
		webRequest.IsSuccess = !www.isError && isSyncanoError == false;
	
		if(isSyncanoError) {
			webRequest.syncanoError = www.downloadHandler.text;
		}
	}
	
	/// <summary>
	/// URLs the builder for most of Dataobjects method calls.
	/// </summary>
	/// <returns>The builder.</returns>
	/// <param name="id">Identifier.</param>
	/// <param name="classType">Class type.</param>
	private string UrlBuilder(string id, Type classType) {

		StringBuilder sb = new StringBuilder(Constants.PRODUCTION_SERVER_URL);
		sb.Append(string.Format(Constants.OBJECTS_DETAIL_URL, SyncanoClient.Instance.InstanceName, classType.ToString(), id.ToString()));

		return sb.ToString();
	}

	/// <summary>
	/// Checks if response if success for Syncano Request. Some requests like DELETE should return true for codes like 404 and 204.
	/// </summary>
	/// <returns><c>true</c>, if if response if success for method was checked, <c>false</c> otherwise.</returns>
	/// <param name="method">Method.</param>
	/// <param name="resultCode">Result code.</param>
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
	}
}
}