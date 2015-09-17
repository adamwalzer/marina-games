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
		Global.instance.soundManager.playMusic(gui.music, false);
		gui.gameUI.SetActive(false);
		gui.bottomPanel.SetActive(false);
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
		gui.bottomPanel.SetActive(true);
		Global.instance.soundManager.stopMusic(gui.music);
		base.OnExit ();
	}
}
