using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using LitJson;
using System.Collections;
using System.Collections.Generic;

public class ComponentItem : MonoBehaviour, IPointerClickHandler {

	public Text componentLabel;
	public Image triangle;
	public string componentName;
	public int componentId;
	public TypeScrollView typeScrollView;
	public ComponentScrollView componentScrollView;

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPointerClick(PointerEventData eventData) {
		componentScrollView.OnComponentItemClick (componentId);
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("api_type", 3);
		wwwForm.AddField ("id", componentId);
		GetComponent<HttpServer> ().SendRequest (Constant.ReuestUrl, wwwForm, typeScrollView.InitializeList);
	}

}
