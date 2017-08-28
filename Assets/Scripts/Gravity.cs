﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gravity : MonoBehaviour {

	//private GameObject[] planets;
	[Range(0.3f,2f)]
	public float divisionFactor = 1;
	public float forceP;
//	public float distance;
	public const float gravityConst = 0.0000000000667408f * 10000000000;

	Transform thisTransform;
	Rigidbody thisRigidBody;
	private planetsGInfo[] planetInfoArray;

	void Start(){
		planetInfoArray = new planetsGInfo[0];
		thisTransform = gameObject.transform;
		thisRigidBody = gameObject.GetComponent (typeof(Rigidbody)) as Rigidbody;

	}
	// Update is called once per frame
	void Update () {
		updatePlanets ();
		forceP = 0;
		foreach (planetsGInfo planetR in planetInfoArray) {
			
			Vector3 direction = planetR.dir;


			float force = Time.deltaTime * 60 *  getGravityForce(planetR.rb.mass, thisRigidBody.mass, planetR.distSqr);/*Sphere.getFictionalForce(gravityMultiplier.multiplier * planetR.rb.mass,distance) * divisionFactor*/;
			Vector3 forceVector = direction * force;

			thisRigidBody.AddForce (forceVector, ForceMode.Force);

			forceP += force;
		}
	}

	public void updatePlanets(){
		
		GameObject[] planets = GameObject.FindGameObjectsWithTag("planetRB");

		List<GameObject> planetsL = new List<GameObject> ();		
		planetsL.AddRange (planets);

		if (planetsL.Contains (this.gameObject))//If any of these gameObjects is youself, then remove yourself
			planetsL.Remove (this.gameObject);

		planets = planetsL.ToArray ();

		planetInfoArray = new planetsGInfo[planets.Length];
		for (int i = 0; i < planetInfoArray.Length; i++) {
			var planetInfo = new planetsGInfo ();

			planetInfo.rb = planets [i].GetComponent<Rigidbody> ();

			planetInfo.distSqr = (planets[i].transform.position - thisTransform.position).sqrMagnitude;

			Vector3 direction = (planetInfo.rb.transform.position - thisTransform.position).normalized;
			planetInfo.dir = direction;

			planetInfoArray [i] = planetInfo;
		}
		//waitTime = /*Mathf.Clamp (2/((thisRigidBody.velocity.magnitude*0.01f)+1),0.1f,1f)*/0.01f;
	}

	public static float getOrbitSpeedByCentripetal(float mass, float centripetal, float radius){
		return Mathf.Sqrt ((centripetal * radius) / mass); //Inverse of getCentrifugalBySpeed function
	}

	public static float getCentrifugalBySpeed(float mass, float speed, float radius){
		return (mass * (speed * speed)) / radius;
	}

	public static float getGravityForce(float obj1Mass, float obj2Mass, float distanceSqrd){
		return gravityConst * ((obj1Mass * obj2Mass) / distanceSqrd);
	}

	private class planetsGInfo{
		public Rigidbody rb;
		public Vector3 dir;
		public float distSqr;
	}
}
