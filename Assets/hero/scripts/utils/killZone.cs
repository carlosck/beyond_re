using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killZone : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {            
            GameController gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            gc.deadByFall();
            
        }
    }
}
