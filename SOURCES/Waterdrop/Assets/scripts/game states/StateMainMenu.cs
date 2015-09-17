using UnityEngine;
using System.Collections;

public class StateMainMenu : BaseState
{
	GUIMainMenu gui;

	public override void OnEnter ()
	{
		GameObject go = GameObject.FindGameObjectWithTag("MainObject");
		gui = go.GetComponent<GUIMainMenu>();
		base.OnEnter ();
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
