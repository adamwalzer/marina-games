using UnityEngine;
using System.Collections;

public class GUIMainMenu : MonoBehaviour 
{
	public Animator startButton;

	void Start(){
		startButton.SetTrigger("Activate");
	}

	public void onStartClick(){
		States.Change<StateGame>();
	}
}
