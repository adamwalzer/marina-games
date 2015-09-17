using UnityEngine;
using System.Collections;

public class GUIMainMenu : MonoBehaviour 
{
	public AudioClip music;

	public void onStartClick(){
		States.Change<StateGame>();
	}

	public void playSound(AudioClip sound){
		Global.instance.soundManager.playSound(sound);
	}
}
