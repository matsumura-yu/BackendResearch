using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using System.Text;

public class FirebaseTest : MonoBehaviour {

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
			StartCoroutine(CreateDocument());
		}
	}

	[Serializable]
	internal class APIKey
	{
		public string apiKey;
		public string projectId;
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

	IEnumerator CreateDocument(){
		var url = "https://firestore.googleapis.com/v1/projects/" + key.projectId + "/databases/(default)/documents/collectionName?key=" + key.apiKey;
		string json = "{'fields': {'firstName': {'stringValue': 'testFirstName'}, 'lastName': {'stringValue':'testLastName'}}}";
		
		UnityWebRequest request = new UnityWebRequest(url, "POST");

		// json(string)をbyte[]に変換
		byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

		// jsonを設定
		request.uploadHandler   = (UploadHandler) new UploadHandlerRaw(bodyRaw);
		request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();

		//ヘッダーにタイプを設定
		//request.SetRequestHeader("Content-Type", "application/json");

		yield return request.SendWebRequest();

		if(request.isNetworkError || request.isHttpError){
			Debug.Log("失敗");
			Debug.Log(request.responseCode);
			Debug.Log(request.error);
		}else{
			Debug.Log("成功");
            Debug.Log(request.responseCode);
            Debug.Log(request.downloadHandler.text);
		}
	}


	// Update is called once per frame
	void Update () {
		
	}
}
