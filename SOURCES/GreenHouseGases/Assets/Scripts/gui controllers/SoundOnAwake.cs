using UnityEngine;
using System.Collections;

public class SoundOnAwake : MonoBehaviour 
{
	public AudioClip sound;

	void OnEnable(){
		Global.instance.soundManager.playSound(sound);
	}
}
