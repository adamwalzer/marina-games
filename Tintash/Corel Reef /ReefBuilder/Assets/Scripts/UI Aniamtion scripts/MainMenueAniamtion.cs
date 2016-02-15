using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MainMenueAniamtion : MonoBehaviour {

	// Use this for initialization

	public GameObject _Button;
	
	public void OnStartFunction()
	{
		_Button.SetActive(false);
	}
		
				public void ActivateThebutton()
				{
		_Button.SetActive(true);
				}
}
