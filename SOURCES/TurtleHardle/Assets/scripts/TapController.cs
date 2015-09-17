using UnityEngine;
using System.Collections;

public class TapController : MonoBehaviour
{
	public Flyer flyer;
	public Vector2 pushUpForce;
	public AudioClip tapSound;
	bool isPlaying = false;

	void Update ()
	{
		if (!isPlaying)
			return;
		if (Input.GetMouseButtonDown (0)) {
			flyer.push (pushUpForce, ForceMode2D.Impulse);
//			Global.instance.soundManager.playSoundOnChannel (SoundManager.PLANT_RUSTLE, tapSound, false);
		}
	}
	
	public void init ()
	{
		isPlaying = true;
	}

	public void stop(){
		isPlaying = false;
	}
}
