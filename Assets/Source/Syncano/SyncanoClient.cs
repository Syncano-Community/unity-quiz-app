using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;

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

	private string baseUrl;

	private bool isInitialized;

	public void Init(string apiKey, string instanceName)
	{
		isInitialized = true;
		InstanceName = instanceName;
		ApiKey = apiKey;
		baseUrl = string.Format(BASE_URL, InstanceName);
	}

	public Coroutine CallScriptEndpointAsync(string endpointId, string scriptName, System.Action<Response> callback)
	{
		return StartCoroutine(CallScriptEndpoint(endpointId, scriptName, callback));
	}

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

		if(webRequest.isError == false)
		{
			response.Populate(webRequest.downloadHandler.text);
		}

		callback (response);
	}
}
