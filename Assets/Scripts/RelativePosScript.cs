using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativePosScript : MonoBehaviour {
	public Transform relativeToWhat;
	private Vector3 posOffset;
	// Use this for initialization
	void Start () {
		posOffset = relativeToWhat.position - gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = relativeToWhat.position - posOffset;
	}
}
