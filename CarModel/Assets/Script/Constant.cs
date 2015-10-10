using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Constant : MonoBehaviour {
	
	// 所有请求接口的url
	public static string ReuestUrl = "http://115.29.178.150:8002/api?r=GetCarItem";
	//public static string ReuestUrl = "172.18.143.64:8002/api?r=GetCarItem";

	// 场景一选中的车型ID，在场景二请求数据时用到
	public static int CarTypeId = 1;

	// 场景一选中的车型名称，在场景二获取汽车实例时用到
	//public static string CarName = "car";
	//public static GameObject car;

	public static string CarModelUrl;
	public static List<string> ComponentUrls = new List<string>();
	public static List<string> ComponentNames = new List<string> ();

	public static List<GameObject> carBrandItems = new List<GameObject> ();
	public static List<GameObject> carTypeItems = new List<GameObject> ();
	public static bool hasChosed = false;

	public static Color lineColor = new Color(78, 89, 116);

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
