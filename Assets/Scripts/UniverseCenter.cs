﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UniverseCenter : MonoBehaviour{
	public static UniverseCenter instance;

	public Transform universeCenter;
	private bool centerUniverse = false;
	public float snapDistance = 1000000;
	private Vector3 shiftedPos;
	//private Vector3 shiftedRot;

	private List<Transform> allOthers;
	public List<LineRenderer> trenderers;
	private GameObject master;
	void Awake(){
		instance = this;
		allOthers = new List<Transform> ();
		trenderers = new List<LineRenderer> ();
	}

	void Start(){
		shiftedPos = Vector3.zero;
		//shiftedRot = Vector3.zero;
	}

	//private int i = 0;
	void Update(){
		if (centerUniverse && universeCenter != null) {
			
			if (universeCenter.position.sqrMagnitude > snapDistance) {
				shiftedPos += universeCenter.position;
				//hiftedRot += universeCenter.rotation.eulerAngles;
				shiftEverything (-universeCenter.position);
			}
		}
	}

	void OnCenterOn(){
		UpdateAllOthers ();
		shiftedPos = universeCenter.position;
		//shiftedRot = universeCenter.eulerAngles;

		master = new GameObject();
		master.transform.position = shiftedPos;
		//master.transform.rotation = Quaternion.Euler(shiftedRot);
		foreach (Transform trans in allOthers) {
			trans.parent = master.transform;
		}
		shiftEverything (-shiftedPos);
		//master.transform.rotation = Quaternion.identity;
	}
	void OnCenterOff(){
		
		shiftEverything(-shiftedPos);
		shiftedPos = Vector3.zero;
		//master.transform.rotation = Quaternion.Euler(shiftedRot);
		foreach (Transform trans in allOthers) {
			trans.parent = null;
		}
		GameObject.Destroy (master);

	}

	void UpdateAllOthers(){
		allOthers.Clear ();
		GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>() ;
		for (int i = 0; i < allObjects.Length; i++) {
			if (allObjects [i].activeInHierarchy && allObjects[i].transform.parent == null) {
				allOthers.Add (allObjects [i].transform);
			}
		}
	}

	void shiftEverything(Vector3 ammount/*, Vector3 rotammount*/){
		
		master.transform.position += ammount;
		//master.transform.eulerAngles += rotammount;

		for (int i = 0; i < trenderers.Count; i++) {
			Vector3[] somethings = new Vector3[trenderers[i].positionCount];trenderers[i].GetPositions(somethings);
			for (int e = 0; e < somethings.Length; e++) {
				somethings [e] += ammount;
			}
			trenderers[i].SetPositions(somethings);
		}
	}

	public void switchCenter(){
		centerUniverse = !centerUniverse;
		if (centerUniverse)
			OnCenterOn ();
		else
			OnCenterOff ();
	}

	public bool isCenter(){
		return centerUniverse;
	}
}