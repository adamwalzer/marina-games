using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGameWindow : MonoBehaviour
{

	public void show ()
	{
		gameObject.SetActive (true);
	}
	
	public void hide ()
	{
		gameObject.SetActive (false);
	}

	public void onDoneClick(){
		States.State<PStateWin>().onCompleteGameClick();
	}

	public void onTryAgainClick(){
		States.State<PStateWin>().onReplayGameClick();
	}
}
