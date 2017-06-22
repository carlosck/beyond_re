using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCrawler : enemyLimits {

	// Use this for initialization
	
	// Update is called once per frame
	bool forward = true;
	Rigidbody2D rb;

	public override void Start(){
		anim=transform.Find("sprites").GetComponent<Animator>();
		rb = transform.GetComponent<Rigidbody2D>();
	}
	public override void FixedUpdate(){
		
		if(busy & !anim.GetBool("death"))
		{
			if(Vector2.Distance(currentTarget.transform.position, transform.position)<distanceToTarget){				
				busy= false;
				getNextTarget();
				
			}
			else{				
					//transform.Translate(new Vector2 (speed, 0)*Time.deltaTime);
					var dist = Vector2.Distance(currentTarget.transform.position, transform.position);
					
					var lookDir = currentTarget.transform.position - transform.position;
					if(Mathf.Abs(lookDir.y)>Mathf.Abs(lookDir.x))
					{
						lookDir.x=0;
					}
					else{
						lookDir.y=0;
					}
					if(currentTarget.transform.position.x<transform.position.x)
					{
						//speed= Mathf.Abs(speed)*-1 ;
						print("speed -1");
						rb.velocity = new Vector2((lookDir.normalized.x*speed*Time.deltaTime)*-1,lookDir.normalized.y*speed*Time.deltaTime);
					}
					else{
						rb.velocity = new Vector2(lookDir.normalized.x*speed*Time.deltaTime,lookDir.normalized.y*speed*Time.deltaTime);
					}
					
					//lookDir.y = 0;
					//rb.velocity = new Vector2(rb.velocity.x * speed, rb.velocity.y);
					
					print(lookDir.normalized.x);
					//rb.velocity= new Vector2(lookDir.x *(speed  * Time.deltaTime),lookDir.y *(speed  * Time.deltaTime));
				}
		}	
		// if(!anim.GetBool("death")) {
		// 	if(Mathf.Abs(transform.position.x-currentTarget.transform.position.x)<distanceToTarget){				
		// 			busy= false;
		// 			StopWalk();
					
		// 		}
		// 		else{				
		// 			//rb.velocity = new Vector2(rb.velocity.x * speed, rb.velocity.y* speed);
		// 			transform.Translate(new Vector2 (speed, 0)*Time.deltaTime);
		// 			StartWalk();
					
		// 		}		
		// }

		// if(busy & !anim.GetBool("death"))
		// {
		// 	if(Mathf.Abs(transform.position.x-currentTarget.transform.position.x)<distanceToTarget){				
		// 		busy= false;
		// 		getNextTarget();
		// 		Debug.Log("llegó");
				
		// 	}
		// 	else{				
		// 			var dist = Vector2.Distance(currentTarget.transform.position, transform.position);
		// 			var lookDir = currentTarget.transform.position - transform.position;
		// 			lookDir.y = 0;

		// 			//transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(lookDir), speed*Time.deltaTime);

		// 			rb.velocity = new Vector2(rb.velocity.x * speed, rb.velocity.y);
					
		// 		}
		// }	
		
		

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
}
