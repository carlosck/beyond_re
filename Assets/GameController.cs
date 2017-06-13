using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class GameController : MonoBehaviour {

	// Use this for initialization
	public GameObject ui;
	public GameObject player;
	Platformer2DUserControl plataformController ;
	void Start () {
		plataformController= player.GetComponent<Platformer2DUserControl>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void gotDead()
	{
		plataformController.setControl(false);
	}
}
