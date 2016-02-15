using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	// Use this for initialization
	public static float 		timer; 
	public 			float 		timerInSeconds;
	public 			float 		timerInSeconds2;
	public  		bool 		timeStarted = false;
	public 			float 		waitingInterval; 
	public   		Text 		timerString;
	public 			GameObject	text;
	private 		bool run;
	//public AudioSource source;
	
	
	void OnEnable() {
		timerInSeconds2 =((DataFile.min*60f)+DataFile.sec);
		timerString.text =GetTimerValue();
		run=true;
		
		
	}
	
	// Update is called once per frame
	void Update () {
		if (timeStarted == true) {
			
			timerInSeconds2 = timerInSeconds2-Time.deltaTime;
			if (timerInSeconds2 <= 0) {
				
				FinishFunction();
				

			}
			else{
			timerString.text =GetTimerValue();
				
				if(timerInSeconds2 <=11f)
				{
					if(run)
					{
						run=false;
						
						text.GetComponent<TextEffect>().Change();
					}
				}
				
			}
			
		}
	}

	
	/*******************************************************
					
	********************************************************/
	
	public string GetTimerValue()
	{
		if (timerInSeconds2 > 0) {
			int minutes = Mathf.FloorToInt (timerInSeconds2 / 60F);
			int seconds = Mathf.FloorToInt (timerInSeconds2 - minutes * 60);
			
			string niceTime = string.Format (" {00:0} : {1:00}", minutes, seconds);
			return niceTime;
		} else
			return " 0:00";
	}
	
	/*******************************************************
					
	********************************************************/
	
	public void FinishFunction()
	{
		Controller.GetInstance().hit=false;
		Controller.GetInstance().levelcompleted();
		
		
	}

	
}
