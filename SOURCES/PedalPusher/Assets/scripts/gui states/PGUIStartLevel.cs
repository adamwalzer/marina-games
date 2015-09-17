using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PGUIStartLevel : MonoBehaviour 
{
	public List<StartLevelWindow> startWindows;
	public GameObject gameUI;
	public GameObject hero;

	StartLevelWindow currentWindow;

	public void show(int i){
		currentWindow = startWindows[i - 1];
		currentWindow.show();
	}

	public void hide(){
		currentWindow.hide();
	}
}
