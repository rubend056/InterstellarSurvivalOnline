using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Gravity))]
public class gravityMultiplier : MonoBehaviour {

	void Start(){
		//multiplier = gameObject.GetComponent<Rigidbody> ().mass/10;
		gravityMultiplier.multiplier = 600;
	}

	public static float multiplier = 1;//{
		//get{ return multiplier;}
		//set{ gameObject.GetComponent<Rigidbody> ().mass = value;multiplier = value;}
	//}

}
