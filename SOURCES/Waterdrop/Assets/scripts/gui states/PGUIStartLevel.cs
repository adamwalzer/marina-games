using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PGUIStartLevel : MonoBehaviour 
{
	//первые три для сежима со стейджами
	//вторая тройка для аркажного режима
	public List<StartLevelWindow> startWindows;

	StartLevelWindow currentWindow;

	public void show(int i){
//		switch(i){
//		case 1 :
//			if(States.State<StateGame>().mode == StateGame.GameMode.StageMode){
//				currentWindow = startWindows[0];
//			}else{
//				currentWindow = startWindows[3];
//			}
//			break;
//		case 4 :
//			if(States.State<StateGame>().mode == StateGame.GameMode.StageMode){
//				currentWindow = startWindows[1];
//			}else{
//				currentWindow = startWindows[4];
//			}
//			break;
//		case 7 :
//			if(States.State<StateGame>().mode == StateGame.GameMode.StageMode){
//				currentWindow = startWindows[2];
//			}else{
//				currentWindow = startWindows[5];
//			}
//			break;
//		}
		currentWindow = startWindows[i - 1];
		currentWindow.show();
	}

	public void hide(){
		currentWindow.hide();
	}
}
