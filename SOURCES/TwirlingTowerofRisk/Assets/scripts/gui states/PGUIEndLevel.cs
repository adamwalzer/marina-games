using UnityEngine;
using System.Collections.Generic;

public class PGUIEndLevel : MonoBehaviour
{
	public List<EndLevelWindow> endWindows;
	public GameObject gameUI;
	public AudioClip music;
	EndLevelWindow currentWindow;
	
	public void show (int i, int scores, string time)
	{
		currentWindow = endWindows [i - 1];
		currentWindow.show ();
		currentWindow.score.text = "SCORE: " + scores;
	}

	public void hide ()
	{
		currentWindow.hide ();
	}
}
