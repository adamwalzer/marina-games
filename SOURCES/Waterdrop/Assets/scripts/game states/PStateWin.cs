using UnityEngine;
using System.Collections;

public class PStateWin : BaseState 
{
	PGUIWin gui;

	public override void OnEntered ()
	{
		base.OnEntered ();
		if(gui == null){
//			GameObject go = GameObject.FindGameObjectWithTag("MainObject");
			gui = GameObject.FindObjectOfType<PGUIWin>();
		}
		gui.show();
	}

	public void onCloseLevelWindow(){
		States.Pop();
		States.State<StateGame>().goToNextLevel();
	}

	public void onReplayLevelClick(){
		States.Pop();
		States.State<StateGame>().replayLevel();
	}

	public void onCloseStageWindow(){
		States.Pop();
		States.State<StateGame>().stageCompleted();
	}

	public void onCloseGameWindow(){
		States.Pop();
		States.State<StateGame>().gameCompleted();
	}
	
	public override void OnExit ()
	{
		base.OnExit ();
		gui.hide();
		gui=null;
	}
}
