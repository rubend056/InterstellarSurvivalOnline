
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMDManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string[] args = System.Environment.GetCommandLineArgs ();
		string otherargs = "";

		//Loop though all the arguments
		for (int i = 0; i < args.Length; i++) {
			//if the argument starts with a '+'
			if (args [i] [0] == '+') {
				
				for (int e = 0; i < args [i].Length-1; e++) {
					otherargs += args [i] [e+1];
				}

				switch (otherargs[0]) {
				case 's':
					//Means it's a server, start as a server, HEADLESS :)
					int localPort;
					int.TryParse (args [i + 1], out localPort);
					startAsServer (localPort);
					break;

				}
				//Loop through the argument adding every command to the otherargs

			}
		}

//		if (Application.platform == RuntimePlatform.Android) {
//			StartCoroutine (startServerLate ());
//		}
	}
	void startAsServer(int port){
		NetTransportManager.instance.localPort = port;
		NetTransportManager.instance.startAsServer ();
	}
//
//	private IEnumerator startServerLate(){
//		yield return new WaitForSeconds (3);
//		NetTransportManager.instance.startAsServer ();
//	}
	// Update is called once per frame
}
