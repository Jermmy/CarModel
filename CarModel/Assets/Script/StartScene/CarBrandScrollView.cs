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

	// Use this for initialization
	void Start () {
		//if (Constant.hasChosed == false) {
			WWWForm param = new WWWForm ();
			param.AddField ("api_type", "0");
			param.AddField ("id", "1");
			GetComponent<HttpServer> ().SendRequest (Constant.ReuestUrl, param, InitializeList);
//		} else {
//			if (Constant.carTypeItems != null && Constant.carBrandItems != null) {
//				InitializeListFromCache();
//			} else {
//				WWWForm param = new WWWForm ();
//				param.AddField ("api_type", "0");
//				param.AddField ("id", "1");
//				GetComponent<HttpServer> ().SendRequest (Constant.ReuestUrl, param, InitializeList);
//			}
//		}

	}
	
	// Update is called once per frame
	void Update () {
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
			//newItem.transform.localScale = new Vector3(0.5f,0.5f,0);
			newItem.GetComponent<RectTransform>().localScale = Vector3.one;
			newItem.transform.SetParent(gridLayout.transform);
			newItem.SetActive(true);
			Constant.carBrandItems.Add(newItem);
		}
		SetContentWidth ();
		if (dataItems.Count > 0) {
			WWWForm wwwForm = new WWWForm();
			wwwForm.AddField("api_type", 1);
			wwwForm.AddField("id", dataItems[0]["id"].ToString());
			GetComponent<HttpServer>().SendRequest(Constant.ReuestUrl, wwwForm, carTypeScrollView.InitializeList);
		}

//		for (int i = 0; i < Constant.carBrandItems.Count; ++i) {
//			DontDestroyOnLoad(Constant.carBrandItems[i]);
//		}
	}

//	public void InitializeListFromCache() {
//		Debug.Log ("Constant.carBrandItems.Count: " + Constant.carBrandItems.Count);
//		for (int i = 0; i < Constant.carBrandItems.Count; ++i) {
//			Constant.carBrandItems[i].GetComponent<Re>
//			Debug.Log(Constant.carBrandItems[i].name);
//			Constant.carBrandItems[i].transform.SetParent(gridLayout.transform);
//			Constant.carBrandItems[i].SetActive(true);
//		}
//		SetContentWidth ();
//
//	}

}
