using UnityEngine;
using System.Collections;

public class PStateEndLevel : BaseState 
{
	PGUIEndLevel gui;

	public override void OnEnter ()
	{
//		GameObject go = GameObject.FindGameObjectWithTag ("MainObject");
		gui = GameObject.FindObjectOfType<PGUIEndLevel> ();
		base.OnEnter ();
//		int lvl = States.State<StateGame> ().currentLevel;
		gui.show ();
	}
	
	public void onNextClick(){
		States.Pop ();
		States.State<StateGame> ().goToNextLevel ();
	}
	
	public void onReplayClick(){
		States.Pop ();
		States.State<StateGame> ().replayLevel ();
	}

	public void onStageCompletedClick(){
		States.Pop ();
		States.State<StateGame> ().stageCompleted ();
	}
	
	public override void OnExit ()
	{
		gui.hide ();
		base.OnExit ();
	}
}
