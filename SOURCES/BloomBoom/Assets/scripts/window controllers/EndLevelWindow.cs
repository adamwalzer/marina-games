using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndLevelWindow : MonoBehaviour
{
	public Text score;
	public Text time;

	bool readyToGo = false;

	public void show ()
	{
		StateGame sG = States.State<StateGame>();
//		time.text = sG.getTimeResult;
		score.text = sG.scores.ToString();
		readyToGo = false; 
		gameObject.SetActive (true);
		StartCoroutine(wait());
	}
	
	public void hide ()
	{
		gameObject.SetActive (false);
	}
	
	public void onNextClick(){
		if(!readyToGo) return;
		States.State<PStateEndLevel>().onNextClick();
	}
	
	public void onReplayClick(){
		if(!readyToGo) return;
		States.State<PStateEndLevel>().onReplayClick();
	}

	IEnumerator wait(){
		yield return new WaitForSeconds(0.5f);
		readyToGo = true;
	}
}
