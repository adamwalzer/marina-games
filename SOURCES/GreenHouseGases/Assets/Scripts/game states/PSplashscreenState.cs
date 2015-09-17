using UnityEngine;
using System.Collections;

public class PSplashscreenState : BaseState
{
	PGUISplashscreen gui;

	public override void OnEntered ()
	{
		gui = GameObject.FindObjectOfType<PGUISplashscreen>();
		base.OnEntered ();
		gui.splashscreen.show ();
//		StartCoroutine(waitAndStart());
	}

	public override void OnExit ()
	{
		gui.splashscreen.hide ();
		base.OnExit ();
	}

	public void onStartClick(){
		States.Pop();
		States.State<StateGame> ().startBriefing ();
	}

//	IEnumerator waitAndStart(){
//		yield return new WaitForSeconds(4f);
//		States.Pop();
//		States.State<StateGame> ().startBriefing ();
//	}
}
