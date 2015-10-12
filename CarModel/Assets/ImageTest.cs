using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ImageTest : MonoBehaviour, IPointerClickHandler {

	public Image image;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPointerClick(PointerEventData enentData) {
		DisplayImage (image, "file:///Users/xyz/Desktop/1.assetbundle");
	}

	public void DisplayImage(Image image, string url) {
		StartCoroutine (SendImgRequest (image, url));
	}
	
	private IEnumerator SendImgRequest(Image image, string url) {
		WWW www = WWW.LoadFromCacheOrDownload(url, 1);
		yield return www;
		Texture t = www.assetBundle.mainAsset as Texture;
		Sprite sprite = Sprite.Create ((Texture2D)t, new Rect (0, 0, t.width, t.height), 
		                               new Vector2 (0, 0));
		image.sprite = sprite;
	}

}
