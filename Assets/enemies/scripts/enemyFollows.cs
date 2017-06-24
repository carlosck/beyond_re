using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFollows : enemyWalk  {
	
		// Use this for initialization
	
	public override void FixedUpdate(){
		if(busy && !anim.GetBool("death")) {
			if(Mathf.Abs(transform.position.x-currentTarget.transform.position.x)<distanceToTarget){				
					//busy= false;
					//StopWalk();
					
				}
				else{				
					transform.Translate(new Vector2 (speed, 0)*Time.deltaTime);
					//busy= true;
					StartWalk();
					
				}		
		}
		
		

	}
	// Update is called once per frame
	
}
