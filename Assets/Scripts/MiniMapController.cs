using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapController : MonoBehaviour {

	public static MiniMapController instance;
	void Awake(){
		instance = this;
	}

	// Use this for initialization
	private List<PlayerInfo> playersInfo;
	private GameObject[] playerObjects;

	public GameObject playerMarkerPrefab;
	void Start () {
		destroyChildren ();
		playersInfo = new List<PlayerInfo> ();
		//networkManager = GameObject.Find ("NetworkManager").GetComponent<NetworkInfo> ();
		//UpdatePlayerCount ();
	}
		
	public void UpdatePlayerCount () {
		//if (lastPlayerCount != playerCount) {
		if (playersInfo == null)playersInfo = new List<PlayerInfo>();
			playersInfo.Clear ();
			playerObjects = GameObject.FindGameObjectsWithTag ("Player");
			foreach(GameObject gO in playerObjects){
				PlayerInfo playerInfo = new PlayerInfo ();
				playerInfo.controller = (gO.GetComponent<PlayerController> ());
				playerInfo.mapCoordinate = Vector2.zero;
				playersInfo.Add (playerInfo);
			}
		UIUtility.changeDimentions (gameObject.GetComponent<RectTransform> (), 5, 5, 5, 5);
		//}

		//lastPlayerCount = playerCount;
	}

	private float count = 0;
	void Update(){
		count += Time.deltaTime;
		if (count > 1f) {
			updatePlayerPositions ();
			updateMap ();
			count = 0f;
		}
	}

	public void updateMap(){
		destroyChildren ();
		Rect rectangle = gameObject.GetComponent<RectTransform> ().rect;
		for(int i = 0;i<playersInfo.Count;i++){
			PlayerInfo info = playersInfo [i];
			GameObject playerUI = (GameObject)Instantiate (playerMarkerPrefab,gameObject.transform);
			info.playerMarker = playerUI;
			RectTransform rectT = playerUI.GetComponent<RectTransform> ();
			Vector3 lPos = rectT.anchoredPosition3D;
			lPos.x = rectangle.width * info.mapCoordinate.x;
			lPos.y = rectangle.height * info.mapCoordinate.y;
			rectT.anchoredPosition3D = lPos;

			info.playerImage = playerUI.GetComponent<Image> ();
			info.playerImage.color = getColorBasedOnContinent ();
			playersInfo [i] = info;
		}

	}

	public void updatePlayerPositions(){
		for(int i = 0;i<playersInfo.Count;i++){
			PlayerInfo info = playersInfo [i];

			if (info.controller == null) {
				UpdatePlayerCount ();
				return;
			}
			Vector2 angles = Sphere.getByPosition (info.controller.transform.position);
			angles.x /= 360;
			angles.y /= 180;
			info.mapCoordinate = angles;
			playersInfo [i] = info;
		}
	}

	void destroyChildren(){
		int count = gameObject.transform.childCount;
		for (int i = 0; i<count;i++)
			GameObject.DestroyImmediate(gameObject.transform.GetChild (0).gameObject);
	}


	public Color getColorBasedOnContinent(/*PlayerController.Continent continent*/){
//		switch (continent) {
//		case PlayerController.Continent.NorthAmerica:
//			return Color.green;
//		case PlayerController.Continent.SouthAmerica:
//			return Color.yellow;
//		case PlayerController.Continent.Asia:
//			return Color.red;
//		case PlayerController.Continent.Antartica:
//			return Color.magenta;
//		case PlayerController.Continent.Europe:
//			return Color.cyan;
//		case PlayerController.Continent.Africa:
//			return Color.gray;
//		case PlayerController.Continent.Australia:
//			return Color.blue;
//		}
		return Color.blue;
	}

	private struct PlayerInfo{
		public PlayerController controller;
		public Vector2 mapCoordinate;
		public GameObject playerMarker;
		public Image playerImage;
	}
}
