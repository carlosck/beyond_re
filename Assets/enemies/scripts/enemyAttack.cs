using UnityEngine;
using System.Collections;

public class enemyAttack : MonoBehaviour {

	public float timeBetweenAttacks = 0.5f;
	public int attackDamage = 10;
	// Use this for initialization

	Animator anim;
	HealthSystem health;
	GameObject player;
	PlayerHealth playerHealth;
	bool playerInRange;
	float timer;
	
	
	void Awake () {		
		

		player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        anim=transform.Find("sprites").GetComponent<Animator>();
		if (playerHealth == null) {
		       
		        Debug.Log("es null");
		    }		

		
		//anim = GetComponent<Animator>();
		//anim = transform.Find("animContainer/animations").GetComponent <Animator>();
		health = GetComponent <HealthSystem>();
	}
	
	void OnCollisionEnter2D(Collision2D other)
	{
		
		if(other.gameObject.tag == "Player")
		{			
			playerInRange = true;
			anim.SetBool("attack",true);
		}
	}
	
	void OnCollisionExit2D(Collision2D other)
	{
		
		if(other.gameObject.tag == "Player")
		{
			playerInRange = false;
			anim.SetBool("attack",false);
		}
	}
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		
		if(timer >= timeBetweenAttacks && playerInRange && playerHealth.currentHealth > 0 && health.currentHealth>0)
		{			
			Attack();
		}

		// if(playerHealth.currentHealth <=0)
		// {
		// 	//anim.setTrigger("playerDead");
		// }
	}

	void Attack()
	{
		timer = 0f;
		
		playerHealth.TakeDamage(attackDamage);
		
	}
}
