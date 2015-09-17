using UnityEngine;
using System.Collections;

public class PStateStartLevel : BaseState
{
	PGUIStartLevel gui;

	public override void OnEntered ()
	{
		base.OnEntered ();
		gui = GameObject.FindObjectOfType<PGUIStartLevel>();
		int lvl = States.State<StateGame> ().currentLevel;
		gui.show (lvl, lvl == 10 || lvl == 11 || lvl == 12);
		gui.gameUI.SetActive(false);
		gui.hero.SetActive(false);
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
		gui.hero.SetActive(true);
		base.OnExit ();
	}
}
