using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrowing : MonoBehaviour {

	public float growingSpeed = 0.05f; //scale/sec
	public float maxSize = 1;

	private float scale = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(scale < maxSize)
			scale += scale * growingSpeed * Time.deltaTime;
		if (scale > maxSize)
			scale = maxSize;
		gameObject.transform.localScale = new Vector3 (scale, scale, scale);
	}
}
