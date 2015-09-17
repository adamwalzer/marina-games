using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGameWindow : MonoBehaviour
{
	public GameObject page1;
	public GameObject page2;
	public Text score;

	public void show(){
		StateGame sG = States.State<StateGame>();
		score.text = sG.scores.ToString();
		gameObject.SetActive(true);
		page1.SetActive(true);
	}
	
	public void hide(){
		gameObject.SetActive(false);
		page1.SetActive(false);
		page2.SetActive(false);
	}
	
	public void onReplay(){
		States.State<PStateWin>().onReplayClick();
	}

	public void onNextClick(){
		page1.SetActive(false);
		page2.SetActive(true);
	}

	public void onFlipAwardsClick(){
		States.State<PStateWin>().onAwardsClick();
	}
}
