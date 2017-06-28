using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	public int startingHealth = 100;
	public int currentHealth;
	//public Slider healthSlider;
	//public Text HealthTextPercent;
	public int defense = 0;
	//public GameObject fadeObj ;
	//public UIActions ui;
	public Animator anim;
	bool isDead;
	bool damaged;	
	//public Transform show_damage;
	GameObject player;
	Transform player_transform;
	public GameObject gameController;
	GameController gc;
	//CharacterMotor characterMotor;
	//SpriteRenderer renderer;


	void Awake()
	{
		
		currentHealth = startingHealth;	
		player = GameObject.FindGameObjectWithTag("Player");
		player_transform= player.GetComponent<Transform>();
		if(GameObject.FindGameObjectWithTag("GameController")!=null)
		{
			gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();	
		}
		
		
		//characterMotor = player.GetComponent<CharacterMotor>();
		//renderer = transform.Find("animations").GetComponent <SpriteRenderer>();
		//childObject.transform.parent.gameObject	
	}
	

	public void TakeDamage(int amount)
	{
		if(isDead) {return ;}
		
		int total_damage = amount-defense;
		if(total_damage<=0)
			total_damage = 0;
		else
		{
			damaged = true;
			currentHealth -= total_damage;
		}
				
		//ShowDamage(total_damage);	
		
		if(currentHealth>0)
		{
			
			
			// updateHealthBar();
			// StartCoroutine(showRed());
		}
		else
		{
			// HealthTextPercent.text = "0";
			// healthSlider.value = 0;
			if(currentHealth <= 0 && !isDead)
			{
				Die();
			}
		}
	}

	void Die()
	{
		
		isDead = true;
		anim.SetBool("Death",true);
		gc.gotDead();
		

	}
	
	void ShowDamage(int amount)
	{
		//var pos= (Vector3) player.GetComponent<Transform>().position;
		
		
		//var show_dmg = (Transform) Instantiate(show_damage, pos, Quaternion.identity);
		
		//Text dmg_txt = (Text) show_dmg.transform.Find("GameObject/Canvas/Text").GetComponent <Text>();
		///dmg_txt.text= amount+" "; 
		//Destroy(show_dmg.gameObject,1.1f);

	}
	// IEnumerator showRed()
	// {		
	// 	renderer.color = new Color(1f, 0f, 0f, 1f);
	// 	yield return new WaitForSeconds(0.5f);
	// 	renderer.color = new Color(1f, 1f, 1f, 1f);		
 //    }
    public void restart()
    {
    	currentHealth = startingHealth;
    	isDead = false;
    	//updateHealthBar();
    	//characterMotor.Revive();
    }
    // public void updateHealthBar(){
    // 	HealthTextPercent.text = currentHealth.ToString() ;
    // 	healthSlider.value = currentHealth;
    // }
    public int getCurrentHealth()
    {
    	return currentHealth;
    }
}