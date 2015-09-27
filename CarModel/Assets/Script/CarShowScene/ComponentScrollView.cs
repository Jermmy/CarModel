using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.Collections;
using System.Collections.Generic;

public class ComponentScrollView : MonoBehaviour {

	public GridLayoutGroup gridLayout;
	public RectTransform scrollContent;
	public ScrollRect scrollRect;

	public GameObject car; 

	public int car_type_id;
	public TypeScrollView typeScrollView;

	public Transform mCamera;

	private bool downLoadFinished = false;
	
	// Use this for initialization
	void Start () {
		car_type_id = Constant.CarTypeId;
		mCamera = Camera.main.gameObject.transform;
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
//		if (car != null) {
//			mCamera.RotateAround(car.transform.position, Vector3.up, Time.deltaTime*50);
//		}
	}

	void OnGUI() {
		if (downLoadFinished == false) {
			//Debug.Log("HH");
			//GUI.Window(0, new Rect(1/3 * Screen.width, 1/3 * Screen.height, 1/3 * Screen.width, 1/3 * Screen.height), OnWindow, "");
			//GUI.Window(0, new Rect(100, 100, 100, 100), OnWindow, "");
			//Debug.Log("screen width: " + Screen.width + " screen height: " + Screen.height);
			GUI.Window(0, new Rect(100, 100, 100, 100), OnWindow, "");
		}
	}

	private void OnWindow(int windowId) {
		//GUI.TextArea (new Rect (1 / 3 * Screen.width, 1 / 3 * Screen.height, 1 / 3 * Screen.width, 1 / 3 * Screen.height), "Loading...");
		GUI.TextArea (new Rect (20, 20, 150, 150), "Loading...");
	}

	public void InitializeCar(WWW www, string location) {
		car = (GameObject)Instantiate (www.assetBundle.mainAsset, 
		                               new Vector3 (0, 0, 0), Quaternion.Euler (270, 220, 0));
		www.assetBundle.Unload (false);

		for (int i = 0; i < Constant.ComponentUrls.Count; ++i) {
			GetComponent<MeshLoader>().DownloadMesh(Constant.ComponentUrls[i], Constant.ComponentNames[i], InitializeCarComponents);
			// 本想从这里控制loading框消失，然而毕竟协程。。。
			if (i == Constant.ComponentUrls.Count-1) {
				downLoadFinished = true;
			}
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
