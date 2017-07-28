using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDetector : MonoBehaviour {
	Transform parent;
	
	GameObject player;
	public GameObject gameController;
	GameController gc;

	// Use this for initialization
	void Start () {
		
		player = GameObject.FindGameObjectWithTag("Player");
	}
	void Awake()
	{
		if(GameObject.FindGameObjectWithTag("GameController")!=null)
		{
			gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();	
		}
	}
		
	void OnTriggerEnter2D(Collider2D other)
	{
		
		
		if(other.gameObject == player)
		{						
			Debug.Log("goal");
			gc.goal();
		}
	}

	
}
