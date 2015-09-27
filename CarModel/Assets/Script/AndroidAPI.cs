using UnityEngine;
using System.Collections;

public class AndroidAPI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.name = "car";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		if (GUILayout.Button ("调用Android API Toast", GUILayout.Height (45))) {
			AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
			jo.Call ("showMyToast", "调用Java方法");
		}
		if (GUILayout.Button ("调用Android API中打开Activity", GUILayout.Height (45))) {
			AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
			jo.Call("startWebView", "http://www.baidu.com");
		}
	}
}
