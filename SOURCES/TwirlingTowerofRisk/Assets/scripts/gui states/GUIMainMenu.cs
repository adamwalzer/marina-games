using UnityEngine;
using System.Collections;

public class GUIMainMenu : MonoBehaviour 
{
	public Animator playBttnAnima;

	public void onStartClick(){
		States.Change<StateGame>();
	}
}
