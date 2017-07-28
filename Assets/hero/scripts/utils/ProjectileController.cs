using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

	Rigidbody2D rb;	

	// Use this for initialization
	void Awake () {
		rb= GetComponent<Rigidbody2D>();		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll){
		Debug.Log(coll.gameObject.tag);
		if (coll.gameObject.tag == "wall")
		{
            Destroy(gameObject);           
		}
		else
		{
			if (coll.gameObject.tag == "Enemy")
			{
	            Destroy(gameObject);
	            coll.gameObject.SendMessage("TakeDamage", 10);
			}
		}
	}
}
