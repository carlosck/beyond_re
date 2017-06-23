using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCrawler : enemyFlyer {

	// Use this for initialization
	
	// Update is called once per frame
	float x_previous=0f;
	
	public override void StartWalk(){
			
		
		desiredVelocity = (currentTarget.transform.position - transform.position).normalized * speed;
		Debug.Log(desiredVelocity.y);
		// if(Mathf.Abs(desiredVelocity.y)>Mathf.Abs(desiredVelocity.x))
		// {
		// 	desiredVelocity.x=0;
		// }
		// else{
		// 	desiredVelocity.y=0;
		// }
		anim.SetBool("walk",true);

		busy=true;
		flip(0);
	}
	public virtual void flip(int scale){
		Vector3 theScale = anim.transform.localScale;
		if(transform.position.x>currentTarget.transform.position.x)
		{			
			theScale.x = Mathf.Abs(theScale.x) *1;
		}
		else{			
			theScale.x = Mathf.Abs(theScale.x) *-1;
		}
		Vector3 temp = anim.transform.rotation.eulerAngles;
		 
		 
		if(Mathf.Abs(desiredVelocity.y)<1)
		{
			temp.z = 0.0f;
		}
		else{
			if(desiredVelocity.y>1)
			{
				if(transform.position.x>x_previous)
				{
					temp.z = 90.0f;	
				}
				else{
					temp.z = -90.0f;		
				}
				
			}
			else{
				if(transform.position.x>x_previous)
				{
					temp.z = -90.0f;	
				}
				else{
					temp.z = 90.0f;		
				}

			}

		}		
		anim.transform.rotation = Quaternion.Euler(temp);
		//anim.transform.localScale = theScale;
		//anim.flipX=true;
		x_previous= transform.position.x;			
	}
}
