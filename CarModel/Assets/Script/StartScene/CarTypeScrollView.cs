using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.Collections;
using System.Collections.Generic;

public class CarTypeScrollView : MonoBehaviour {

	//public GameObject item;
	public GridLayoutGroup gridLayout;
	public RectTransform scrollcontent;
	public ScrollRect scrollRect;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void ClearAll() {
		for (int i = 0; i < gridLayout.transform.childCount; i++) {
			Destroy(gridLayout.transform.GetChild(i).gameObject);
		}
		Constant.carTypeItems.Clear ();
	}

	public void SetContentWidth() {
		float scrollContentWidth = (gridLayout.transform.childCount * gridLayout.cellSize.x) + 
						(gridLayout.transform.childCount - 1) * gridLayout.spacing.x + gridLayout.padding.left;
		float scrollContentHeight = GetComponent<RectTransform>().rect.height;
		scrollcontent.sizeDelta = new Vector2 (scrollContentWidth, scrollContentHeight);
	}

	public void InitializeList(JsonData data) {
		ClearAll ();
		JsonData dataItems = data["data_list"];
		for (int i = 0; i < dataItems.Count; i++) {
			//item = Instantiate(item) as GameObject;
			GameObject item = GameObject.FindGameObjectWithTag("cartypeitem");
			GameObject newItem = Instantiate(item) as GameObject;
			newItem.GetComponent<CarTypeItem>().car_type_id = (int)dataItems[i]["id"];
			newItem.GetComponent<CarTypeItem>().img_url = dataItems[i]["logo_url"].ToString();
			newItem.GetComponent<CarTypeItem>().carName = dataItems[i]["name"].ToString();
			newItem.GetComponent<CarTypeItem>().name = dataItems[i]["name"].ToString();
			newItem.GetComponent<CarTypeItem>().carText.text = dataItems[i]["name"].ToString();
			newItem.GetComponent<CarTypeItem>().carModelUrl = dataItems[i]["model_url"].ToString();
			newItem.GetComponent<ImageLoader>().DisplayImage(newItem.GetComponent<CarTypeItem>().carImg, dataItems[i]["logo_url"].ToString());
			JsonData jsonArray = dataItems[i]["components_url"];
			for (int j = 0; j < jsonArray.Count; ++j) {
				newItem.GetComponent<CarTypeItem>().componentsUrls.Add(jsonArray[j].ToString());
			}
			jsonArray = dataItems[i]["components_name"];
			for (int j = 0; j < jsonArray.Count; ++j) {
				newItem.GetComponent<CarTypeItem>().componentNames.Add(jsonArray[j].ToString());
			}
			//newItem.transform.localScale = Vector3.one;
			newItem.GetComponent<RectTransform>().localScale = Vector3.one;
			newItem.transform.SetParent (gridLayout.transform);
			newItem.SetActive (true);
			Constant.carTypeItems.Add(newItem);
		}
		SetContentWidth ();
	}

	public void InitializeListFromCache() {
		for (int i = 0; i < Constant.carTypeItems.Count; ++i) {
			Constant.carTypeItems[i].transform.SetParent(gridLayout.transform);
			Constant.carTypeItems[i].SetActive(true);
		}
	}

}
