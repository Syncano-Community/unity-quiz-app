using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Syncano;
using Syncano.Data;

[System.Serializable]
public class ResponseGetList<T> : Response<T> {//where T : List<SyncanoObject<T>>, new() {

	public string prev;
	public string next;

	public List<T> objects;
}