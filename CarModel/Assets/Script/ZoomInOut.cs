using UnityEngine;
using System.Collections;

public class ZoomInOut : MonoBehaviour {

	private Transform mCamera;
	// Current distance of the camera
	private float mDistance;

	private Transform mCar;

	/// <summary>
	/// 摄像机的缩放速率
	/// </summary>
	private float mZoomRate=0.5F;

	/// <summary>
	/// 定义摄像机的最近距离	
	/// </summary>
	private float mNear=2.5F;

	/// <summary>
	/// 定义摄像机的最远距离
	/// </summary>
	private float mFar=7.5F;

	// Use this for initialization
	void Start () {
		this.name = "Main Camera";
		mCar = GameObject.FindWithTag ("car").transform;
		mCamera = Camera.main.gameObject.transform;
		// calculate the distance between car and camera
		mDistance = Vector3.Distance (mCar.position, mCamera.position);

	}
	
	// Update is called once per frame
	void Update () {

	}

	/// <summary>
	/// 定义一个放大的方法供外部调用
	/// </summary>
	public void ZoomIn() {
		mDistance -= mZoomRate;
		mDistance = Mathf.Clamp (mDistance, mNear, mFar);
		//mCamera.transform.position=mCamera.transform.rotation * new Vector3(0,0,-mDistance)+mCamera.transform.position;
		mCamera.Translate (Vector3.forward * Time.deltaTime * mDistance);
	}

	/// <summary>
	/// 定义一个缩小的方法供外部调用
	/// </summary>
	public void ZoomOut()
	{
		mDistance+=mZoomRate;
		mDistance=Mathf.Clamp(mDistance,mNear,mFar);
		//mCamera.position=mCamera.transform.rotation * new Vector3(0,0,-mDistance)+mCamera.position;
		mCamera.Translate (-Vector3.forward * Time.deltaTime * mDistance);
	}
}
