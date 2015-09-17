using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;

public class EndLevelWindow : MonoBehaviour
{
	public Text score;
	public Text time;
	public GameObject[] locks;
	public AudioClip unlockSound;

	bool ready = false;

	public void show ()
	{
		StateGame gState = States.State<StateGame>();
		time.text = gState.getTimeResult;
		score.text = "SCORE: " + gState.scores;
		gameObject.SetActive (true);
		showUnlocks(gState.currentLevel);
		StartCoroutine (waitForReady (() =>{
			ready = true;
		}, 0.5f));
	}
	
	public void hide ()
	{
		ready = false;
		StopAllCoroutines();
		gameObject.SetActive (false);
	}
	
	public void onNextClick(){
		if(!ready) return;
		States.State<PStateEndLevel>().onNextClick();
	}
	
	public void onReplayClick(){
		if(!ready) return;
		States.State<PStateEndLevel>().onReplayClick();
	}
	void showUnlocks(int count){
		for(int i = 0; i < locks.Length; i++){
			locks[i].SetActive(i >= count);
			if(i == count){
				locks[i].GetComponent<Animator>().SetTrigger("Open");
				StartCoroutine(waitForReady(()=>{
					Global.instance.soundManager.playSound(unlockSound);
				}, 1f));

			}
		}
	}

	IEnumerator waitForReady (Action act, float time)
	{
		yield return new WaitForSeconds (time);
		act();
	}
}
