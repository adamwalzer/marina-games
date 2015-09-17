using UnityEngine;
using System.Collections;

public class PStateEndLevel : BaseState 
{
	PGUIEndLevel gui;
	int lvl;

	public override void OnEnter ()
	{
		GameObject go = GameObject.FindGameObjectWithTag ("MainObject");
		gui = go.GetComponentInChildren<PGUIEndLevel> ();
		base.OnEnter ();
		lvl = States.State<StateGame> ().currentLevel;
		Global.instance.soundManager.playMusic(gui.winMusic, false);
		gui.show (lvl);
	}
	
	public void onNextClick(){
		States.Pop ();
		States.State<StateGame> ().goToNextLevel ();
	}
	
	public void onReplayClick(){
		States.Pop ();
		States.State<StateGame> ().replayLevel ();
	}

	public void onExitClick(){
		gui.hide();
		gui.warningWindow.show(onConfirmQuitClick, onCancelQuitClick);
	}
	
	public void onCancelQuitClick(){
		gui.warningWindow.hide();
		gui.show(lvl);
	}
	
	public void onConfirmQuitClick(){
		States.Pop ();
		States.State<StateGame>().quitGame();
	}
	
	public override void OnExit ()
	{
		gui.hide ();
		base.OnExit ();
	}
}
