using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndLevelWindow : MonoBehaviour
{
	public Text score;
	public Text time;

	bool ready = false;

	public void show ()
	{
		time.text = "TIME: " + States.State<StateGame>().getTimeResult;
//		score.text = States.State<StateGame>().scores.ToString();
		gameObject.SetActive (true);
		StartCoroutine (waitForReady ());
	}
	
	public void hide ()
	{
		ready = false;
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

	IEnumerator waitForReady ()
	{
		yield return new WaitForSeconds (0.5f);
		ready = true;
	}
}
