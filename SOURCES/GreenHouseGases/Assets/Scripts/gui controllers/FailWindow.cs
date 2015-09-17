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
		States.State<PStateFail>().onRestartClick();
	}
	
	public void onExitClick(){
		States.State<PStateFail>().onExitClick();
	}

	public void onCloseClick(){
		States.State<PStateFail>().onExitClick();
	}
}
