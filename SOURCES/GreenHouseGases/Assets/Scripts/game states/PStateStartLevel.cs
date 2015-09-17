using UnityEngine;
using System.Collections;

public class PStateStartLevel : BaseState
{
	PGUIStartLevel gui;
	int lvl;

	public override void OnEntered ()
	{
		GameObject go = GameObject.FindGameObjectWithTag ("MainObject");
		gui = go.GetComponentInChildren<PGUIStartLevel> ();
		base.OnEntered ();
		lvl = States.State<StateGame> ().currentLevel;
		gui.show (lvl);
	}

	public void onPlayClick ()
	{
		States.Pop ();
		States.State<StateGame> ().startLevel ();
	}

	public void onCloseClick ()
	{
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
