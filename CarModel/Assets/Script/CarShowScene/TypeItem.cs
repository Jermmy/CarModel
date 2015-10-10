using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class TypeItem : MonoBehaviour, IPointerClickHandler {

	public Text typeText;
	public Image image;
	public Image line;

	public int component_type_id;
	public string img_url;
	public string ab_url; // assetbundle url
	public string location; //  type of one of the components of the car, eg: AI20_009_43

	public ComponentScrollView componentScrollView;
	Transform car;

	public TypeScrollView typeScrollView;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPointerClick(PointerEventData eventData) {
		typeScrollView.OnTypeItemClick (component_type_id);
		GetComponent<MeshLoader> ().DownloadMesh (ab_url, location, LoadMesh);
	}
		                                      
	private void LoadMesh(WWW www, string location) {
		car = componentScrollView.car.transform;
//		car.Find("3D Models at 3dxy").Find(location).GetComponent<MeshFilter>().mesh = 
//			((GameObject)(www.assetBundle.mainAsset)).GetComponent<MeshFilter> ().mesh;
		car.Find("3D Models at 3dxy").Find(location).renderer.material = ((GameObject)(www.assetBundle.mainAsset)).renderer.material;
		www.assetBundle.Unload (false);
	}

}
