using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
public class CloudFirestoreTest : MonoBehaviour {

	void Start () {
        StartCoroutine(CreateDocument());
    }

	IEnumerator CreateDocument(){
		var url = "https://firestore.googleapis.com/v1/projects/PROJECT_ID/databases/(default)/documents/unity?key=";
		// string json = "{'name':'unity'}";

		UnityWebRequest request = new UnityWebRequest(url, "POST");

		// json(string)をbyte[]に変換
		// byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

		// jsonを設定
		// request.uploadHandler   = (UploadHandler) new UploadHandlerRaw(bodyRaw);
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

    IEnumerator SignIn() {
        WWWForm form = new WWWForm();
        form.AddField("email", "a@example.com");
        form.AddField("password", "testtest");

        var url = "https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPassword?key=[APIキー]";

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
}
