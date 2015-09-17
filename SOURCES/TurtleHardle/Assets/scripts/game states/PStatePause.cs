using UnityEngine;
using System.Collections;

public class PStatePause : BaseState 
{
	PGUIPause gui;

	public override void OnEnter ()
	{
		base.OnEnter ();
		if(gui == null){
			gui =  GameObject.FindObjectOfType<PGUIPause>();
		}
		gui.window.show(onQuitConfirmClick, onWindowClose);
		Time.timeScale = 0;
	}

	public void onWindowClose(){
		States.Pop();
	}

	public void onQuitConfirmClick(){
		States.Pop ();
		States.State<StateGame>().exitGame(false);
	}
	
	public override void OnExit ()
	{
		base.OnExit ();
		gui.window.hide();
		Time.timeScale = 1;
	}
}
