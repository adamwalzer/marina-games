using UnityEngine;
using System.Collections;

public class PStateEndLevel : BaseState 
{
	public int scores;
	public string time;
	int lvl;

	PGUIEndLevel gui;

	public override void OnEnter ()
	{
		gui = GameObject.FindObjectOfType<PGUIEndLevel>();
		base.OnEnter ();
		lvl = States.State<StateGame> ().currentLevel;
		gui.show (lvl, scores, time);
		gui.gameUI.SetActive(false);
		gui.hero.SetActive(false);
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

	public override void OnPaused ()
	{
		base.OnPaused ();
		gui.hide();
	}
	
	public override void OnResume ()
	{
		base.OnResume ();
		gui.show (lvl, scores, time);
	}

	public override void OnExit ()
	{
		gui.hide ();
		gui.gameUI.SetActive(true);
		base.OnExit ();
	}
}
