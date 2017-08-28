using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickerController : MonoBehaviour {

	public MaskableGraphic[] graphics;
	public Color HColor;
	private Color normalColor;
	// Use this for initialization
	void Start () {
		if (graphics.Length > 0) {
			normalColor = graphics [0].color;
			graphics [0].color = HColor;
		}
	}

	public void updatePick(int what){
		for (int i = 0;i<graphics.Length;i++){
			graphics[i].color = normalColor;
		}
		graphics [what].color = HColor;
	}
}
