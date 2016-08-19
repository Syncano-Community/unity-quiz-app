using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;
using System;


public class SyncanoObject<T> : JsonData<T> {

	/// <summary>
	/// Empty constructor is required when deserializing Syncano objects.
	/// </summary>
	public SyncanoObject() { }

	/// <summary>
	/// The identifier of this object from Syncano.
	/// </summary>
	public long id;

}
