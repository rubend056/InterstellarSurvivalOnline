using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	//public PlayerController.Continent continent = PlayerController.Continent.NorthAmerica;
	private const float minSpeed = 3;
	private const float maxSpeed = 80;
	private AudioSource audioSource;
	private Rigidbody rb;
	public float velocity;
	private float minSpeedSqr = minSpeed * minSpeed;
	private float maxSpeedSqr = maxSpeed * maxSpeed;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
		audioSource = gameObject.GetComponent<AudioSource> ();
		//gameObject.GetComponent<TrailRenderer> ().Clear ();
	}
	
	// Update is called once per frame

	void Update () {
		velocity = rb.velocity.sqrMagnitude;
		if (velocity > minSpeedSqr) {
			velocity -= minSpeedSqr;
			velocity /= (maxSpeedSqr - minSpeedSqr);
			if (velocity > 1)
				velocity = 1;
			
			audioSource.volume = velocity;
		} else audioSource.volume = 0;
	}
}
