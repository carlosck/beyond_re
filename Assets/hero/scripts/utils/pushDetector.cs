using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushDetector : MonoBehaviour {
	
	
	GameObject player;

	// Use this for initialization
	void Start () {
		
		player = GameObject.FindGameObjectWithTag("Player");
	}
		
	void OnTriggerEnter2D(Collider2D other)
	{
		
		
		if(other.gameObject == player)
		{						
			player.SendMessage("pushArea", true);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject == player)
		{						
			player.SendMessage("pushArea", false);
		}
	}
}
