using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGameWindow : PopWindow
{
	public Animator stars;
	public AudioClip starSound;
	public Text score;
	public Text time;

	float pitch;

	bool ready = false;

	public override void show(){
		base.show();
		StateGame gState = States.State<StateGame>();
		time.text = gState.getTimeResult;
		score.text = "SCORE: " + gState.scores;
		pitch = 1f;
		stars.SetTrigger("Show");
		StartCoroutine(waitForReady());
	}
	
	public override void hide(){
		base.hide();
		ready = false;
	}
	
	public void onQuitClick(){
		if(!ready) return;
		States.State<PStateWin>().onCompleteGameClick();
	}

	public void onRestartClick(){
		if (!ready)
			return;
		States.State<PStateWin>().onRestartGameClick();
	}

	public void onStarAnimEnd(){
		Global.instance.soundManager.playSound(starSound, pitch);
		pitch += 0.1f;
	}

	IEnumerator waitForReady ()
	{
		yield return new WaitForSeconds (0.5f);
		ready = true;
	}
}
