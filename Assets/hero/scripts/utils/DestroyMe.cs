using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour {

	public float ttl;
	// Use this for initialization
	void Awake () {
		Destroy(gameObject, ttl);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
