using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGameWindow : PopWindow
{
	public AudioClip starSound;

	float pitch;

	bool ready = false;

	public override void show(){
		base.show();
		StartCoroutine(waitForReady());
	}
	
	public override void hide(){
		base.hide();
		ready = false;
	}
	
	public void onOKClick(){
		if(!ready) return;
		States.State<PStateWin>().onCompleteGameClick();
	}

	public void onNextClick(){
		if(!ready) return;
		States.State<PStateWin>().onNextGameClick();
	}

	IEnumerator waitForReady ()
	{
		yield return new WaitForSeconds (0.5f);
		ready = true;
	}
}
