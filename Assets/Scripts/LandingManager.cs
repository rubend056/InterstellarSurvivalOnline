using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;

public class LandingManager : MonoBehaviour {

	public GameObject[] toActivateOnStart;
	public GameObject[] toActivateOnStop;
	public GameObject[] makeParentNull;
	public GameObject player;
	public GameObject cam;
	public Animator anim;
	public float animationTime = 30;
	private bool started = false;
	private float time = 0;
	private NetIdentityCustom ni;
	// Use this for initialization
	void Start () {
		ni = gameObject.GetComponent<NetIdentityCustom> ();
		if (ni.HasAuthority) {
			List<SpawningInfo> availableSpawns = GameObject.Find ("PlayerSpawnHolder").GetComponent<SpawningManager> ().availableSpawns;
			int randomNumber = (int)(Random.value * availableSpawns.Count);
			gameObject.transform.position = availableSpawns[randomNumber].trans.TransformPoint (availableSpawns [randomNumber].position);
			gameObject.transform.rotation = Quaternion.LookRotation(availableSpawns[randomNumber].trans.TransformDirection( availableSpawns [randomNumber].rotation));
		}
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		startAnimation ();
	}
	
	// Update is called once per frame
	private bool done = false;
	void Update () {
		if (started) 
			time += Time.deltaTime;
		
		if (time >= animationTime && !done) {
			started = false;
			done = true;
			activateGObjects (toActivateOnStop, true);
			nullGObjects (makeParentNull);
			if (ni.HasAuthority) {
				CameraControlAdva CCA = cam.GetComponent<CameraControlAdva> ();
				CCA.changeFollow (player.gameObject);
				//CCA.invert = false;
				CCA.yOffset = 1f;
				cam.GetComponent<SmoothLookAtC> ().target = player.transform;
			}
		}
	}


	public void startAnimation(){
		anim.enabled = true;
		started = true;
		activateGObjects (toActivateOnStart, true);
		cam.GetComponent<CameraControlAdva> ().enabled = true;
		cam.GetComponent<SmoothLookAtC> ().enabled = true;
	}

	void activateGObjects(GameObject[] gOs, bool active){
		for (int i = 0; i < gOs.Length; i++) {
			gOs[i].SetActive(active);
		}
	}
	void nullGObjects(GameObject[] gOs){
		for (int i = 0; i < gOs.Length; i++) {
			gOs[i].transform.parent = null;
		}
	}

}
