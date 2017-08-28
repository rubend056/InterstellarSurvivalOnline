using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorController : MonoBehaviour {

	public Animator anim;
	public Text doorText;

	public string openedText = "Close";
	public string closedText = "Open";
	private bool open = false;

	// Use this for initialization
	void Start () {
		if (!doorText)
			doorText = gameObject.GetComponent<Text> ();
	}

	void Update(){
		if (anim.GetBool ("Open") != open) updateState (!open);
	}

	public void switchState(){
		open = !open;
		anim.SetBool ("Open", open);
		if (open)
			doorText.text = openedText;
		else
			doorText.text = closedText;
	}

	public void updateState(bool state){
		open = state;
		if (open)
			doorText.text = openedText;
		else
			doorText.text = closedText;
	}
}
