using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StartLevelWindow : MonoBehaviour
{
	public List<GameObject> pages;

	int counter = 0;
	GameObject currentPage;

	public void show ()
	{
		counter = 0;
		gameObject.SetActive (true);
		currentPage = pages[counter];
		currentPage.SetActive(true);
	}
	
	public void hide ()
	{
		currentPage.SetActive(false);
		gameObject.SetActive (false);
	}
	
	public void onPlayClick ()
	{
		counter++;
		if(counter < pages.Count){
			currentPage.SetActive(false);
			currentPage = pages[counter];
			currentPage.SetActive(true);
		}else{
			States.State<PStateStartLevel>().onPlayClick();
		}	
	}

	public void onCloseClick ()
	{
		States.State<PStateStartLevel>().onCloseClick();
	}
}
