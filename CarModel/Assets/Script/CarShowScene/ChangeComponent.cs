using UnityEngine;
using System.Collections;

public class ChangeComponent : MonoBehaviour {

	private bool change1;
	private bool change2;
	private bool change3;

	public Mesh ME1;
	public Material MA1;

	public Mesh ME2;
	public Material MA2;

	public Mesh ME3;
	public Material MA3;

	Transform truck;

	string download_url = "http://cm.sh.dl4.baidupcs.com/file/75fa623f14ee0aa1fce936617680db59?bkt=p2-nj02-586&fid=4043988066-250528-438895941245204&time=1441957953&sign=FDTAXGERLBH-DCb740ccc5511e5e8fedcff06b081203-Gp9TLN6VuwdGrKNVb9yvfVfcAMM%3D&to=scnj2&fm=Nan,B,M,mn&sta_dx=0&sta_cs=0&sta_ft=assetbundle&sta_ct=0&fm2=Nanjing02,B,M,mn&newver=1&newfm=1&secfm=1&flow_ver=3&pkey=140075fa623f14ee0aa1fce936617680db590b98df6400000002a3e5&sl=80805967&expires=8h&rt=sh&r=633705813&mlogid=813499542&vuk=4043988066&vbdid=3858589694&fin=gai.assetbundle&fn=gai.assetbundle&slt=pm&uta=0&rtype=1&iv=0&isw=0&dp-logid=5883126373078249273&dp-callid=0.1";

	// Use this for initialization
	void Start () {
		change1 = true;
		change2 = true;
		change3 = true;
		truck = GameObject.FindWithTag("car").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		if (GUILayout.Button ("back", GUILayout.Width(300), GUILayout.Height(100))) {
//			if (change1) {
//				truck.Find ("3D Models at 3dxy").Find("AI20_009_46").GetComponent<MeshFilter>().mesh = null;
//				truck.Find ("3D Models at 3dxy").Find("AI20_009_46").renderer.material = null;
//				change1 = false;
//			} else {
//				truck.Find("3D Models at 3dxy").Find("AI20_009_46").GetComponent<MeshFilter>().mesh = ME1;
//				truck.Find("3D Models at 3dxy").Find("AI20_009_46").renderer.material = MA1;
//				change1 = true;
//			}
			Application.LoadLevel("StartScene");
		}
//		if (GUILayout.Button ("Download component",  GUILayout.Width(300), GUILayout.Height(100))) {
//			//StartCoroutine(loadBundleMesh("file:///Users/xyz/Desktop/gai.assetbundle"));
//			StartCoroutine(loadBundleMesh(download_url));
//		}
	}

	IEnumerator loadBundleMesh(string url){
		Mesh meshtmp;
		//WWW date = new WWW(url);
		WWW date = WWW.LoadFromCacheOrDownload (url, 1);
		yield return date;
		AssetBundle bundle = date.assetBundle;
		AssetBundleRequest request = bundle.LoadAsync ("AI20_009_46", typeof(GameObject));
		yield return request;
		GameObject obj = request.asset as GameObject;
		meshtmp = obj.GetComponent<MeshFilter> ().mesh;
		truck.Find("3D Models at 3dxy").Find("AI20_009_46").GetComponent<MeshFilter>().mesh = meshtmp;
		truck.Find("3D Models at 3dxy").Find("AI20_009_46").renderer.material = MA1;
		date.assetBundle.Unload(false);
	}
}
