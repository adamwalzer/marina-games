using UnityEngine;
using System.Collections;

public class PStateFail : BaseState
{
	public int scores;
	PGUIFail gui;

	public override void OnEntered ()
	{
		base.OnEntered ();
		if(gui == null){
			gui = GameObject.FindObjectOfType<PGUIFail>();
		}
		gui.window.show();
//		gui.window.scores.text = "Score: " + scores;
		gui.gameUI.SetActive(false);
		gui.hero.SetActive(false);
		Global.instance.soundManager.playMusic(gui.music, false);
	}

	public override void OnPaused ()
	{
		base.OnPaused ();
		gui.window.hide();
	}
	
	public override void OnResume ()
	{
		base.OnResume ();
		gui.window.show ();
	}
	
	public override void OnExit ()
	{
		gui.gameUI.SetActive(true);
		gui.window.hide();
		gui.warning.hide();
		base.OnExit ();
	}

	public void onRestartClick(){
		States.Pop ();
		States.State<StateGame>().replayLevel();
	}

	public void onExitClick(){
		gui.warning.show(onCancelClick, onQuitConfirmClick);
		gui.window.hide();
	}

	public void onCancelClick(){
		gui.window.show();
		gui.warning.hide();
	}

	public void onQuitConfirmClick(){
		States.Pop ();
		States.State<StateGame>().exitGame();
	}
}
