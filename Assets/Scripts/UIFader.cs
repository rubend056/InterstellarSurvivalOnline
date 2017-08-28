using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFader : MonoBehaviour {

	public MaskableGraphic toFade;
	public bool showOnStart = true;
	public float startAlpha = 1;
	// Use this for initialization
	void Start () {
		Color color = toFade.color;color.a = 0;
		toFade.color = color;
		if (showOnStart) {
			showWithDefaults ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void showWithDefaults(){
		changeAlpha (startAlpha);
	}

	private Coroutine coroutine;
	public void changeAlpha(float alpha, float interpolation = 2){
		alpha = Mathf.Clamp01 (alpha);
		if (coroutine != null)
			StopCoroutine (coroutine);
		coroutine = StartCoroutine (changeAlphaCoroutine ( toFade, alpha, interpolation));
	}

	private IEnumerator changeAlphaCoroutine(MaskableGraphic toFade, float alpha, float interpolation){
		while(toFade.color.a != alpha){ 
			Color color = toFade.color;
			color.a = Mathf.MoveTowards (color.a, alpha, Time.deltaTime *(Mathf.Abs(alpha-color.a)/interpolation));
			toFade.color = color;
			yield return null;
		}
	}
}
