using UnityEngine;
using System.Collections;

public class PStateEndLevel : BaseState 
{
	public int scores;
	public string time;

	PGUIEndLevel gui;

	public override void OnEnter ()
	{
		gui = GameObject.FindObjectOfType<PGUIEndLevel>();
		base.OnEnter ();
		int lvl = States.State<StateGame> ().currentLevel;
		gui.show (lvl, scores, time);
		gui.gameUI.SetActive(false);
		Global.instance.soundManager.playMusic(gui.music, false);

	}
	
	public void onNextClick(){
		States.Pop ();
		States.State<StateGame> ().goToNextLevel ();
	}
	
	public void onReplayClick(){
		States.Pop ();
		States.State<StateGame> ().replayLevel ();
	}

	public override void OnExit ()
	{
		gui.hide ();
		gui.gameUI.SetActive(true);
		base.OnExit ();
	}
}
