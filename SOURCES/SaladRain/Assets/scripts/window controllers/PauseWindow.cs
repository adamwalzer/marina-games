using UnityEngine;
using System.Collections;

public class PauseWindow : PopWindow
{
	public override void show(){
		gameObject.SetActive(true);
	}
	
	public override void hide(){
		gameObject.SetActive(false);
	}
	
	public void onOKClick(){
		States.State<PStatePause>().onWindowClose();
	}

}
