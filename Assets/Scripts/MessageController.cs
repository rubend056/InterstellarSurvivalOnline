//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using System.Text;
//
//
//public class MessageController : MonoBehaviour {
//	public InputField inputField;
//
//	// Use this for initialization
//	void Start () {
//		
//	}
//	
//	// Update is called once per frame
//	void Update(){
//		if (Input.GetKeyDown (KeyCode.Return)) {
//			if (inputField.text != "") {
//				List<byte> toSend = new List<byte> ();
//				toSend.AddRange (System.BitConverter.GetBytes((int)NetObjectSync.DataType.TextMessage));
//				toSend.AddRange(System.Text.Encoding.ASCII.GetBytes(inputField.text));
//				inputField.text = "";
//				NetTransportManager.instance.sendToConnected (toSend.ToArray(),NetTransportManager.reliableChannel);
//			}
//		}
//	}
//}
