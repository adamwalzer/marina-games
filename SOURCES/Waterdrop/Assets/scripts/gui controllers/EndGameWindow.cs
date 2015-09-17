using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGameWindow : MonoBehaviour
{
//	public GameObject page1;
//	public GameObject page2;	
	public Text totalScore;

	public void show ()
	{
		totalScore.text = "SCORE: " + States.State<StateGame> ().scores;
		gameObject.SetActive (true);
	}
	
	public void hide ()
	{
		gameObject.SetActive (false);
	}

	public void onNextClick(){
		States.State<PStateWin>().onCloseGameWindow();
	}

	public void onReplayClick(){
		States.State<PStateWin>().onReplayLevelClick();
	}
}
