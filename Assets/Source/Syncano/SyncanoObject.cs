using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;
using System;
using Syncano.Data;

namespace Syncano {
/// <summary>
/// Class representing basic data structure for all DataObjects returned from Syncano. Every class must override it, otherwise it won't deserialize properly.
/// </summary>
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
}