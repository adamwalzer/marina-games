using UnityEngine;
using System.Collections;

public class MusicSlot : MonoBehaviour
{
	public AudioClip music;
	public bool loop = true;

	void Start() {
		Global.instance.soundManager.playMusic(music, loop);
	}

	void OnDestroy() {
		if (Global.instance != null) {
			Global.instance.soundManager.stopMusic(music);
		}
	}
}
