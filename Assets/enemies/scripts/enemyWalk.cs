using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyWalk : MonoBehaviour {
	
	
	public float speed = -3f;
	public GameObject currentTarget;	
	public Animator anim;
	public int distanceToTarget= 1;
	//Rigidbody2D rb;
	public bool busy=false;
	// Use this for initialization
	
	public virtual void Start () {
		anim=transform.Find("sprites").GetComponent<Animator>();
	}
	

	
	// Update is called once per frame
	public virtual void FixedUpdate(){
		//if(!anim.GetBool("death")) return true;
		if(busy & !anim.GetBool("death"))
		{
			if(Mathf.Abs(transform.position.x-currentTarget.transform.position.x)<distanceToTarget){				
				busy= false;
				getNextTarget();
				
			}
			else{				
					transform.Translate(new Vector2 (speed, 0)*Time.deltaTime);
					
				}
		}	
		

	}
	public virtual void getNextTarget(){		
		StartWalk();
	}
	public virtual void StartWalk()
	{
		if(transform.position.x>currentTarget.transform.position.x)
		{
			speed= Mathf.Abs(speed)*-1;
			flip(1);
		}
		else{
			speed= Mathf.Abs(speed);
			flip(-1);
		}
		
		anim.SetBool("walk",true);
		busy=true;
	}
	
	public virtual void StopWalk(){
		
		busy=false;
		anim.SetBool("walk",false);
	}
	void setPlayerInRange(bool inRange){
		
		if(inRange){
			StartWalk();
		}
		else{
			StopWalk();
		}
		
	}
	public virtual void flip(int scale){
		Vector3 theScale = transform.localScale;
		theScale.x = Mathf.Abs(theScale.x) *scale;		
		anim.transform.localScale = theScale;
	}
	
}
