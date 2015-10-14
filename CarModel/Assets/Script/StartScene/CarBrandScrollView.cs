using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using LitJson;

public class CarBrandScrollView : MonoBehaviour {

	public GridLayoutGroup gridLayout;
	public RectTransform scrollContent;
	public ScrollRect scrollRect;

	public CarTypeScrollView carTypeScrollView;
	public List<GameObject> brandItems;

	private bool isWindowShow = false;


	// Use this for initialization
	void Start () {
		brandItems = new List<GameObject> ();
		WWWForm param = new WWWForm ();
		param.AddField ("api_type", "0");
		param.AddField ("id", "1");
		GetComponent<HttpServer> ().SendRequest (Constant.ReuestUrl, param, InitializeList);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape) && isWindowShow == false) {
			isWindowShow = true;
		}
	}

	void OnGUI() {
		if (isWindowShow == true) {
			GUIStyle labelStyle = new GUIStyle (GUI.skin.label);
			//labelStyle.fontSize = 20;
			GUILayout.Window(1, new Rect(Screen.width*1/3, Screen.height*1/3, Screen.width*1/3, Screen.height*1/3), WindowCallBack, "");
		}
	}

	void WindowCallBack(int windowID) {
		GUILayout.BeginVertical ();
		GUIStyle labelStyle = new GUIStyle (GUI.skin.label);
		labelStyle.fontSize = 40;
		GUILayout.Label("Do you want to leave?", labelStyle);
		GUILayout.FlexibleSpace ();
		
		GUILayout.BeginHorizontal ();
		GUIStyle btnStyle = new GUIStyle (GUI.skin.button);
		btnStyle.fontSize = 40;
		if (GUILayout.Button ("sure", btnStyle)) {
			AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
			jo.Call ("finishUnity", "");
		}
		if (GUILayout.Button ("cancel", btnStyle)) {
			isWindowShow = false;
		}
		GUILayout.EndHorizontal ();
		GUILayout.EndVertical ();
	}

	public void SetContentWidth() {
		float scrollContentWidth = (gridLayout.transform.childCount * gridLayout.cellSize.x) + 
			((gridLayout.transform.childCount - 1) * gridLayout.spacing.x);
		float scrollContentHeight = GetComponent<RectTransform> ().rect.height;
		scrollContent.sizeDelta = new Vector2 (scrollContentWidth, scrollContentHeight);
	}


	// 初始化商标列表， 同时初始化第一种商标类型的汽车列表（包括图标和文字）
	public void InitializeList(JsonData data) {
		JsonData dataItems = data["data_list"];
		for (int i = 0; i < dataItems.Count; i++) {
			//GameObject item = (GameObject)Instantiate (item);
			GameObject item = GameObject.FindGameObjectWithTag("branditem");
			GameObject newItem = Instantiate(item) as GameObject;
			newItem.GetComponent<CarBrandItem> ().img_url = dataItems[i]["logo_url"].ToString();
			newItem.GetComponent<CarBrandItem> ().brand_id = (int)dataItems[i]["id"];
			newItem.GetComponent<CarBrandItem> ().name = dataItems[i]["name"].ToString();
			newItem.GetComponent<ImageLoader>().DisplayImage(newItem.GetComponent<CarBrandItem>().brandImg, newItem.GetComponent<CarBrandItem> ().img_url);
			//newItem.transform.localScale = Vector3.one;
			newItem.GetComponent<RectTransform>().localScale = Vector3.one;
			newItem.transform.SetParent(gridLayout.transform);
			newItem.SetActive(true);

			Color temp = newItem.GetComponent<CarBrandItem>().triangle.color;
			if (i == 0) {
				//Texture2D texture = (Texture2D)Resources.Load("tab_sel");
				//newItem.GetComponent<CarBrandItem>().triangle.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
				temp.a = 255f;
			} else {
				temp.a = 0f;
			}
			newItem.GetComponent<CarBrandItem>().triangle.color = temp;

			brandItems.Add(newItem);
			Constant.carBrandItems.Add(newItem);
		}
		SetContentWidth ();
		if (dataItems.Count > 0) {
			WWWForm wwwForm = new WWWForm();
			wwwForm.AddField("api_type", 1);
			wwwForm.AddField("id", dataItems[0]["id"].ToString());
			GetComponent<HttpServer>().SendRequest(Constant.ReuestUrl, wwwForm, carTypeScrollView.InitializeList);
		}

	}

	public void OnBrandItemClick(int brandId) {
		for (int i = 0; i < brandItems.Count; i++) {
			Color temp = brandItems[i].GetComponent<CarBrandItem>().triangle.color;
			if (brandItems[i].GetComponent<CarBrandItem>().brand_id == brandId) {
				temp.a = 255f;
			} else {
				temp.a = 0f;
			}
			brandItems[i].GetComponent<CarBrandItem>().triangle.color = temp;
		}
	}

}
