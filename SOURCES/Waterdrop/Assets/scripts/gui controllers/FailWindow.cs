using UnityEngine;
using System.Collections;

public class FailWindow : MonoBehaviour 
{
	public void show(){
		gameObject.SetActive(true);
	}
	
	public void hide(){
		gameObject.SetActive(false);
	}
	
	public void onRestartClick(){
		States.State<StateGame>().replayLevel();
		States.Pop();
	}
	
	public void onExitClick(){
		States.State<StateGame> ().onQuitClick ();
	}
}
