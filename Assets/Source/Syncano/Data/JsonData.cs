using UnityEngine;
using System.Collections;

/// <summary>
/// Wrapper for a default Unity JSON serializer, deserializer.
/// </summary>
namespace Syncano.Data {
[System.Serializable]
public class JsonData<T>
{
	public static T FromJson(string json)
	{
		if (string.IsNullOrEmpty(json))
			return default(T); // Return null for generic.

		T instance = JsonUtility.FromJson<T>(json);
		return instance;
	}

	public string ToJson(bool prettyPrint = false)
	{
		return JsonUtility.ToJson(this, prettyPrint);
	}

	public void Populate(string json)
	{
		JsonUtility.FromJsonOverwrite(json, this);
	}
}
}