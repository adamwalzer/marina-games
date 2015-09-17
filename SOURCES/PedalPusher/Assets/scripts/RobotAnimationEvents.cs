using UnityEngine;
using System.Collections;

public class RobotAnimationEvents : MonoBehaviour
{
	public Robot robot;

	public void onFlipAnimEnded(){
		robot.onFlipEnded();
	}

	public void onRockAnimEnded(){
		robot.onRockEnded();
	}

	public void onWaterAnimEnded(){
		robot.onWaterEnded();
	}
}
