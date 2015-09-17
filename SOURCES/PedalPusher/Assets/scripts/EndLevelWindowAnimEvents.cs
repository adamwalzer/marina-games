using UnityEngine;
using System.Collections;

public class EndLevelWindowAnimEvents : MonoBehaviour 
{
	public AudioClip sound1;
	public AudioClip sound2;
	public AudioClip sound3;
	public EndLevelWindow window;

	public void oncashAnimEnd(){
		Global.instance.soundManager.playSound (sound1);
	}

	public void onInterestTextAnimEnd(){
		Global.instance.soundManager.playSound (sound1);
	}

	public void onInterestValueAnimEnd(){
		Global.instance.soundManager.playSound (sound1);
	}

	public void allAnimEnd(){
		window.onAnimationEnded ();
	}
}
