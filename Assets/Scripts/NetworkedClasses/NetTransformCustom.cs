using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

[RequireComponent(typeof(NetIdentityCustom))]
public class NetTransformCustom : MonoBehaviour{

	public Transform trans;
	[Range(0,30)]
	public int sendRate = 5;
	public NetTransportObjectSync.TransUpdateType updateType;
	[HideInInspector]
	public Coroutine coroutine;
	[HideInInspector]
	public bool smoothing = true;

	public NetTransformCustom(){}

	void Start(){
		if (trans == null)
			trans = gameObject.transform;
	}

	private Coroutine moveToCoroutineValue;
	public void moveTo(Vector3 value){
		if (moveToCoroutineValue != null) {
			StopCoroutine (moveToCoroutineValue);
			moveToCoroutineValue = null;
		}
		if (smoothing)
			moveToCoroutineValue = StartCoroutine (moveToCoroutine (value, 1 / (sendRate+1), trans));
		else
			trans.position = value;
	}

	private IEnumerator moveToCoroutine(Vector3 value, float interpolTime, Transform trans){
		float interpol = (0.01f / interpolTime);
		interpol = Mathf.Clamp (interpol, 0.01f, 0.8f);
		while (trans.position != value) {
			trans.position = Vector3.Slerp (trans.position, value, interpol * Time.deltaTime * 60);
			yield return null;
		}
	}

	private Coroutine rotateToCoroutineValue;
	public void rotateTo(Quaternion value){
		if (rotateToCoroutineValue != null) {
			StopCoroutine (rotateToCoroutineValue);
			rotateToCoroutineValue = null;
		}
		if (smoothing)
			rotateToCoroutineValue = StartCoroutine (rotateToCoroutine (value, 1 / (sendRate+1), trans));
		else
			trans.rotation = value;
	}

	private IEnumerator rotateToCoroutine(Quaternion value, float interpolTime, Transform trans){
		float interpol = (0.01f / interpolTime);
		interpol = Mathf.Clamp (interpol, 0.01f, 0.5f);
		while (trans.rotation != value) {
			trans.rotation = Quaternion.Slerp (trans.rotation, value, interpol * Time.deltaTime * 60);
			yield return null;
		}
	}

//	public void OnSlider(){
//		sendRate = (int)slider.value;
//	}


}