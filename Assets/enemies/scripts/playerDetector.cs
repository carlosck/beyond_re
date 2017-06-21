using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDetector : MonoBehaviour {
	Transform parent;
	public Transform enemy;
	GameObject player;

	// Use this for initialization
	void Start () {
		parent=transform.parent;
		var children = parent.GetComponentsInChildren<Transform>();
		for (int i = 0; i < parent.childCount; i++){
			Transform child = parent.GetChild(i);
			Debug.Log(child.tag);
			if(child.tag=="Enemy")
			{
				enemy=child;
			}
		}
		//enemy= parent.FindGameObjectWithTag("Enemy"); 
		player = GameObject.FindGameObjectWithTag("Player");
	}
		
	void OnTriggerEnter2D(Collider2D other)
	{
		
		
		if(other.gameObject == player)
		{			
			Debug.Log("setPlayerInRange s");
			enemy.SendMessage("setPlayerInRange", true);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject == player)
		{			
			Debug.Log("setPlayerInRange n");
			enemy.SendMessage("setPlayerInRange", false);
		}
	}
}
