﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartLevelWindow : PopWindow
{
	public GameObject[] pages;

	bool readyToGo = false;

	int currentPage = 0;

	public override void show ()
	{
		readyToGo = false;
		if (pages.Length > 0) {
			currentPage = 0;
			pages [currentPage].SetActive (true);
			gameObject.SetActive (true);
			StartCoroutine(waitForReady());
			Debug.Log("PAGES " + pages.Length);
		} else {
			gameObject.SetActive (true);
			StartCoroutine(wait());
			Debug.Log("PAGES 0");
		}
	}

	public override void hide ()
	{
		gameObject.SetActive (false);
	}

	public void onOKClick ()
	{
		if(!readyToGo) return;
		currentPage++;
		if (currentPage < pages.Length) {
			pages [currentPage].SetActive (true);
			pages [currentPage - 1].SetActive (false);
		} else {
			pages [currentPage - 1].SetActive (false);
			currentPage = 0;
			States.State<PStateStartLevel> ().onPlayClick ();
		}
	}

	IEnumerator wait(){
		yield return null;
		States.State<PStateStartLevel> ().onPlayClick ();
	}

	IEnumerator waitForReady(){
		yield return new WaitForSeconds(0.5f);
		readyToGo = true;
	}
}