using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;

public class SyncanoClient : SelfInstantiatingSingleton<SyncanoClient>
{
	/// <summary>
	/// The name of the instance.
	/// </summary>
	public string InstanceName;

	/// <summary>
	/// The API key.
	/// </summary>
	public string ApiKey;

	private string baseUrl;

	public IEnumerator CallScriptEndpoint(string endpointId, string scriptName, System.Action<Response> callback)
	{
		baseUrl = string.Format("https://api.syncano.io/v1.1/instances/{0}/", InstanceName);

		StringBuilder sb = new StringBuilder(baseUrl);
		sb.Append("endpoints/scripts/p/");
		sb.Append(endpointId);
		sb.Append("/");
		sb.Append(scriptName);
		sb.Append("/");

		UnityWebRequest webRequest = UnityWebRequest.Get(sb.ToString());

		yield return webRequest.Send();

		if(webRequest.isError)
		{
			throw new SyncanoException(webRequest.error);
		}

		else
		{
            Debug.Log(webRequest.downloadHandler.text);
			Response response = Response.FromJson(webRequest.downloadHandler.text);
			callback (response);
		}
	}
}
