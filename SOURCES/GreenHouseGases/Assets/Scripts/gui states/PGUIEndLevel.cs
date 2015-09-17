using UnityEngine;
using System.Collections.Generic;

public class PGUIEndLevel : MonoBehaviour 
{
	public List<EndLevelWindow> endWindowsLM;
	public List<EndLevelWindow> endWindowsSM;
	public WarningWindow warningWindow;
	public AudioClip winMusic;

	EndLevelWindow currentWindow;
	
	public void show(int i){
		if(States.State<StateGame>().mode == StateGame.GameMode.LevelMode){
			currentWindow = endWindowsLM[i - 1];
		}else{
			currentWindow = endWindowsSM[i - 1];
		}
		currentWindow.show();
	}

	public void showEndGame(){
		currentWindow.show();
	}
	
	public void hide(){
		currentWindow.hide();
	}
}
