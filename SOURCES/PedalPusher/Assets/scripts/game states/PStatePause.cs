using UnityEngine;
using System.Collections;

public class PStatePause : BaseState 
{
	PGUIPause gui;

	public override void OnEnter ()
	{
		base.OnEnter ();
		if(gui == null){
			GameObject go = GameObject.FindGameObjectWithTag("MainObject");
			gui = go.GetComponentInChildren<PGUIPause>();
		}
		gui.show();
		Time.timeScale = 0;
	}

	public void onWindowClose(){
		States.Pop();
	}
	
	public override void OnExit ()
	{
		base.OnExit ();
		gui.hide();
		Time.timeScale = 1;
	}
}
