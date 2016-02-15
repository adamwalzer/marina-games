using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnCompleteStarAnimation : MonoBehaviour {

public GameObject replayButton;
public GameObject nextButton;
public Sprite image;


	public void OnCompletionStarAnimation()
	{	
		replayButton.SetActive(true);
		nextButton.SetActive(true);
	}
	
	void OnDisable()
	{
		transform.GetComponent<Image>().sprite=image;
	}
}
