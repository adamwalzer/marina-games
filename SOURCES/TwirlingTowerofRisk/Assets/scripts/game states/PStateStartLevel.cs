using UnityEngine;
using System.Collections;

public class PStateStartLevel : BaseState
{
	PGUIStartLevel gui;

	public override void OnEntered ()
	{
		gui = GameObject.FindObjectOfType<PGUIStartLevel>();
		base.OnEntered ();
		int lvl = States.State<StateGame> ().currentLevel;
		gui.show (lvl);
		gui.gameUI.SetActive(false);
	}

	public void onPlayClick ()
	{
		States.Pop ();
		States.State<StateGame> ().startLevel ();
	}

	public void onCloseClick ()
	{
		States.Pop ();
		States.State<StateGame> ().startLevel ();
	}

	public override void OnExit ()
	{
		gui.hide ();
		gui.gameUI.SetActive(true);
		base.OnExit ();
	}
}
