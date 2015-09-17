using UnityEngine;
using System.Collections;

public class PStateQuitWarning : BaseState 
{
	PGUIQuitWarning gui;

	public override void OnEntered ()
	{
		base.OnEntered ();
		gui = GameObject.FindObjectOfType<PGUIQuitWarning>();
		gui.window.show(onCancelClick, onQuitClick);
		Time.timeScale = 0f;
	}

	void onQuitClick(){
		States.State<StateGame>().exitGame();
	}

	void onCancelClick(){
		gui.window.hide();
		States.Pop();
	}

	public override void OnExit ()
	{
		Time.timeScale = 1f;
		base.OnExit ();
	}
}
