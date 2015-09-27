using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DisplayImage(Image image, string url) {
		StartCoroutine (SendImgRequest (image, url));
	}

	private IEnumerator SendImgRequest(Image image, string url) {
		//WWW www = WWW.LoadFromCacheOrDownload(url, 1);
		WWW www = new WWW (url);
		yield return www;
		//AssetBundle assetBundle = www.assetBundle.;
		//Texture texture = assetBundle.Load (asset);
		//Texture texture = www.texture;
		Sprite sprite = Sprite.Create (www.texture, new Rect (0, 0, www.texture.width, www.texture.height), 
		                       new Vector2 (0, 0));
		image.sprite = sprite;
	}
}
