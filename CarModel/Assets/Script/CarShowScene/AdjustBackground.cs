using UnityEngine;
using System.Collections;

public class AdjustBackground : MonoBehaviour {

	public Sprite sprite;

	// Use this for initialization
	void Start () {
		Texture texture = sprite.texture;
		float widthFactor = 1.3f;
		float heightFactor = 1.3f;
		//Debug.Log ("texture width: " + texture.width + " texture height: " + texture.height);
		//Debug.Log ("Screen width: " + Screen.width + " Screen height: " + Screen.height);
		if (Screen.width > texture.width) {
			widthFactor = (float)Screen.width / texture.width;
		} 
		if (Screen.height > texture.height) {
			heightFactor = (float)Screen.height / texture.height;
		}
		transform.localScale = new Vector3 (widthFactor, heightFactor, 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
