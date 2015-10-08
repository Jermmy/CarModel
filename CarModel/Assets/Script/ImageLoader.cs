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
		WWW www = WWW.LoadFromCacheOrDownload("file://Users/xyz/Desktop/car.assetbundle", 1);
		//WWW www = WWW.LoadFromCacheOrDownload(url, 1);
		//WWW www = new WWW (url);
		//WWW www = new WWW ("file://Users/xyz/Desktop/car.assetbundle");
		yield return www;
//		Texture2D texture2d = (Texture2D)www.assetBundle.Load("car.assetbundle", typeof(Texture2D));
//		Sprite sprite = Sprite.Create ((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
//		Sprite sprite = Sprite.Create (www.texture, new Rect (0, 0, www.texture.width, www.texture.height), 
//		                       new Vector2 (0, 0));
//		image.sprite = sprite;
		image.sprite = www.assetBundle.mainAsset as Sprite;
	}
}
