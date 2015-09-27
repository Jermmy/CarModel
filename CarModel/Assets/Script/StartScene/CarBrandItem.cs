using UnityEngine;
using UnityEngine.UI;
using LitJson;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class CarBrandItem : MonoBehaviour, IPointerClickHandler {

	public Image brandImg; 
	public int brand_id;
	public string img_url;
	public CarTypeScrollView carTypeScrollView;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPointerClick(PointerEventData eventData) {
		Debug.Log("click");
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("api_type", 1);
		wwwForm.AddField ("id", brand_id);
		GetComponent<HttpServer> ().SendRequest (Constant.ReuestUrl, wwwForm, new HttpServer.GetJson(carTypeScrollView.InitializeList));
	}

}
