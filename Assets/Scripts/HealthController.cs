//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//
//public class HealthController : MonoBehaviour {
//
//	public const float maxHealth = 100;
//	[Range(1f,30f)]
//	public float healthMultiplier = 1f;
//	[Space(5)]
//	public bool regenerate = false;
//	public float regAmmount = 0.1f;
//	[Space(5)]
//	public int deathTimeSec = 6;
//	private bool dead = false;
//	public float health = maxHealth;
//	public RectTransform healthBar;
//	private NetIdentityCustom ni;
//
//	void Start(){
//		ni = gameObject.GetComponent<NetIdentityCustom> ();
//	}
//
//	void Update(){
//		
//		debugging ();
//		if (!NetIdentityCustom.server)
//			return;
//
//		if (regenerate && health < maxHealth) {
//			health += regAmmount * Time.deltaTime;
//			if (health > maxHealth)
//				health = maxHealth;
//		}
//	}
//		
//	void debugging(){
//		if (!NetIdentityCustom.client)
//			return;
//		if (Input.GetKeyDown (KeyCode.H)) {
//			
//			NetHealth.instance.doDamage (ni.objectID,5);
//		}
//	}
//
//
//	public void receiveDamage(float ammount){
//		health -= ammount / healthMultiplier;
//		if (isDead ()) {
//			health = 0;
//		}
//	}
//
//	public bool isDead(){
//		return (health <= 0);
//	}
//
//	public void CmdSetRegen(bool value, float ammount){
//		regenerate = value;
//		regAmmount = ammount;
//	}
//		
//	public void onChangeHealth(float healthLocal){
//		health = healthLocal;
//		UIUtility.changeDimentions (healthBar, 0, 200 - (health * 2), 0, 0);
//
//		if (ni.localPlayer)
//		if (isDead ())
//			deadAction();
//	}
//
//
//	public void deadAction(){
//		if (dead)
//			return;
//
//		GameObject cam = GameObject.FindGameObjectWithTag ("MainCamera");
//		PlayerController pc = gameObject.GetComponent<PlayerController> ();
//		PointerPick pp = gameObject.GetComponent<PointerPick> ();
//		CameraControlAdva cca = cam.GetComponent<CameraControlAdva> ();
//		SmoothLookAtC slac = cam.GetComponent<SmoothLookAtC> ();
//
//		pc.enabled = false;
//		pp.enabled = false;
//		cca.togglePlanetView(true);
//		slac.target = slac.planet;
////		CmdWhenDead (CustomNMUI.instance.playerName);
//		Notifier.instance.notify ("YouLost", Color.red, deathTimeSec);
//		gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
//		dead = true;
//		StartCoroutine(afterDeathWait(deathTimeSec));
//	}
//
//	private IEnumerator afterDeathWait(float wait){
//		yield return new WaitForSeconds (wait);
////		CmdSetInactive ();
//	}
//
////	[Command]
////	void CmdWhenDead(string playerName){
////		if (CustomNMUI.instance.playerName == playerName) {
////			Notifier.instance.notify ("You have died", Color.red, 150f);
////		} else Notifier.instance.notify ( playerName + " has died", Color.red,5f);
////		gameObject.GetComponent<AudioSource> ().Play ();
////		RpcWhenDead (playerName);
////	}
////
//////	[ClientRpc]
////	void RpcWhenDead(string playerName){
////		if (CustomNMUI.instance.playerName == playerName) {
////			Notifier.instance.notify ("You have died", Color.red,150f);
////		} else Notifier.instance.notify ( playerName + " has died", Color.red,5f);
////		gameObject.GetComponent<AudioSource> ().Play ();
////	}
////
//////	[Command]
////	void CmdSetInactive(){
////		gameObject.SetActive (false);
////		Communication.instance.RpcUpdateMap ();
////		RpcSetInactive ();
////	}
//////	[ClientRpc]
////	void RpcSetInactive(){
////		gameObject.SetActive (false);
////		Communication.instance.RpcUpdateMap ();
////	}
//}
