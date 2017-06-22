using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyLimits : enemyWalk {

	public GameObject[] limits;
	public int currentLimitNumber =0;	
	
	void Awake()
	{
		currentTarget= limits[0];
	}
	// Update is called once per frame
	
	public override void getNextTarget(){
		Debug.Log("getNextTarget son");
		currentLimitNumber=currentLimitNumber+1;
		
		if(currentLimitNumber==limits.Length)
		{
			currentLimitNumber=0;
		}

		currentTarget= limits[currentLimitNumber];
		
		StartWalk();
		
	}
	
	
	
}
