using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PGUIStartLevel : MonoBehaviour 
{
	public List<StartLevelWindow> startWindows;
	public GameObject gameUI;
	public GameObject hero;
	public GameObject backgroundDay;
	public GameObject backgroundNight;

	StartLevelWindow currentWindow;

	public void show(int i, bool isNight){
		backgroundDay.SetActive (!isNight);
		backgroundNight.SetActive (isNight);
		currentWindow = startWindows[i - 1];
		currentWindow.show();
	}

	public void hide(){
		backgroundDay.SetActive(false);
		backgroundNight.SetActive(false);
		currentWindow.hide();
	}
}
