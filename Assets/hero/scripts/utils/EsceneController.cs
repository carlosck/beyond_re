using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsceneController : MonoBehaviour {

	// Use this for initialization
	public GameObject player;
	public GameObject camera;
	void Awake () {
		GameController gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		gc.initScene(player,camera);
	}
	
	
}
