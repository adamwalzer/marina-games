using UnityEngine;
using System.Collections.Generic;

public class PGUIWin : MonoBehaviour
{
	public EndGameWindow endGameWindow;
	
	public void show ()
	{
		endGameWindow.show ();
	}

	public void hide ()
	{
		endGameWindow.hide ();
	}
}
