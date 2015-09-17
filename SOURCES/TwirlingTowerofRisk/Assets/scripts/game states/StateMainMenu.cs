using UnityEngine;
using System.Collections;

public class StateMainMenu : BaseState
{
	GUIMainMenu gui;

	public override void OnEntered ()
	{
		gui = GameObject.FindObjectOfType<GUIMainMenu>();
		base.OnEntered ();
		gui.playBttnAnima.SetTrigger("Show");
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
