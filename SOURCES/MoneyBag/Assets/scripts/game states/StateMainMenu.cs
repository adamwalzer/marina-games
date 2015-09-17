using UnityEngine;
using System.Collections;

public class StateMainMenu : BaseState
{
	GUIMainMenu gui;

	public override void OnEntered ()
	{
//		GameObject go = GameObject.FindGameObjectWithTag("MainObject");
		gui = GameObject.FindObjectOfType<GUIMainMenu>();
		base.OnEntered ();
		Global.instance.soundManager.playMusic(gui.music, true);
	}	

	public override void OnExit ()
	{
		base.OnExit ();
	}

	public override void OnPaused ()
	{
		base.OnPaused ();
	}

	public override void OnResume ()
	{
		base.OnResume ();
	}
}
