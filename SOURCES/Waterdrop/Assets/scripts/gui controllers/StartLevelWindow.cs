using UnityEngine;
using System.Collections;

public class StartLevelWindow : MonoBehaviour
{
	public void show ()
	{
		gameObject.SetActive (true);
	}
	
	public void hide ()
	{
		gameObject.SetActive (false);
	}
	
	public void onContinueClick ()
	{
		States.State<PStateStartLevel>().onCloseWindow();
	}
}
