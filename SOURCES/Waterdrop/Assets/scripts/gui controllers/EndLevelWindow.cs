using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndLevelWindow : MonoBehaviour
{
	public Text scores;

	public void show ()
	{
		scores.text = "SCORE: " + States.State<StateGame>().scores;
		gameObject.SetActive (true);
	}
	
	public void hide ()
	{
		gameObject.SetActive (false);
	}
	
	public void onNextClick(){
		States.State<PStateEndLevel>().onNextClick();
	}
	
	public void onReplayClick(){
		States.State<PStateEndLevel>().onReplayClick();
	}

	public void onStageCompleteClick(){
		States.State<PStateEndLevel>().onStageCompletedClick();
	}
}
