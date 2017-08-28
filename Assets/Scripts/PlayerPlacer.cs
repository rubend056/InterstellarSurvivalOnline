using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlacer : MonoBehaviour {

	public ObjectPlacer objPlacer;
	public Vector3 whereWeAreStarting;
	public PlanetGenerator3 pg3;
	public GameObject generator;
	public GameObject car;
	public GameObject character;
	private ObjSpawningHelper os;

	public Color desColor;
	private Color[] colors;
	private Mesh mesh;
	// Use this for initialization
	/*void Start () {
		if (pg3 != null && colors != null && mesh != null) {
			mesh = gameObject.GetComponent<MeshFilter> ().mesh;
			colors = pg3.planetInfo.colorArray [indexOfPlacing];
			os.updatePos (mesh, colors, desColor);

			placeEverything ();
		}
	}
	
	
	// Update is called once per frame
	void Update () {
		
	}

	void placeEverything(){
		Transform myself = gameObject.transform;
		if (os.getAvalPos().Length >= 5) {
			//os.spawnObject (generator, myself, 0);
			//os.spawnObject (car, myself, 2);
			//pg3.player = os.spawnObject (character, myself, 4).transform;
		} else
			Debug.Log ("Not Enough Space");
	}*/
}

