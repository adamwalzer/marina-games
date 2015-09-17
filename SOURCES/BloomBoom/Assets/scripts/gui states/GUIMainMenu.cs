using UnityEngine;
using System.Collections;

public class GUIMainMenu : MonoBehaviour 
{
	public Animator anima;

	public void show(){
		anima.SetTrigger("Show");
	}

	public void onStartClick(){
		States.Change<StateGame>();
	}
}
