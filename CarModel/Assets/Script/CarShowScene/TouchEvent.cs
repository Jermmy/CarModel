﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TouchEvent : MonoBehaviour {

	// 记录手指触屏的位置
	Vector2 m_screenpos = new Vector2();

	public Transform mCar;
	public GameObject toolBars;
	public GameObject scrollView;

	private Transform mCamera;

	float speed = 200.0f;

	float mx;
	float my;

	// Use this for initialization
	void Start () {
		Input.multiTouchEnabled = true;
		mCamera = transform;
	}
	
	// Update is called once per frame
	void Update () {
		//mCamera.RotateAround(mCar.position, Vector3.left, Time.deltaTime*speed);
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
		MobileInput();
#else 
		DesktopInput ();
#endif

	}

	// 桌面系统鼠标操作
	void DesktopInput() {
		// 记录鼠标左键的移动距离
		if (Input.GetMouseButtonDown (0)) {
			mx = Input.GetAxis("Mouse X");
			my = Input.GetAxis("Mouse Y");
			Debug.Log("key down: " + "mx: " + mx + " my: " + my);
		} else if (Input.GetMouseButtonUp(0)) {
			float upMX = Input.GetAxis("Mouse X");
			float upMY = Input.GetAxis("Mouse Y");
			if (upMX > mx && upMY > toolBars.transform.localPosition.y && my > toolBars.transform.localPosition.y) {
				mCamera.RotateAround(mCar.position, Vector3.up, Time.deltaTime*speed*100);
			} else if (upMX < mx && upMY > toolBars.transform.localPosition.y && my > toolBars.transform.localPosition.y) {
				mCamera.RotateAround(mCar.position, Vector3.up, -Time.deltaTime*speed*100);
			}
		}
	}

	// 移动平台触屏操作
	void MobileInput() {
		if (Input.touchCount <= 0) {
			return;
		}

		if (Input.touchCount == 1) {
			if (Input.touches [0].phase == TouchPhase.Began) {
					// 记录手指触屏的位置
				m_screenpos = Input.touches [0].position;
			} 
			// 手指移动
			else if (Input.touches [0].phase == TouchPhase.Moved) {
				Vector2 pos = Input.touches[0].position;
				if (Mathf.Abs (pos.x - m_screenpos.x) > Mathf.Abs (pos.y - m_screenpos.y)) {
					if (pos.x > m_screenpos.x && pos.y > toolBars.transform.localPosition.y && m_screenpos.y > toolBars.transform.localPosition.y) {

						mCamera.RotateAround(mCar.position, Vector3.up, Time.deltaTime*speed);
					} else if (pos.x < m_screenpos.x && pos.y > toolBars.transform.localPosition.y && m_screenpos.y > toolBars.transform.localPosition.y) {

						mCamera.RotateAround(mCar.position, Vector3.up, -Time.deltaTime*speed);
					}
				} else {
					if (pos.y > m_screenpos.y) {
						//mCar.Rotate (Vector3.left * Time.deltaTime * speed);
						//mCamera.RotateAround(mCar.position, Vector3.left, Time.deltaTime*speed);
					} else {
						//mCar.Rotate (-Vector3.left * Time.deltaTime * speed);
						//mCamera.RotateAround(mCar.position, Vector3.left, -Time.deltaTime*speed);
					}
				}
			}
			// 手指离开屏幕 判断移动方向
			if (Input.touches [0].phase == TouchPhase.Ended && 
					Input.touches [0].phase != TouchPhase.Canceled) {
					Vector2 pos = Input.touches [0].position;
					// 手指水平移动
					if (Mathf.Abs (m_screenpos.x - pos.x) > Mathf.Abs (m_screenpos.y - pos.y)) {
							if (m_screenpos.x > pos.x) {
									//手指向左划动
							} else {
									//手指向右划动
							}
					} else {
							if (m_screenpos.y > pos.y) {
									//手指向下划动
							} else {
									//手指向上划动
							}
					}
			}
		} else if (Input.touchCount > 1) {
			// 记录两个手指的位置
			Vector2 finger1 = new Vector2();
			Vector2 finger2 = new Vector2();

			// 记录两个手指的移动
			Vector2 mov1 = new Vector2();
			Vector2 mov2 = new Vector2();

			for (int i = 0; i < 2; ++i) {
				Touch touch = Input.touches[i];
				if (touch.phase == TouchPhase.Ended) {
					break;
				}
				if (touch.phase == TouchPhase.Moved) {
					float mov = 0;
					if (i == 0) {
						finger1 = touch.position;
						mov1 = touch.deltaPosition;
					} else {
						finger2 = touch.position;
						mov2 = touch.deltaPosition;
						if (finger1.x > finger2.x) {
							mov = mov1.x;
						} else {
							mov = mov2.x;
						}
						if (finger1.y > finger2.y) {
							mov += mov1.y;
						} else {
							mov += mov2.y;
						}

						if ((Vector3.Distance(Camera.main.transform.position, Vector3.zero) >= 5.5 && mov < 0)
						    || Vector3.Distance(Camera.main.transform.position, Vector3.zero) <= 0.2 && mov > 0) {
							return;
						} else {
							Camera.main.transform.Translate(0, 0, mov * Time.deltaTime);
						}

//						Vector3 newPos = Camera.main.transform.position + new Vector3(0, 0, mov * Time.deltaTime);
//						if (mCar != null && Vector3.Distance(newPos, mCar.position) <= 5.8f) {
//							Camera.main.transform.Translate(0, 0, mov * Time.deltaTime);
//						} else if (mCar != null && Vector3.Distance(newPos, mCar.position) > 5.8f && mov > 0) {
//							Camera.main.transform.Translate(0, 0, mov * Time.deltaTime);
//						}
					}
				}
			}
		}

	}
}
