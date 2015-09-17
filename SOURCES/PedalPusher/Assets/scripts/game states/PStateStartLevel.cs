using UnityEngine;
using System.Collections;

public class PStateStartLevel : BaseState
{
	PGUIStartLevel gui;
	int lvl;

	public override void OnEntered ()
	{
		base.OnEntered ();
		gui = GameObject.FindObjectOfType<PGUIStartLevel>();
		lvl = States.State<StateGame> ().currentLevel;
		gui.show (lvl);
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

	public override void OnPaused ()
	{
		base.OnPaused ();
		gui.hide();
	}

	public override void OnResume ()
	{
		base.OnResume ();
		gui.show (lvl);
	}

	public override void OnExit ()
	{
		gui.hide ();
		gui.gameUI.SetActive(true);
		gui.hero.SetActive(true);
		base.OnExit ();
	}
}
