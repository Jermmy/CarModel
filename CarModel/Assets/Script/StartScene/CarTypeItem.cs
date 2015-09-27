using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class CarTypeItem : MonoBehaviour, IPointerClickHandler {

	public Text carText;
	public Image carImg;
	public int car_type_id;
	public string carName;
	public string img_url;

	public string carModelUrl;
	//public string carModelName;
	public List<string> componentsUrls = new List<string>();
	public List<string> componentNames = new List<string>();

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPointerClick(PointerEventData enentData) {
		Debug.Log ("click");

		for (int i = 0; i < Constant.carTypeItems.Count; ++i) {
			DontDestroyOnLoad(Constant.carTypeItems[i]);
		}
		Constant.hasChosed = true;
		Constant.CarTypeId = car_type_id;
		//Constant.CarName = carName;
		Constant.ComponentUrls = componentsUrls;
		Constant.ComponentNames = componentNames;
		Constant.CarModelUrl = carModelUrl;
		//Constant.CarModelName = carModelName;
 		Application.LoadLevel("CarShow");
	}

}
