using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyWalk : MonoBehaviour {

	public GameObject[] limits;
	public int currentLimitNumber =0;
	public float speed = -3f;
	public GameObject currentLimit;
	public GameObject playerDetector;
	Animator anim;
	int distanceToLimit= 20;
	Rigidbody2D rb;
	bool busy=false;
	// Use this for initialization
	void Awake () {
		//player = GameObject.FindGameObjectWithTag("Player");
		currentLimit= limits[0];
		rb= GetComponent<Rigidbody2D>();
		anim=transform.Find("sprites").GetComponent<Animator>();
	}

	
	// Update is called once per frame
	void Update(){
		
		if(busy)
		{
			if(Mathf.Abs(transform.position.x-currentLimit.transform.position.x)<1){
				Debug.Log("Choco");
				busy= false;
				getNextLimit();
				
			}
			else
				//rigidbody2D.AddRelativeForce (new Vector2 (speed*-1, 0));

				transform.Translate(new Vector2 (speed, 0)*Time.deltaTime);
		}
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
		//transform.Translate(new Vector2 (speed, 0)*Time.deltaTime);

	}
	void getNextLimit(){
		currentLimitNumber=currentLimitNumber+1;
		Debug.Log("currentLimitNumber");
		Debug.Log(currentLimitNumber);
		if(currentLimitNumber==limits.Length)
		{
			currentLimitNumber=0;
		}

		currentLimit= limits[currentLimitNumber];
		
		Startwalk();
	}
	void Startwalk()
	{
		
		if(transform.position.x>currentLimit.transform.position.x)
		{
			speed= Mathf.Abs(speed)*-1;
			flip(1);
		}
		else{
			speed= Mathf.Abs(speed);
			flip(-1);
		}
		// rigidbody2D.velocity= new Vector2 (1, rigidbody2D.velocity.y);
		// rigidbody2D.AddForce (new Vector2 (speed, 0));
		anim.SetBool("walk",true);
		busy=true;
	}
	void StopWalk(){
		//rb.velocity= (new Vector2 (0, 0));
		busy=false;
		anim.SetBool("attack",false);
	}
	void setPlayerInRange(bool inRange){
		Debug.Log("setPlayerInRange"+inRange);
		if(inRange){
			Startwalk();
		}
		else{
			StopWalk();
		}
		
	}
	void flip(int scale){
		Vector3 theScale = transform.localScale;
		theScale.x = Mathf.Abs(theScale.x) *scale;
		transform.localScale = theScale;
	}
	
}
