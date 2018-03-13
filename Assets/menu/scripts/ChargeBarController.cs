using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBarController : MonoBehaviour {

	// Use this for initialization
	private float TimeScale = 1f;
	public GameObject ui_charge_1;
	public GameObject ui_charge_2;
	public GameObject ui_charge_3;


	void Start () {
		ui_charge_1.SetActive(false);
		ui_charge_2.SetActive(false);
		ui_charge_3.SetActive(false);	
	}
		
	public void updateChargeLvl(int charge_level){
		switch(charge_level){
			case 0: 
			ui_charge_1.SetActive(false);
			ui_charge_2.SetActive(false);
			ui_charge_3.SetActive(false);
			break;
			case 1: 
			ui_charge_1.SetActive(true);
			ui_charge_2.SetActive(false);
			ui_charge_3.SetActive(false);
			break;
			case 2: 
			ui_charge_1.SetActive(true);
			ui_charge_2.SetActive(true);
			ui_charge_3.SetActive(false);
			break;
			case 3: 
			ui_charge_1.SetActive(true);
			ui_charge_2.SetActive(true);
			ui_charge_3.SetActive(true);
			break;
		}
	}

	 public void restart(){
		ui_charge_1.SetActive(false);
		ui_charge_2.SetActive(false);
		ui_charge_3.SetActive(false);
	 }
	 
}
