using UnityEngine;
using System.Collections;

public class TapController : MonoBehaviour
{
	public Hero robot;
	public Vector2 pushUpForce;
	bool isPlaying = false;

	void Update ()
	{
		if (!isPlaying)
			return;
		if (Input.GetMouseButtonDown (0)) {
			robot.push (pushUpForce, ForceMode2D.Impulse);
		}
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
