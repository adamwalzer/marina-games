﻿using UnityEngine;
using System.Collections;

public class StateMainMenu : BaseState
{
	GUIMainMenu gui;

	public override void OnEnter ()
	{
		gui = GameObject.FindObjectOfType<GUIMainMenu>();
		base.OnEnter ();
		gui.show();
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