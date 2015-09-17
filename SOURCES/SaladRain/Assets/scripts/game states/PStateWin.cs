using UnityEngine;
using System.Collections;

public class PStateWin : BaseState 
{
	PGUIWin gui;

	public override void OnEntered ()
	{
		base.OnEntered ();
		if(gui == null){
			gui = GameObject.FindObjectOfType<PGUIWin>();
		}
		gui.endGameWindow.show();
		gui.gameUI.SetActive(false);
		Global.instance.soundManager.playMusic(gui.music, false);
	}

	public void onCompleteGameClick(){
		States.Pop();
		States.State<StateGame>().exitGame();
	}

	public void onNextGameClick(){
		States.Pop();
		States.State<StateGame>().exitGame();
	}

	public override void OnExit ()
	{
		gui.gameUI.SetActive(true);
		base.OnExit ();
		gui.endGameWindow.hide();
		Global.instance.soundManager.stopMusic(gui.music);
		gui=null;
	}
}
