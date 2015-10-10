using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class CaptureEvent : MonoBehaviour, IPointerClickHandler {

	#if UNITY_EDITOR || UNITY_STANDALONE_WIN
	//public static string PathURL = "file://" + Application.dataPath +"/StreamingAssets/";
	public static string PathURL = Application.dataPath +"/StreamingAssets/";
	#elif UNITY_IPHONE
	public static readonly string PathURL = Application.dataPath +"/Raw/";
	#elif UNITY_ANDROID
	public static readonly string PathURL = "jar:file://" + Application.dataPath + "!/assets/";
	#endif

	//string mPath3=Application.dataPath+"/ScreenShot/ScreenShot3.png"; 

	Transform mCamera;

	// Use this for initialization
	void Start () {
		mCamera = Camera.main.gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPointerClick(PointerEventData eventData) {
		StartCoroutine (CaptureByCamera (mCamera.camera, new Rect(0, 0, Screen.width, Screen.height), PathURL+"1.png"));
	}

	private IEnumerator CaptureByCamera(Camera mCamera, Rect mRect, string mFileName) {
		yield return new WaitForEndOfFrame();
		RenderTexture mRender = new RenderTexture ((int)mRect.width, (int)mRect.height, 0);
		mCamera.targetTexture = mRender;
		mCamera.Render ();
		RenderTexture.active = mRender;
		Texture2D mTexture = new Texture2D ((int)mRect.width, (int)mRect.height, TextureFormat.RGB24, false);
		mTexture.ReadPixels (mRect, 0, 0);
		mTexture.Apply ();

		mCamera.targetTexture = null;
		RenderTexture.active = null;
		GameObject.Destroy (mRender);

		byte [] bytes = mTexture.EncodeToPNG ();
		System.IO.File.WriteAllBytes (mFileName, bytes);
	}
}
