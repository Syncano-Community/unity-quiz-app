﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;

/// <summary>
/// Client of Syncano using connection over UnityWebRequest. Provides functionality of sending end geting http request to and from Syncano.
/// </summary>
public class Syncano : SelfInstantiatingSingleton<Syncano> {
	/// <summary>
	/// The name of the instance.
	/// </summary>
	public string InstanceName { get; private set; }

	/// <summary>
	/// The API key.
	/// </summary>
	public string ApiKey { get; private set; }

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
	}

	public RequestBuilder Please()
	{
		return new RequestBuilder();
	}
}