using UnityEngine;
using System.Collections.Generic;

public class PGUIEndLevel : MonoBehaviour 
{
	public List<EndLevelWindow> endWindowsLM;
	public List<EndLevelWindow> endWindowsSM;

	public EndLevelWindow endLvlWindow;
	
	EndLevelWindow currentWindow;
	
	public void show(){
		endLvlWindow.show();
		States.State<StateGame>().toggleBucket(false);
	}
	
	public void hide(){
		endLvlWindow.hide();
	}
}
