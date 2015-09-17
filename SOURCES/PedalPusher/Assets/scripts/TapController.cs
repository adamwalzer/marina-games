using UnityEngine;
using System.Collections;

public class TapController : MonoBehaviour
{
	public Robot robot;
	public Vector2 pushUpForce;
	bool isPlaying = false;

	void Update ()
	{
		if (!isPlaying)
			return;
		if (Input.GetMouseButtonDown (0)) {
			robot.push (pushUpForce, ForceMode2D.Impulse);
		}
		#if (UNITY_WEBPLAYER || UNITY_WEBGL) && !UNITY_EDITOR

		if(Input.GetKeyDown(KeyCode.Space)){
			robot.flip ();
		}
#else
		if (Input.GetMouseButtonDown (1)) {
			robot.flip ();
		}
#endif
	}
	
	public void init ()
	{
		isPlaying = true;
	}

	public void stop(){
		isPlaying = false;
	}
	
	public void toggle (bool state)
	{
		isPlaying = state;
	}
}
