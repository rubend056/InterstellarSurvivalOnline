using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour {

	// Use this for initialization
	public float lumberAmmount = 20;
	public float startSizeBase = 1;
	//public float startSizeDeviation = 0.2f;
	public float depletedSize = 0.1f;
	public float maxSize = 1.5f;
	public float lumberGrowRate = 0.1f;

	public bool wasBorn = false;
	public bool grow = true;

	private float size;
	private Transform treeTransform;
	void Start () {
		treeTransform = gameObject.transform;
		if (wasBorn)// {
			size = 0;
		//} else {
			//float deviation = Random.Range (-startSizeDeviation, startSizeDeviation);
			//size = startSizeBase + startSizeDeviation;
		//}
		size = 1;
		updateTree (size);
	}

	public float suck(float ammount){
		float toReturn = 0;
		ammount /= lumberAmmount;
		if (size - ammount > 0) {
			size -= ammount;
			toReturn = ammount;
		} else {
			toReturn = size;
			size = 0;
		}
		updateTree (size);
		return toReturn*lumberAmmount;
	}

	public bool depleted(){
		return (size < depletedSize);
	}

	void updateTree(float size){
		treeTransform.localScale = new Vector3 (size, size, size);
	}
}
