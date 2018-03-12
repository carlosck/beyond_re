using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsceneController : MonoBehaviour {

	// Use this for initialization
	public GameObject player;
	public GameObject camera;
	GameController gc;
	void Awake () {
		if(GameObject.FindGameObjectWithTag("GameController")!=null)
		{
			gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
			gc.initScene(player,camera);
		}
	}

	void updateChargeLvl(int charge_level){
		gc.updateChargeLvl(charge_level);
	}	
	
}
