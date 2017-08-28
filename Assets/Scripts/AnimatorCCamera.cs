using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCCamera : MonoBehaviour {

	public Animator animator;
	private float time;
	private float count;
	void Start () {
		if (animator==null)
			animator = gameObject.GetComponent (typeof(Animator)) as Animator;
		animator.StartPlayback ();
		time = animator.playbackTime;
	}
	
	// Update is called once per fram
	void Update () {
		if (count < time-1)
			count = Time.time;
		else
			animator.StopPlayback ();

	}
}
