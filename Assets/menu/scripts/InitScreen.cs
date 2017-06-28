using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitScreen : MonoBehaviour {

	public GameController gc;
	public GameObject select_newGame; 
	public GameObject select_continue; 
		
	
	
	//public Animator animMenu; 

	public bool busy= true;
	
	
	int current_position=0;

	// Update is called once per frame
	void Awake()
	{		
		select_continue.SetActive(false);
	}
	void Update () {
		if (Input.GetKeyDown("up"))
		{
			//print("up was pressed go");
			current_position++;
			if(current_position>1)current_position=1;			
			update_select_position();
		}
		if (Input.GetKeyDown("down"))
		{
			//print("down key was pressed go");
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
		
		//print(current_position);
		switch(current_position)
		{
			case 0: 
					select_newGame.SetActive(false);
					select_continue.SetActive(true);	
			break;
			case 1: 
				
				select_newGame.SetActive(true);
				select_continue.SetActive(false);
			break;
			
				
		}
	}
	public void selectMenu()
	{
		// Debug.Log("selectMenu from gameOver_menu");
		// Debug.Log(current_position);
		switch(current_position)
		{
			case 0: 
				newGame();
			break;
			case 1: 				
				continueGame();
			break;			
		}
	}
	public void continueGame()
	{
		
		gc.startContinue();				
	}
	public void newGame()
	{		
		gc.startNewGame();		
	}
	public void mouseOverNewGame(){
		
		current_position=1;
		update_select_position();
	}

	public void mouseOverContinue(){
		
		current_position=0;
		update_select_position();
	}
	

	
	
	public bool isBusy(){
		return busy;
	}
	
}


