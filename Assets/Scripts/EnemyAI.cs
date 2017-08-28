using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	public enum EnemyType{Snake};
	public EnemyType enemyType = EnemyType.Snake;
	public float attackDistance = 5;
	public float chaseDistance = 9;
	public float moveForce = 100;
	public float moveRayLength = 0.1f;


	private Vector2 enemyDirVector;
	private float yOffset;
	private Vector2 targetPos;
	private float distanceToTarget;
	private Transform ownTransform;
	private Rigidbody ownRigidbody;
	private Transform torsoTrasform;
	private float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
