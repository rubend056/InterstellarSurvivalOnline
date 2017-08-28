//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//
//public class UIController : MonoBehaviour {
//
//	public static UIController instance;
//
//	public List<GameObject> screens;
//
//	[Header("JoinMenu")]
//	public GameObject joinMenu;
//	public InputField ipField;
//	public InputField localPortField;
//	public InputField remotePortField;
//
//	[Space(3)]
//	[Header("PlayerSpawnMenu")]
//	public GameObject spawnMenu;
//
//	[Space(3)]
//	[Header("General")]
//	public Slider speedSlider;
//	// Use this for initialization
//
//	void Awake(){
//		instance = this;
//	}
//	public void hostButton(){
//		NetTransportCustom.instance.startAsServer ();
//	}
//	public void joinButton(){
//		NetTransportCustom.instance.startAsClient ();
//	}
//	public void changedIP(){
//		string ip = ipField.text;
//		NetTransportCustom.instance.ipAddress = ip;
//	}
//	public void changedLocalPort(){
//		string portString = localPortField.text;
//		int port;
//		int.TryParse (portString, out port);
//		NetTransportCustom.instance.localPort = port;
//	}
//	public void changedRemotePort(){
//		string portString = remotePortField.text;
//		int port;
//		int.TryParse (portString, out port);
//		NetTransportCustom.instance.remotePort = port;
//	}
//	public void speedChanged(){
//		Time.timeScale = speedSlider.value;
//	}
//
//	public void spawnPlayer(){
//		var sManager = CameraControlAdva.instance.toFollow.GetComponent<SpawningManager> ();
//		if (sManager != null && sManager.availableSpawns.Count > 0) {
//			int randomVal = Random.Range (0, sManager.availableSpawns.Count);
//			var spawnInfo = sManager.availableSpawns [randomVal];
//			var playerInstance = (GameObject)Instantiate (UniverseManager.instance.playerPrefab, spawnInfo.position, spawnInfo.rotation);
//
//			PlayerController.instance.changePlayerInstance (playerInstance);
//			PlayerController.instance.planet = CameraControlAdva.instance.toFollow.transform;
//			CameraControlAdva.instance.changeFollow (playerInstance);
//			CameraControlAdva.instance.useTargetOrientation (true);
//		}
//	}
//
//	public void onConnChanged(bool state){
//		joinMenu.SetActive (!state);
//	}
//
//	public void togglePlayerSpawner(bool value){
//		spawnMenu.SetActive (value);
//
//		if(!NetTransportCustom.instance.isConnected())
//			joinMenu.SetActive (!value);
//	}
//}