using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class AuthenticationTest : MonoBehaviour {

	public string filePath = "Key";

	APIKey key;
	void Start () {
		if(!string.IsNullOrEmpty(filePath))
		{
			TextAsset textAsset = Resources.Load(filePath) as TextAsset;

			if(textAsset != null)
			{
				key = JsonUtility.FromJson<APIKey>(textAsset.text);
			}
		}

		if(key != null){
			Debug.Log(key.apiKey);
			StartCoroutine(SignIn());
		}
	}

	[Serializable]
	internal class APIKey
	{
		public string apiKey;
	}
	
	IEnumerator SignIn() {
        WWWForm form = new WWWForm();
        form.AddField("email", "test@gmail.com");
        form.AddField("password", "testtest");

        var url = "https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPassword?key=" + key.apiKey;

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError) {
            Debug.Log("失敗");
            Debug.Log(www.responseCode);
            Debug.Log(www.error);
        } else {
            Debug.Log("成功");
            Debug.Log(www.responseCode);
            Debug.Log(www.downloadHandler.text);
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
