using UnityEngine;
using System.Collections;

public class PSplashScreenState : BaseState
{
	PGUISplashScreen gui;
	
	public override void OnEntered ()
	{
		gui = GameObject.FindObjectOfType<PGUISplashScreen>();
		base.OnEntered ();
		gui.splashScreen.SetActive (true);
		gui.gameUI.SetActive(false);
		Global.instance.soundManager.playMusic(gui.music, false);
		StartCoroutine(wait ());
	}
	
	public override void OnExit ()
	{
		gui.splashScreen.SetActive (false);
		gui.gameUI.SetActive(true);
		base.OnExit ();
	}

	IEnumerator wait(){
		yield return new WaitForSeconds(gui.waitTime);
		States.Pop();
		States.State<StateGame> ().startBriefing ();
	}
}
