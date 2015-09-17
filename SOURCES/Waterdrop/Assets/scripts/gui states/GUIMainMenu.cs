using UnityEngine;
using System.Collections;

public class GUIMainMenu : MonoBehaviour 
{
	public void onStartClick(){
		States.Change<StateGame>();
	}
}
