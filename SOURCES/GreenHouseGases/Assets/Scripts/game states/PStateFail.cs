using UnityEngine;
using System.Collections;

public class PStateFail : BaseState
{
	PGUIFail gui;

	public override void OnEntered ()
	{
		base.OnEntered ();
		if(gui == null){
			GameObject go = GameObject.FindGameObjectWithTag("MainObject");
			gui = go.GetComponentInChildren<PGUIFail>();
		}
		gui.failWindow.show();
		Global.instance.soundManager.playMusic(gui.failSound, false);
		gui.road.SetActive(false);
	}
	
	public override void OnExit ()
	{
		base.OnExit ();
		gui.failWindow.hide();
		gui.road.SetActive(true);
	}

	public void onRestartClick(){
		States.Pop ();
		States.State<StateGame>().replayLevel();
	}

	public void onExitClick(){
		gui.failWindow.hide();
		gui.warningWindow.show(onConfirmQuitClick, onCancelQuitClick);
	}

	public void onCancelQuitClick(){
		gui.warningWindow.hide();
		gui.failWindow.show();
	}

	public void onConfirmQuitClick(){
		States.Pop ();
		States.State<StateGame>().quitGame();
	}
}
