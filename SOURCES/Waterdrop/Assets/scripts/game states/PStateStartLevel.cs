using UnityEngine;
using System.Collections;

public class PStateStartLevel : BaseState
{
	PGUIStartLevel gui;

	public override void OnEnter ()
	{
//		GameObject go = GameObject.FindGameObjectWithTag ("MainObject");
		gui = GameObject.FindObjectOfType<PGUIStartLevel> ();
		base.OnEnter ();
		int lvl = States.State<StateGame> ().currentLevel;
		gui.show (lvl);
	}

	public void onCloseWindow ()
	{
		States.Pop ();
		States.State<StateGame> ().startLevel ();
	}

	public override void OnExit ()
	{
		gui.hide ();
		base.OnExit ();
	}
}
