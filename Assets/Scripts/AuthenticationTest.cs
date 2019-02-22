using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AuthenticationTest : MonoBehaviour {

	public string filePath = "Key";
	// Use this for initialization
	void Start () {
		if(!string.IsNullOrEmpty(filePath))
		{
			TextAsset textAsset = Resources.Load(filePath) as TextAsset;

			if(textAsset != null)
			{
				APIKey key = JsonUtility.FromJson<APIKey>(textAsset.text);
				Debug.Log(key.apiKey);
			}
		}
	}

	[Serializable]
	internal class APIKey
	{
		public string apiKey;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
