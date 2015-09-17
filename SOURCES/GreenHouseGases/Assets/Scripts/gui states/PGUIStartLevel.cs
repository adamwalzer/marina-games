using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PGUIStartLevel : MonoBehaviour 
{
	//первые три для сежима со стейджами
	//вторая тройка для аркажного режима
	public List<StartLevelWindow> startWindows;
	public WarningWindow warningWindow;

	StartLevelWindow currentWindow;

	public void show(int i){
		currentWindow = startWindows[i - 1];
		currentWindow.show();
	}

	public void hide(){
		currentWindow.hide();
	}
}
