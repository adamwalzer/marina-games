using UnityEngine;
using System.Collections;

public class EndLevelWindow : MonoBehaviour
{
	public void show ()
	{
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

	public void onExitClick(){
		States.State<PStateEndLevel>().onExitClick();
	}
}
