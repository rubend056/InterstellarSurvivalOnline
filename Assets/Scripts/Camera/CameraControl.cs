using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public enum FollowMode {Static, Planetary};
	public FollowMode followMode;

	//LookAt Values
	public Transform target;
	public GameObject planet;
	public float RotationSpeed;
	public float translationSpeed;
	public float offsetY;

	//values for internal use
	private Quaternion _lookRotation;
	private Vector3 _direction;

	float height;
	float diff;

	float actualZoom = 3f;
	float desiredZoom = 3f;
	float maxZoom = 3f;
	float minZoom = 10f;

	//Text text;

	// Use this for initialization
	void Start () {
		//text = GameObject.Find("Canvas").GetComponent (typeof(Text)) as Text;

		Vector3 dist3 = target.position - gameObject.transform.position;
		diff = dist3.magnitude;
		if (followMode == FollowMode.Planetary) {
			dist3 = planet.transform.position - gameObject.transform.position;
			height = dist3.magnitude;
		}

		/*Debug.Log ("Squared Magnitude :" + dist3.sqrMagnitude);
		Debug.Log ("Magnitude :" + dist3.magnitude);
		Debug.Log ("Actual Value :" + dist3);*/
	}
	
	// Update is called once per frame
	void Update () {
		zoom ();
		lookAt ();
		positionUpdate ();
	}

	void positionUpdate(){
		float currentDifference = (target.position - gameObject.transform.position).magnitude;
		/*if (followMode == FollowMode.Planetary) {
			if (currentDifference > diff * actualZoom) {
				Vector3 f = target.position - (currentDifference * 0.995f);
				transform.position = getHeight (f);
				Debug.Log ("JustMoved" + currentDifference);
			} else {
				transform.position = getHeight (transform.position);
			}
		} else {*/
			if (currentDifference > diff * actualZoom) {
				Vector3 toGo = target.position - transform.position;
				toGo.y += offsetY;
				toGo = toGo * translationSpeed + transform.position;
				transform.position = toGo;
			}
		//}
	}


	Vector3 getHeight(Vector3 toCalculate){
		Vector3 reference = (toCalculate - planet.transform.position);
		reference.Normalize ();
		Vector3 whereTo = planet.transform.position + (reference * height);
		return whereTo;
	}



	void zoom(){
		if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			desiredZoom = desiredZoom *  0.8f;
			if (desiredZoom < maxZoom)desiredZoom = maxZoom;
		} else if (Input.GetAxis ("Mouse ScrollWheel") < 0){
			desiredZoom = desiredZoom *  1.2f;
			if (desiredZoom > minZoom)desiredZoom = minZoom;
		}

		if (!(actualZoom == desiredZoom)) {
			actualZoom -= ((actualZoom - desiredZoom) * 0.03f);
			if (Mathf.Abs (actualZoom - desiredZoom) < 0.002f)
				actualZoom = desiredZoom;
			diff = actualZoom;
		}
		//text.text = "Difference :" + diff;
	}
	void lookAt(){
		_direction = (target.transform.position - transform.position).normalized;

		//create the rotation we need to be in to look at the target

		Vector3 reference;
		if (followMode == FollowMode.Planetary) {
			reference = (target.position - planet.transform.position);
			reference.Normalize ();
		} else 
			reference = Vector3.up;

		_lookRotation = Quaternion.LookRotation(_direction, reference);

		//rotate us over time according to speed until we are in the required rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
	}

}
	