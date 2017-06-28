﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	// Use this for initialization	
	public GameObject ui_start;
	public GameObject ui_death;

	public GameObject introCamera;
	public GameObject currentCamera;
	public GameObject player;
	public int currentScene=0;
	public string[] scenes;
	Platformer2DUserControl plataformController ;

	
	
	void Start () {
				
		ui_start.SetActive(true);
		ui_death.SetActive(false);
		//Todo load from disk
		currentScene=0;
		
		
	}
	
	public void startContinue(){
		
		StartCoroutine(LoadGameScene(currentScene));
		

	}
	public void startNewGame(){
		

		StartCoroutine(LoadGameScene(0));
		

	}

	IEnumerator LoadGameScene(int SceneNumber)
	 {
		 Debug.Log ( "START Load Async" );
		 var result = SceneManager.LoadSceneAsync ( scenes[SceneNumber], LoadSceneMode.Additive );
		 result.allowSceneActivation = true;

		 while (!result.isDone) 
		 {
			 Debug.Log ( "progress: " + result.progress );
			 yield return new WaitForEndOfFrame ();
		 }
		 Debug.Log ( "YEAH Loaded Async" );
		 // still scene one should be active, tryed it as workaround, did not help
		 ui_start.SetActive(false);
		 introCamera.SetActive(false) ;
		 SceneManager.SetActiveScene (SceneManager.GetSceneByName (scenes[SceneNumber]));		 
		 currentScene= SceneNumber;
	}

	public void gotDead()
	{
		plataformController.setControl(false);
		StartCoroutine(letEmDie(2.0f));
		
	}
	public void deadByFall()
	{
		plataformController.setControl(false);
		StartCoroutine(letEmDie(0f));
	}
	public void deathContinue(){
		introCamera.SetActive(true);
		ui_death.SetActive(false);		
		Time.timeScale=1;
		restartLevel();
	}
	public void deathExit(){
		SceneManager.UnloadScene(currentScene);
		ui_start.SetActive(true);
		ui_death.SetActive(false);
	}
	private void restartLevel (){
		plataformController.setControl(true);
		SceneManager.UnloadScene(currentScene);
		StartCoroutine(LoadGameScene(currentScene));
	}
	
    IEnumerator  letEmDie(float animTime)
	{		
		yield return new WaitForSeconds(animTime);
		ui_death.SetActive(true);
		Time.timeScale=0;
    }

    public void initScene(GameObject _player,GameObject _camera)
    {
    	player= _player;
    	plataformController= player.GetComponent<Platformer2DUserControl>();
    	currentCamera= _camera;
    	//Debug.Log(" --------- initScene ---------- ");
    	Time.timeScale=1;
    }
    public void goal(){
    	if(currentScene<scenes.Length-1){
    		SceneManager.UnloadScene(currentScene);
    		currentScene++;
    		LoadGameScene(currentScene);
    	}
    	
    }
	
	
}
