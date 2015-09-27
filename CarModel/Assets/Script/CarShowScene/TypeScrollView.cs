using UnityEngine;
using LitJson;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TypeScrollView : MonoBehaviour {
	
	//public GameObject item;
	public GridLayoutGroup gridLayout;
	public RectTransform scrollContent;
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
	}

	public void SetContentWidth() {
		float scrollContentWidth = (gridLayout.transform.childCount * gridLayout.cellSize.x) + 
						((gridLayout.transform.childCount - 1) * gridLayout.spacing.x);
		float scrollContentHeight = GetComponent<RectTransform> ().rect.height;
		scrollContent.sizeDelta = new Vector2 (scrollContentWidth, scrollContentHeight);
	}

	public void InitializeList(JsonData data) {
		ClearAll ();
		JsonData jsonArray = data["data_list"];
		for (int i = 0; i < jsonArray.Count; i++) {
			GameObject item = GameObject.FindGameObjectWithTag("typeitem");
			GameObject newItem = Instantiate(item) as GameObject;
			newItem.GetComponent<TypeItem>().component_type_id = (int)jsonArray[i]["id"];
			newItem.GetComponent<TypeItem>().name = jsonArray[i]["name"].ToString();
			newItem.GetComponent<TypeItem>().img_url = jsonArray[i]["logo_url"].ToString();
			newItem.GetComponent<TypeItem>().location = jsonArray[i]["location"].ToString();
			newItem.GetComponent<TypeItem>().typeText.text = jsonArray[i]["name"].ToString();
			newItem.GetComponent<TypeItem>().ab_url = jsonArray[i]["ab_url"].ToString();
			newItem.GetComponent<ImageLoader>().DisplayImage(newItem.GetComponent<TypeItem>().image, jsonArray[i]["logo_url"].ToString());
			//newItem.GetComponent<TypeItem>().ab_url = 
			//newItem.transform.localScale = Vector3.one;
			//newItem.transform.localScale = new Vector3(0.7f, 0.7f, 0);
			newItem.GetComponent<RectTransform>().localScale = Vector3.one;
			newItem.transform.SetParent (gridLayout.transform);
			newItem.SetActive (true);
		}
		SetContentWidth ();
	}


	private IEnumerator MoveTowardsTarget(float time, float from, float target) {
		float i = 0;
		float rate = 1 / time;
		while (i < 1) {
			i += rate * Time.deltaTime;
			scrollRect.horizontalNormalizedPosition = Mathf.Lerp(from, target, i);
			yield return 0;
		}
	}
 
}
