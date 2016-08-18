using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;

/// <summary>
/// Client of Syncano using connection over UnityWebRequest. Provides functionality of sending end geting http request to and from Syncano.
/// </summary>
public class SyncanoClient : SelfInstantiatingSingleton<SyncanoClient>
{
	private const string BASE_URL = "https://api.syncano.io/v1.1/instances/{0}/";

	/// <summary>
	/// The name of the instance.
	/// </summary>
	public string InstanceName { get; private set; }

	/// <summary>
	/// The API key.
	/// </summary>
	public string ApiKey { get; private set; }

	/// <summary>
	/// The base URL variable.
	/// </summary>
	private string baseUrl;

	/// <summary>
	/// This flag checks if Syncano client was initialized.
	/// </summary>
	private bool isInitialized;

	/// <summary>
	/// This method must be called before making any call to Syncano.
	/// </summary>
	/// <param name="apiKey">API key.</param>
	/// <param name="instanceName">Instance name.</param>
	public void Init(string apiKey, string instanceName)
	{
		isInitialized = true;
		InstanceName = instanceName;
		ApiKey = apiKey;
		baseUrl = string.Format(BASE_URL, InstanceName);
	}

	/// <summary>
	/// Calls the script endpoint.
	/// </summary>
	/// <returns>The script endpoint.</returns>
	/// <param name="endpointId">Endpoint identifier.</param>
	/// <param name="scriptName">Script name.</param>
	/// <param name="callback">Callback to oprate when call is done.</param>
	public IEnumerator CallScriptEndpoint(string endpointId, string scriptName, System.Action<Response> callback)
	{
		if(isInitialized == false)
		{
			throw new SyncanoException("Error - Syncano client must be initialized. Please call Init method with correct parameters");
			yield break;
		}

		StringBuilder sb = new StringBuilder(baseUrl);
		sb.Append("endpoints/scripts/p/");
		sb.Append(endpointId);
		sb.Append("/");
		sb.Append(scriptName);
		sb.Append("/");

		UnityWebRequest webRequest = UnityWebRequest.Get(sb.ToString());

		yield return webRequest.Send();
		Response response = new Response();
		response.webError = webRequest.error;
		response.responseCode = webRequest.responseCode;

        Debug.Log(webRequest.downloadHandler.text);
		if(webRequest.isError == false)
		{
			response.Populate(webRequest.downloadHandler.text);
		}

		callback (response);
	}

	/// <summary>
	/// Calls the script endpoint asynchornously.
	/// </summary>
	/// <returns>Corouitne to yield</returns>
	/// <param name="endpointId">Endpoint identifier.</param>
	/// <param name="scriptName">Script name.</param>
	/// <param name="callback">Callback to oprate when call is done.</param>
	public Coroutine CallScriptEndpointAsync(string endpointId, string scriptName, System.Action<Response> callback)
	{
		return StartCoroutine(CallScriptEndpoint(endpointId, scriptName, callback));
	}
}
