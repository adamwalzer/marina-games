using UnityEngine;
using System.Collections;

public class HeroAnimationEvents : MonoBehaviour
{
	public Hero parent;

	public void onCrashAnimEnded(){
		parent.onRockEnded();
	}
}
