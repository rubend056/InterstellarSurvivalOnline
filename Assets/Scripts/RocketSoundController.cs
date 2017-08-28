using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSoundController : MonoBehaviour {

	public AudioSource audioSource;
	public ParticleSystem ps;
	private ParticleSystem.EmissionModule pe;
	// Use this for initialization
	void Start () {
		if (!ps)
			ps = gameObject.GetComponent<ParticleSystem> ();
		if (!audioSource)
			audioSource = gameObject.GetComponent<AudioSource> ();
		pe = ps.emission;
	}
	
	// Update is called once per frame
	void Update () {
		pe = ps.emission;
		float emissionRate = pe.rateOverTimeMultiplier;
		audioSource.volume = emissionRate / 25;
		if (emissionRate == 0 && audioSource.isPlaying)
			audioSource.Stop ();
		else if (emissionRate > 0 && !audioSource.isPlaying)
			audioSource.Play ();
	}
}
