using UnityEngine;
using LitJson;
using System.Collections;

public class HttpServer : MonoBehaviour {

	public delegate void GetJson(JsonData data);

	WWW www;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float GetProgress() {
		if (www != null && !www.isDone) {
			return www.progress;
		}
		return 0;
	}

	public void SendRequest(string url, WWWForm wwwForm, GetJson getJson) {
		StartCoroutine (SendPostRequest (url, wwwForm, getJson));
	}

//	public void SendRequest(string url) {
//		StartCoroutine (SendRequest (url));
//	}

	private IEnumerator SendPostRequest(string url, WWWForm wwwForm, GetJson getJson) {
		www = new WWW (url, wwwForm);
		yield return www;
		if (www.error != null) {
			Debug.Log(www.error);
		} else {
			Debug.Log(www.text);
			JsonData jsonData = JsonMapper.ToObject(www.text);
			if (jsonData["status_id"].ToString().Equals("1")) {
				if (getJson != null) {
				    getJson(jsonData);
				}
			} else {

			}
		}
	}

}
