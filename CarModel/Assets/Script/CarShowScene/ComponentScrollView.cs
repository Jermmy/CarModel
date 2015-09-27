using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.Collections;
using System.Collections.Generic;

public class ComponentScrollView : MonoBehaviour {

	public GridLayoutGroup gridLayout;
	public RectTransform scrollContent;
	public ScrollRect scrollRect;
	//public Slider slider;

	public GameObject car; 

	public int car_type_id;
	public TypeScrollView typeScrollView;

	public Transform mCamera;
	
	// Use this for initialization
	void Start () {
		car_type_id = Constant.CarTypeId;
		mCamera = Camera.main.gameObject.transform;
		//mCamera.RotateAround(new Vector3 (0, 0, 0), Vector3.up, Time.deltaTime*50);
//		slider.enabled = false;
//		slider.gameObject.SetActive (false);
//		slider.maxValue = 1.0f;
//		slider.minValue = 0;
//		slider.normalizedValue = 0;
		//Debug.Log ("CarModelUrl: " + Constant.CarModelUrl);
		GetComponent<MeshLoader>().DownloadMesh(Constant.CarModelUrl, "", InitializeCar);
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("api_type", 2);
		wwwForm.AddField ("id", car_type_id);
		GetComponent<HttpServer> ().SendRequest (Constant.ReuestUrl, wwwForm, InitializeList);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel("StartScene");
		}
		//Debug.Log("rotate");
		if (car != null) {
			mCamera.RotateAround(car.transform.position, Vector3.up, Time.deltaTime*50);
		}
	}

	public void InitializeCar(WWW www, string location) {
//		car = (GameObject)Instantiate (www.assetBundle.mainAsset, 
//		                               new Vector3 (-0.5f, 0f, 4.44f), Quaternion.Euler (270, 220, 0));
		//Debug.Log ("Instiate");
		car = (GameObject)Instantiate (www.assetBundle.mainAsset, 
		                               new Vector3 (0, 0, 0), Quaternion.Euler (270, 220, 0));
		www.assetBundle.Unload (false);


		for (int i = 0; i < Constant.ComponentUrls.Count; ++i) {
			GetComponent<MeshLoader>().DownloadMesh(Constant.ComponentUrls[i], Constant.ComponentNames[i], InitializeCarComponents);
		}
	}

	public void InitializeCarComponents(WWW www, string location) {
		//Debug.Log ("location: " + location);
		car.transform.Find ("3D Models at 3dxy").Find (location).GetComponent<MeshFilter> ().mesh = 
			((GameObject)(www.assetBundle.mainAsset)).GetComponent<MeshFilter> ().mesh;
		www.assetBundle.Unload (true);
	} 

	public void ClearAll() {
		for (int i = 0; i < gridLayout.transform.childCount; i++) {
			Destroy(gridLayout.transform.GetChild(i).gameObject);
		}
	}

	public void SetContentWidth() {
		float scrollContentWidth = (gridLayout.transform.childCount * gridLayout.cellSize.x) + 
						((gridLayout.transform.childCount - 1) * gridLayout.spacing.x) + gridLayout.padding.left;
		float scrollContentHeight = GetComponent<RectTransform>().rect.height;
		scrollContent.sizeDelta = new Vector2 (scrollContentWidth, scrollContentHeight);
 	}

	// 初始化部件列表， 同时初始化第一种部件类型的清单列表（包括图标和文字）
	public void InitializeList(JsonData data) {
		ClearAll ();
		JsonData jsonArray = data["data_list"];
		for (int i = 0; i < jsonArray.Count; i++) {
			GameObject item = GameObject.FindGameObjectWithTag("componentitem");
			//item = (GameObject)Instantiate(item);
			GameObject newItem = Instantiate(item) as GameObject;
			newItem.GetComponent<ComponentItem>().componentId = (int)jsonArray[i]["id"];
			newItem.GetComponent<ComponentItem>().componentName = jsonArray[i]["name"].ToString();
			newItem.GetComponent<ComponentItem>().componentLabel.text = jsonArray[i]["name"].ToString();
			newItem.GetComponent<ComponentItem>().name = jsonArray[i]["name"].ToString();
			newItem.GetComponent<RectTransform>().localScale = Vector3.one;
			newItem.transform.SetParent (gridLayout.transform);
			newItem.SetActive (true);
		}
		SetContentWidth ();
		if (jsonArray.Count > 0) {
			WWWForm wwwForm = new WWWForm();
			wwwForm.AddField("api_type", 3);
			wwwForm.AddField("id", (int)jsonArray[0]["id"]);
			GetComponent<HttpServer>().SendRequest(Constant.ReuestUrl, wwwForm, new HttpServer.GetJson(typeScrollView.InitializeList));
		}
	}

}
