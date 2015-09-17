using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;

public class EndLevelWindow : MonoBehaviour
{
	public Text text;
	public string[] texts;
	public AudioClip backgroundMusic;
	bool ready = false;

	public void show (int level)
	{
		string s = (texts[level - 1]).Replace("_NL", "\n");
		text.text = s;
		gameObject.SetActive (true);
		StartCoroutine(waitForReady(()=>{
			ready = true;
		}, 0.5f));
		Global.instance.soundManager.stopMusic(backgroundMusic);
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

	IEnumerator waitForReady (Action act, float time)
	{
		yield return new WaitForSeconds (time);
		act();
	}
}
