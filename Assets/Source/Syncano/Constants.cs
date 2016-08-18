using UnityEngine;
using System.Collections;

public class Constants : MonoBehaviour {

	/// <summary>
	/// Server URL
	/// </summary>
	public const string PRODUCTION_SERVER_URL = "https://api.syncano.io";

	public const string HTTP_HEADER_API_KEY = "X-API-KEY";

	#region objects
	public const string OBJECTS_DETAIL_URL = "/v1.1/instances/{0}/classes/{1}/objects/{2}/";
	#endregion objects
}
