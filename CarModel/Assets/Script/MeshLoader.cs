using UnityEngine;
using System.Collections;

public class MeshLoader : MonoBehaviour {

	public delegate void GetWWW(WWW www, string location);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DownloadMesh(string url, string location, GetWWW getWWW) {
		StartCoroutine (DownloadAssetBundle (url, location, getWWW));
	}

	private IEnumerator DownloadAssetBundle(string url, string location, GetWWW getWWW) {
		//Debug.Log ("url: " + url);
		WWW www = new WWW (url);
		yield return www;
		if (www.error != null) {
			Debug.Log(www.error);
		} else if (getWWW != null) {
			getWWW(www, location);
		}
	}

//	private IEnumerator DownloadAssetBundle(string url, string location, GetMesh getMesh) {
//		WWW www = WWW.LoadFromCacheOrDownload (url, 1);
//		yield return www;
//		Mesh mesh = (www.assetBundle.LoadAsync (location, typeof(GameObject)).asset as GameObject).
//			GetComponent<MeshFilter>().mesh;
//		if (getMesh != null) {
//			getMesh(mesh);
//		}
//		www.assetBundle.Unload (true);
//	}
}
