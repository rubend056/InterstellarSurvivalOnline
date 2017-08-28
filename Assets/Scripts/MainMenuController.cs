using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour {
	private Animator anim;
	public AudioSource audioSource;

	public Transform afterTrans;

	public Transform menuTrans;
	private Vector3 initialMenuPos;
	private int exitHash = Animator.StringToHash ("Base Layer.Exit");
	private int caHash = Animator.StringToHash("Base Layer.CameraAnimation");

	public float maxSpeed = 10;
	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator> ();
		initialMenuPos = menuTrans.localPosition;
	}

	private bool speedChanged = false;
	private bool check = false;
	private bool menuState = false;
	// Update is called once per frame
	void Update () {
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo (0);
		//RuntimeAnimatorController rac = anim.runtimeAnimatorController;
		if (!menuState) {
			if (stateInfo.fullPathHash == caHash && stateInfo.normalizedTime >= 0.9999f) {
				gameObject.transform.parent = afterTrans;
				PlanetRotator pr = afterTrans.GetComponent<PlanetRotator> ();
				if (pr != null)
					pr.changeSpeed (new Vector3 (0, -0.03f, 0), 10);
				Vector3 pos = gameObject.transform.position;
				anim.StopPlayback ();
				//anim.Stop ();
				gameObject.transform.position = pos;

				//Move the menu Foward
				StartCoroutine (changePositionCoroutine (new Vector3 (0, 1.1f, 5.8f), 6));
				menuState = true;
			} else if (speedChanged && (stateInfo.fullPathHash == caHash && stateInfo.normalizedTime >= 0.90f)) {
				if (!check) {
					changeSpeed (1f, 0.6f);
					check = true;
				}
			} else if (Input.GetKeyUp (KeyCode.Escape) || Input.GetKeyUp (KeyCode.Space)) {
				changeSpeed (maxSpeed, 4);
				speedChanged = true;
				Debug.Log ("SpeedChanged");
			}
		} else {
			RaycastWorldUI ();
		}

	}

	public void restart(){
		StopAllCoroutines ();
		menuTrans.localPosition = initialMenuPos;
		audioSource.pitch = 1;
		if (audioSource.isPlaying)
			audioSource.time = 0;
		else
			audioSource.Play ();

		speedChanged = false;
		check = false;
		menuState = false;
		gameObject.transform.parent.GetComponent<PlanetRotator> ().restart ();
		gameObject.transform.parent = null;

		anim.Rebind ();
		anim.Play ("CameraAnimation");
	}

	private Coroutine coroutine;
	void changeSpeed(float speed, float interpolation){
		if (coroutine != null)
			StopCoroutine (coroutine);
		coroutine = StartCoroutine (changeSpeedCoroutine (speed, interpolation));
	}

	private IEnumerator changeSpeedCoroutine(float speed, float interpolation){
		while(anim.GetFloat("Speed2") != speed){ 
			float animSpeed = anim.GetFloat("Speed2");
			anim.SetFloat( "Speed2", Mathf.MoveTowards (animSpeed, speed, Time.deltaTime *(Mathf.Abs(speed-animSpeed)/interpolation)));
			animSpeed = (((animSpeed-1) / (maxSpeed-1)) * 0.5f) + 1;
			animSpeed = Mathf.Clamp (animSpeed, 1f, 3f);
			audioSource.pitch = animSpeed;
			yield return null;
		}
	}

	private IEnumerator changePositionCoroutine( Vector3 position, float interpolation){
		while(menuTrans.localPosition != position){
			menuTrans.localPosition = Vector3.MoveTowards (menuTrans.localPosition, position,  Time.deltaTime *(Mathf.Abs(Mathf.Sqrt(position.sqrMagnitude-menuTrans.localPosition.sqrMagnitude))/interpolation));
			yield return null;
		}
	}


	bool RaycastWorldUI(){

		PointerEventData pointerData = new PointerEventData (EventSystem.current);

		pointerData.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

		List<RaycastResult> results = new List<RaycastResult> ();
		EventSystem.current.RaycastAll (pointerData, results);

		if (results.Count > 0) {
			string hitTag = results [0].gameObject.tag;

			if (hitTag == "BuildingUI" || hitTag == "BuildingUII") {
				if (hitTag == "BuildingUII")
					results [0].gameObject.SendMessage ("beingHit");
				return true;

			} else if (results.Count > 1) {
				string hitTag2 = results [1].gameObject.tag;
				if (hitTag2 == "BuildingUI" || hitTag2 == "BuildingUII") {
					if (hitTag2 == "BuildingUII")
						results [0].gameObject.SendMessage ("beingHit");
					return true;
				}
			} else {
				//Debug.Log (hitTag);
			}
		}
		return false;
	}

}
