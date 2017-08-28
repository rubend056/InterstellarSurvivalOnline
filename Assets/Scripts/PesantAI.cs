using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PesantAI : MonoBehaviour {

	public enum PesantType {TreePicker, LaserRobot, Snake};
	public enum State{iddle, walking, chasing, building, attacking};

	public PesantType pesantType = PesantType.TreePicker;
	public bool enemy = false;
	private State state = State.iddle;

	[Space(5)]
	[Header("General Variables")]
	public float checkDistance = 0.1f;
	public float moveForce = 1000;
	public float maxSpeed = 5;
	public Animator anim;
	public Rigidbody ownRigidBody;
	public Transform ownTransform;
	public Transform torsoTransform;
	public Transform planet;
	public float moveRayLength = 0.1f;

	[Space(5)]
	[Header("ForCollector")]
	public bool work = false;
	public float full = 0;
	public float suckAmmount = 2;

	private BuildingController buildingController;
	private TreeController treeController;
	private Vector3 pickupPos;
	private Vector3 dropoffPos;
	private Vector2 pickup;
	private Vector2 dropoff;

	[Space(5)]
	[Header("ForLaserRobot")]
	public Transform laserObject;
	public Transform robotHead;

	[Space(5)]
	[Header("ForEnemies")]
	public float attackDistance = 5;
	public float chaseDistance = 9;
	private Vector3 targetPos;
	private Vector2 targetPos2;



	[Space(5)]
	[Header("Status")]
	public Vector2 ownPosition;

	public float yOffset = 0;
	public float speed;

	public float distanceToCheckpoint;
	private Transform targetTrans;
	private Vector3 goToPos = Vector3.zero;
	private Vector2 goToPos2 = Vector2.zero;
	private LineRenderer lr;
	//private Vector2 targetDirVector;


	// Use this for initial
	private List<GameObject> touchingObjects;
	private int checkCount = 0;
	void Start () {
		planet = GameObject.FindGameObjectWithTag ("planet").transform;
		touchingObjects = new List<GameObject> ();
		lr = gameObject.GetComponent<LineRenderer> ();
		updatePosAndSpeed ();
		//Debug.Log(Sphere.findDistanceAndVector(new Vector2(90,90),new Vector2(160,90)).ToString());
	}





	// Update is called once per frame
	void Update () {
		
		alignToPlanet ();

		if (pesantType == PesantType.TreePicker && work ) {
			goToPos = Vector3.zero;
			updatePosAndSpeed ();
			Vector3 target;
			Vector2 target2;
			if (full > 0) {
				target = dropoffPos;
				target2 = dropoff;
			} else {
				target = pickupPos;
				target2 = pickup;
			}
			updatePosAndSpeed ();

			lr.positionCount = 2;
			lr.SetPositions (new [] { torsoTransform.position, target });

			if (full > 0 && buildingController!=null && (touchingObjects.Contains(buildingController.gameObject) ||  //if full, given BController and touching object then
				buildingController.transform.parent != null && touchingObjects.Contains(buildingController.transform.parent.gameObject)))							//drop staff
				StartCoroutine (dropAction ());
			else if (full == 0 && treeController!=null && (touchingObjects.Contains(treeController.gameObject) ||   // if hungry, given TreeCont, (touchingTree, or close) then eat
				!(distanceToCheckpoint > checkDistance))) 
				StartCoroutine (pickupAction ());
			else {
				//float angle = getAngleUpdateDist (goToPos);
				goTowards (target);
			} 

			
		} else if (goToPos != Vector3.zero && !enemy) {
			updatePosAndSpeed ();
			updateDistance (Sphere.getByPosition (goToPos));

			if (distanceToCheckpoint > checkDistance) {
				goTowards (goToPos);
				checkCount = 0;
			} else
				checkCount++;
			if (checkCount > 10)
				goToPos = Vector3.zero;


		} else if (!enemy){
			anim.SetBool ("Walking", false); 
			lr.positionCount = 0;
		}

		if (pesantType == PesantType.LaserRobot)
			updateLaser ();

		if (enemy) {
			updateDistance (targetPos2);
			switch (state) {
			case State.iddle:
				if (distanceToCheckpoint <= attackDistance) {
					anim.SetBool ("Walking", true);
					state = State.chasing;
				}
				break;
			case State.chasing:
				updatePosAndSpeed ();
				goTowards (targetPos);		//already checks for speed
				if (distanceToCheckpoint > chaseDistance) {
					anim.SetBool ("Walking", false);
					state = State.iddle;
				} else if (touchingObjects.Contains(targetTrans.gameObject)){
					anim.SetBool ("Attacking", true);
					state = State.attacking;
				}
				break;
			case State.attacking:
				anim.SetBool ("Attacking", true);
				break;
			}
		}
	}







	void updatePosAndSpeed(){
		ownPosition = Sphere.getByPosition ((gameObject.transform.position - planet.position).normalized);
		speed = torsoTransform.InverseTransformDirection (ownRigidBody.velocity).z;
	}
	void updateDistance(Vector2 target){
		distanceToCheckpoint = Sphere.findVectorAndDistance (ownPosition, target).z;
	}

	void alignToPlanet(){
		Vector3 rotation = Sphere.getRotationToObject (ownTransform.position, planet.position);
		rotation.x -= 90;
		gameObject.transform.eulerAngles = rotation;
		Vector3 localAngles = new Vector3(0,yOffset,0);
		torsoTransform.localEulerAngles = localAngles;
	}

	void goTowards(Vector3 target){

		anim.SetBool ("Walking", true);
		yOffset = Quaternion.LookRotation (ownTransform.InverseTransformDirection((target - ownTransform.position).normalized)).eulerAngles.y;

		if (speed < maxSpeed)
			moveFoward (moveForce, torsoTransform, ownRigidBody, moveRayLength);
	}

	void moveFoward(float force, Transform transform, Rigidbody rb, float rayLenght){


		Vector3 pushDirection = transform.forward;
		float ratio = 0;

		Vector3 rayDir = Vector3.Lerp (pushDirection, -transform.up, 0.60f);

		Ray ray = new Ray (transform.position, rayDir);
		RaycastHit rayHit;
		if (Physics.Raycast (ray, out rayHit, rayLenght)) {
			ratio = transform.InverseTransformVector(rayHit.normal).x * (1-(speed/maxSpeed));
			ratio = Mathf.Clamp (ratio, -0.4f, 0.4f);
			//outRatio = ratio;
			ratio = Mathf.Clamp01 (ratio);
		}

		rb.AddForce (pushDirection * (1 - ratio) * force * Time.deltaTime*60);
		rb.AddForce (transform.up * ratio * force * Time.deltaTime*60);

	}

	bool anythingInFront(float distance, int layer){
		if (Physics.Raycast (torsoTransform.position, torsoTransform.forward, distance, layer))
			return true;
		else
			return false;
	}
	bool anythingInLeft(float distance, int layer){
		if (Physics.Raycast (torsoTransform.position, Vector3.Lerp(torsoTransform.forward,-torsoTransform.right,0.5f), distance, layer))
			return true;
		else
			return false;
	}
	bool anythingInRight(float distance, int layer){
		if (Physics.Raycast (torsoTransform.position, Vector3.Lerp(torsoTransform.forward,torsoTransform.right,0.5f), distance, layer))
			return true;
		else
			return false;
	}

	/*float getAngleUpdateDist(Vector2 target){
		Vector3 vAndD = Sphere.findVectorAndDistance (Sphere.getByPosition (gameObject.transform.position), target);
		distanceToCheckpoint = vAndD.z;
		targetDirVector = new Vector2 (vAndD.x, vAndD.y);
		return Sphere.findAngleByVector2 (targetDirVector);
	}*/

	public State checkState(){
		return state;
	}








	//Pesant Functions**************************************
	private IEnumerator pickupAction(){
		if (checkDepleted ())
			full = 0;
		else {
			anim.SetTrigger ("Stick");
			work = false;
			yield return new WaitForSeconds(2.5f);
			if (treeController != null)
				full = treeController.suck (suckAmmount);
			yield return new WaitForSeconds(2f);
			work = true;
		}
	}
	private IEnumerator dropAction(){
		anim.SetTrigger ("Regurgutate");
		work = false;
		yield return new WaitForSeconds(2.3f);
		work = true;
		if (buildingController != null)
		if (buildingController.build (full) || full < suckAmmount) {
			work = false;
		}
		full = 0;

	}

	bool checkDepleted(){
		if (treeController.depleted ()) {
			work = false;
			return true;
		}
		return false;
	}


	public void goToCommand(Vector3 pos){
		ownPosition = Sphere.getByPosition ((gameObject.transform.position - planet.position).normalized);
		Vector3 vandDistance = Sphere.findVectorAndDistance (ownPosition, Sphere.getByPosition(pos));
		//howMuch = (int)vandDistance.z / 5;
		Vector3[] positions = Sphere.createLine(ownTransform.position.normalized,pos.normalized,20,ownTransform.position.magnitude, pos.magnitude);
		lr.positionCount = positions.Length;
		lr.SetPositions (positions);
		Debug.Log (Sphere.getByPosition (goToPos).ToString ());
		Debug.Log (vandDistance.ToString());
		goToPos = pos;
	}

	public void setPickup (Vector3 pickup3, TreeController tcLocal){
		pickup = Sphere.getByPosition ((pickup3-planet.position).normalized);
		pickupPos = pickup3;
		if (tcLocal != null)
			treeController = tcLocal;
	}
	public void setDropOff (Vector3 dropoff3, BuildingController bcLocal){
		dropoff = Sphere.getByPosition ((dropoff3-planet.position).normalized);
		dropoffPos = dropoff3;
		if (bcLocal != null)
			buildingController = bcLocal;
	}

	public void setTargetTrans(Transform targetTransLocal){
		targetTrans = targetTransLocal;
	}




	//General Fumctioms***************************
	void OnCollisionEnter(Collision collision){
		if (collision.transform.parent != null)
			touchingObjects.Add (collision.transform.parent.gameObject);
		else
			touchingObjects.Add (collision.gameObject);
	}
	void OnCollisionExit(Collision collision){
		if (collision.transform.parent != null)
			touchingObjects.Remove (collision.transform.parent.gameObject);
		else
			touchingObjects.Remove (collision.gameObject);
	}

	public void updateTarget(Vector3 targetPosLocal){
		targetPos2 = targetPosLocal;
	}

	//LaserRobot Functions ***********************************
	void updateLaser(){
		
	}
}
