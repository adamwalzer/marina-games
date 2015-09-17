using UnityEngine;
using System.Collections.Generic;

public class PGUIEndLevel : MonoBehaviour
{
	public EndLevelWindow endWindows;
	public GameObject gameUI;
	public GameObject bottomPanel;
	public AudioClip music;
	
	public void show (int level)
	{
		endWindows.show (level);

	}

	public void hide ()
	{
		endWindows.hide ();
	}
}
