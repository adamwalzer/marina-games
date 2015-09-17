using UnityEngine;
using System.Collections;

public class PStateFail : BaseState
{
	PGUIFail gui;

	public override void OnEnter ()
	{
		base.OnEnter ();
		if(gui == null){
			GameObject go = GameObject.FindGameObjectWithTag("MainObject");
			gui = go.GetComponentInChildren<PGUIFail>();
		}
		gui.show();
	}
	
	public override void OnExit ()
	{
		base.OnExit ();
		gui.hide();
	}
}
