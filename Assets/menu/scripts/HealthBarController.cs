using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour {

	// Use this for initialization
	private float TimeScale = 1f;
	
	// void Start () {
		
	// }
	
	// // Update is called once per frame
	// void Update () {
		
	// }

	public void updateHealthBar(float percent_current,float percent_new)
	{
		StartCoroutine(scaleBar(percent_current,percent_new));

	}

	IEnumerator scaleBar(float current_health, float new_health)
	{
		float progress = 0;
		
		Vector3 scale_1 = new Vector3(current_health, 1f, 1f);
		Vector3 scale_end = new Vector3(new_health, 1f, 1f);
		while(progress <= 1){
			transform.localScale = Vector3.Lerp(scale_1, scale_end, progress);
			progress += Time.deltaTime * TimeScale;
			yield return null;
		}
		transform.localScale = scale_end;

	 }
	 
}
