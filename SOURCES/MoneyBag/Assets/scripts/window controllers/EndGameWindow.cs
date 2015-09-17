using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGameWindow : PopWindow
{
	public AudioClip starSound;
	public GameObject page1;
	public GameObject page2;
	public AudioClip backgroundMusic;

	float pitch;

	bool ready = false;

	public override void show(){
		base.show();
		page1.SetActive(true);
		page2.SetActive(false);
		StartCoroutine(waitForReady());
		Global.instance.soundManager.stopMusic(backgroundMusic);
	}
	
	public override void hide(){
		base.hide();
		ready = false;
	}

	public void onReplayClick(){
		if(!ready) return;
		States.State<PStateWin>().onReplayClick();
	}

	public void onOKClick(){
		if(!ready) return;
		States.State<PStateWin>().onCompleteGameClick();
	}

	public void onNextClick(){
		if(!ready) return;
//		States.State<PStateWin>().onNextGameClick();
		page1.SetActive(false);
		page2.SetActive(true);
	}

	IEnumerator waitForReady ()
	{
		yield return new WaitForSeconds (0.5f);
		ready = true;
	}
}
