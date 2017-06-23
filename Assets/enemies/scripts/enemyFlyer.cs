using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFlyer : enemyLimits {

	// Use this for initialization
	Rigidbody2D rb;
	Vector2 lookDir;	
	public Vector2 desiredVelocity;
	public bool forward = true;

	public override void Start(){
		anim=transform.Find("sprites").GetComponent<Animator>();
		rb = transform.GetComponent<Rigidbody2D>();
	}
	void Awake()
	{
		currentTarget= limits[0];		
		lookDir = (currentTarget.transform.position - transform.position).normalized * speed;	
		desiredVelocity = lookDir;
	}
	// public void Update(){
		
	// 	if ( busy && Vector2.Distance(currentTarget.transform.position, transform.position)<0.1)
	// 	    {		         
	// 	         desiredVelocity = Vector3.zero;
	// 	         busy= false;		         
	// 	         getNextTarget();
	// 	    } 		
	// }
	public override void FixedUpdate(){
		
		if ( busy && Vector2.Distance(currentTarget.transform.position, transform.position)<0.1)
		    {		         
		         desiredVelocity = Vector3.zero;
		         busy= false;		         
		         getNextTarget();
		    } 
		if(busy & !anim.GetBool("death"))
		{
			rb.velocity = desiredVelocity;								
		}	
	
		
		

	}
	public override void getNextTarget(){		
		if(forward){
			currentLimitNumber=currentLimitNumber+1;
			if(currentLimitNumber==limits.Length)
			{
				forward= false;
				currentLimitNumber=limits.Length-1;
			}
		}
		else
		{
			currentLimitNumber=currentLimitNumber-1;
			if(currentLimitNumber<0)
			{
				forward= true;
				currentLimitNumber=1;
			}
		}

		currentTarget= limits[currentLimitNumber];
		
				
		StartWalk();
		
	}
	public override void StartWalk(){
		if(transform.position.x>currentTarget.transform.position.x)
		{			
			flip(1);
		}
		else{			
			flip(-1);
		}		
		desiredVelocity = (currentTarget.transform.position - transform.position).normalized * speed;
		anim.SetBool("walk",true);

		busy=true;
	}
	public override void StopWalk()
	{
		busy=false;
		anim.SetBool("walk",false);		
		desiredVelocity = Vector3.zero;
		rb.velocity= desiredVelocity;
	}

	
}
