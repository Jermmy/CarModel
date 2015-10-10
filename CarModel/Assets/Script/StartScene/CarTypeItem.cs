using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class CarTypeItem : MonoBehaviour, IPointerClickHandler {

	public Text carText;
	public Image carImg;
	public Image line;
	public int car_type_id;
	public string carName;
	public string img_url;

	public string carModelUrl;
	public List<string> componentsUrls = new List<string>();
	public List<string> componentNames = new List<string>();

	private bool showWindow = false;

	public CarTypeScrollView carTypeScrollView;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI() {
		if (showWindow == true) {
			GUIStyle labelStyle = new GUIStyle (GUI.skin.label);
			labelStyle.fontSize = 20;
			GUILayout.Window(0, new Rect(Screen.width*1/3, Screen.height*1/3, Screen.width*1/3, Screen.height*1/3), WindowCallBack, "notice");
			//GUILayout.Window(0, new Rect(0, 0, Screen.width, Screen.height), WindowCallBack, "notice");

		}
	}

	void WindowCallBack(int windowID) {
		GUILayout.BeginVertical ();
		GUIStyle labelStyle = new GUIStyle (GUI.skin.label);
		labelStyle.fontSize = 30;
		GUILayout.Label("Do you want to choose this car?", labelStyle);
		GUILayout.FlexibleSpace ();

		GUILayout.BeginHorizontal ();
		GUIStyle btnStyle = new GUIStyle (GUI.skin.button);
		btnStyle.fontSize = 30;
		if (GUILayout.Button ("sure", btnStyle)) {
			Constant.hasChosed = true;
			Constant.CarTypeId = car_type_id;
			Constant.ComponentUrls = componentsUrls;
			Constant.ComponentNames = componentNames;
			Constant.CarModelUrl = carModelUrl;
			Application.LoadLevel("CarShow");
		}
		if (GUILayout.Button ("cancel", btnStyle)) {
			showWindow = false;
		}
		GUILayout.EndHorizontal ();
		GUILayout.EndVertical ();
	}

	public void OnPointerClick(PointerEventData enentData) {
		Debug.Log ("click");

		showWindow = true;
		//carTypeScrollView.GetComponent<CarTypeScrollView> ().OnCarTypeCliclListener (car_type_id);
		carTypeScrollView.OnCarTypeCliclListener (car_type_id);
	}

}
