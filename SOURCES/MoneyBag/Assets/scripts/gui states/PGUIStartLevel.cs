using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PGUIStartLevel : MonoBehaviour 
{
	public GameObject mainWindow;
	public List<StartLevelWindow> startWindows;
	public GameObject gameUI;
	public GameObject bottomPanel;
	public AudioClip music;

	StartLevelWindow currentWindow;

	public void show(int i){
		mainWindow.SetActive(true);
		currentWindow = startWindows[i - 1];
		currentWindow.show();
	}

	public void hide(){
		mainWindow.SetActive(false);
		currentWindow.hide();
	}
}
