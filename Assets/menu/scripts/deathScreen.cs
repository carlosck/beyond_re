﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathScreen : MonoBehaviour {

	public GameController gc;
	public GameObject select_exit; 
	public GameObject select_continue; 
		
	
	
	//public Animator animMenu; 

	public bool busy= true;
	
	
	int current_position=0;

	// Update is called once per frame
	void Awake()
	{		
		select_exit.SetActive(false);
	}
	void Update () {
		if (Input.GetKeyDown("left"))
		{
			//print("up was pressed go");
			current_position++;
			if(current_position>1)current_position=1;			
			update_select_position();
		}
		if (Input.GetKeyDown("right"))
		{
			//print("right key was pressed go");
			current_position--;
			if(current_position<0)current_position=0;
			update_select_position();			
		}

		if (Input.GetKeyDown("return"))
		{
			//print("return key was pressed go");
			
			selectMenu();			
		}	
		            
	}

	void update_select_position()
	{
		
		print(current_position);
		switch(current_position)
		{
			case 0: 
				select_exit.SetActive(true);
				select_continue.SetActive(false);		
			break;
			case 1: 
				select_exit.SetActive(false);
				select_continue.SetActive(true);
				
			break;
			
				
		}
	}
	public void selectMenu()
	{
		// Debug.Log("selectMenu from death_menu");
		// Debug.Log(current_position);
		switch(current_position)
		{
			case 0: 
				exitGame();
			break;
			case 1: 				
				continueGame();
			break;			
		}
	}
	void continueGame()
	{
		//print("continue");
		gc.deathContinue();
		//Quest currentQuest=questContainer.getQuestAt(0);		
		
	}
	void exitGame()
	{
		//print("exitGame");
		gc.deathExit();
		//animMenu.SetTrigger("exit");
		//gameQuestContainer.startGame();
		//ui.closeMenu();
		
	}
	

	
	
	public bool isBusy(){
		return busy;
	}
	
}

