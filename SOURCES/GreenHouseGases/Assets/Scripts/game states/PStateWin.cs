using UnityEngine;
using System.Collections;

public class PStateWin : BaseState 
{
	PGUIWin gui;

	public override void OnEntered ()
	{
		base.OnEntered ();
		if(gui == null){
			GameObject go = GameObject.FindGameObjectWithTag("MainObject");
			gui = go.GetComponentInChildren<PGUIWin>();
		}
		Global.instance.soundManager.playMusic(gui.winSound, false);
		gui.endGameWindow.show();
	}

	public void onCompleteGameClick(){
		States.Pop();
		States.State<StateGame>().completeGame();
	}

	public void onReplayGameClick(){
		States.Pop();
		States.State<StateGame>().replayGame();
	}
	
	public override void OnExit ()
	{
		base.OnExit ();
		gui.endGameWindow.hide();
		gui=null;
	}
}
