using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	// Use this for initialization	
	public GameObject ui_start;
	public GameObject ui_death;
	public GameObject ui_hurt;

	public GameObject introCamera;
	public GameObject currentCamera;
	public GameObject player;
	public int currentScene=0;
	public string[] scenes;
	Platformer2DUserControl plataformController ;
	bool busy = false;
	
	
	void Start () {
				
		ui_start.SetActive(true);
		ui_death.SetActive(false);
		ui_hurt.SetActive(false);
		//Todo load from disk
		currentScene=0;
		
		
	}
	
	public void startContinue(){
		if(busy) return ;
		busy= true;
		StartCoroutine(LoadGameScene(currentScene));
		

	}
	public void startNewGame(){
		
		if(busy) return ;
		busy= true;
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
		 Time.timeScale=1;
		 busy=false;
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
		Debug.Log("deathContinue");
		introCamera.SetActive(true);
		ui_death.SetActive(false);		
		Time.timeScale=1;
		restartLevel();
	}
	public void deathExit(){
		SceneManager.UnloadScene(scenes[currentScene]);
		ui_start.SetActive(true);
		ui_death.SetActive(false);
	}
	private void restartLevel (){
		Debug.Log("rstart");
		busy=true;
		plataformController.setControl(true);
		SceneManager.UnloadScene(scenes[currentScene]);
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
    	Debug.Log(" --------- initScene ---------- ");
    	Time.timeScale=1;
    }
    public void showDamage()
    {
    	ui_hurt.SetActive(true);
    	StartCoroutine(hideDamage(2.0f));
    }
    
    IEnumerator  hideDamage(float animTime)
	{		
		yield return new WaitForSeconds(animTime);
		ui_hurt.SetActive(false);
    }
    

    public void goal(){
    	if(currentScene<scenes.Length-1){
    		Time.timeScale=0;
    		SceneManager.UnloadSceneAsync(scenes[currentScene]);
    		currentScene++;
    		StartCoroutine(LoadGameScene(currentScene));
    	}
    	
    }
	
	
}
