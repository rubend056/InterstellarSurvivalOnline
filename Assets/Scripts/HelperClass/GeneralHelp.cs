using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralHelp : MonoBehaviour {

	public delegate void GODelegate(GameObject gO);

	public static void searchHierarchy(GODelegate toCall, GameObject gO){
		toCall (gO);
		int i = 0;
		while (gO.transform.parent != null && i<10) {
			gO = gO.transform.parent.gameObject;
			toCall (gO);
			i++;
		}
	}

	public static int[] deleteFromIntArray(int[] sourceArray, int[] exeptions){
		List<int> connIDs = new List<int> ();
		for (int i = 0; i < sourceArray.Length; i++) {
			bool check = true;
			for (int e = 0; e < exeptions.Length; e++) {
				if (sourceArray [i] == exeptions [e]) {
					check = false;
					break;
				}
			}
			if(check)
				connIDs.Add(sourceArray [i]);
		}
		return connIDs.ToArray();
	}
}
