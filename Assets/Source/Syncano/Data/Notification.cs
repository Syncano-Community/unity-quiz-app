using UnityEngine;
using System.Collections;
using Syncano;
using System.Xml;

[System.Serializable]
public class Notification : SyncanoObject {

	/// <summary>
	/// The payload.
	/// </summary>
	public Payload payload;

	/// <summary>
	/// The admin.
	/// </summary>
	public Author admin;

	/// <summary>
	/// The action.
	/// </summary>
	public MetaData action;

	public Notification() { }

	[System.Serializable]
	public struct Payload
	{
		public string content;
	}

	[System.Serializable]
	public struct Author
	{
		public int admin;
	}

	[System.Serializable]
	public struct MetaData
	{
		string type;
	}
}