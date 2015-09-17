using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinWindow : MonoBehaviour 
{
	public void show(){
		gameObject.SetActive(true);
	}

	public void hide(){
		gameObject.SetActive(false);
	}

	public void onContinueClick(){
		States.State<PStateWin>().onCloseStageWindow();
	}

	public void onExitClick(){
		Application.Quit();
	}
}
