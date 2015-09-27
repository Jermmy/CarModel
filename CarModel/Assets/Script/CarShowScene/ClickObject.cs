using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using LitJson;
using System.Collections;

public class ClickObject : MonoBehaviour, IPointerClickHandler {


	//Transform item;
	//Transform car;
	public Text text;

	// Use this for initialization
	void Start () {
		//item = transform.parent;
		//car = GameObject.FindWithTag ("car").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void OnPointerClick(PointerEventData eventData) {
		Debug.Log ("click");
		//StartCoroutine (loadBundleMesh (item.GetComponent<TypeItem>().ab_url));
		WWWForm param = new WWWForm ();
		param.AddField ("api_type", "1");
		param.AddField ("id", "1");

		GetComponent<HttpServer> ().SendRequest (Constant.ReuestUrl, param, InitializeList);

	}


	public void InitializeList(JsonData data) {
		text.text = data.ToString ();
	}


//	IEnumerator loadBundleMesh(string url){
//		Debug.Log (url);
//		Mesh meshtmp;
//		//WWW date = new WWW(url);
//		WWW date = WWW.LoadFromCacheOrDownload (url, 1);
//		yield return date;
//		AssetBundle bundle = date.assetBundle;
//		AssetBundleRequest request = bundle.LoadAsync (item.GetComponent<TypeItem>().location, typeof(GameObject));
//		Debug.Log (item.GetComponent<TypeItem>().location);
//		yield return request;
//		GameObject obj = request.asset as GameObject;
//		meshtmp = obj.GetComponent<MeshFilter> ().mesh;
//		car.Find("3D Models at 3dxy").Find(item.GetComponent<TypeItem>().location).GetComponent<MeshFilter>().mesh = meshtmp;
//		//car.Find("3D Models at 3dxy").Find(item.GetComponent<TypeItem>().type).renderer.material = ma;
//		date.assetBundle.Unload(false);
//	}
}
