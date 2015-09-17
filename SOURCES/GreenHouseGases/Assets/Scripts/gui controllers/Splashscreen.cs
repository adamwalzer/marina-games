using UnityEngine;
using System.Collections;

public class Splashscreen : MonoBehaviour 
{
	public Animator startBttnAnima;

	public void show(){
		gameObject.SetActive(true);
		startBttnAnima.SetTrigger("Activate");
	}

	public void hide(){
		gameObject.SetActive(false);
	}

	public void onStartClick(){
		States.State<PSplashscreenState>().onStartClick();
}
}
