using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyWalk : MonoBehaviour {

	public GameObject[] limits;
	public float speed = 3f;
	public GameObject currentLimit;
	public GameObject playerDetector;
	GameObject player;
	bool playerInRange;
	int distanceToLimit= 20;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		currentLimit= limits[0];
	}

	void OnTriggerEnter(Collider other)
	{
		
		
		if(other.gameObject == player)
		{			
			startWalking();
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject == player)
		{			
			stopWalking();
		}
	}
	// Update is called once per frame
	void FixedUpdate(){
		// if(playerInRange){
		// 	if(Mathf.Abs(transform.position.x<)){
				
		// 	}
		// }
		// if(distance >= 0) {
		// 	rigidbody2D.velocity = new Vector2 (speed, rigidbody2D.velocity.y);
		// 	distance = endingPositionX - transform.position.x;
		// 	print (distance);
		// }
		// else if(distance <= 0) {
		// 	startingPositionX = originalPositionX + endingPositionX;
		// 	endingPositionX =originalPositionX;
		// 	Flip ();
		// 	print ("elerte");
		// 	rigidbody2D.velocity = new Vector2 (-speed, rigidbody2D.velocity.y);
		// 	distance = Mathf.Abs(endingPositionX - transform.position.x);
		// }
	}
	void startWalking(){
		playerInRange=true;
	}
	void stopWalking(){
		playerInRange=false;
	}
}
