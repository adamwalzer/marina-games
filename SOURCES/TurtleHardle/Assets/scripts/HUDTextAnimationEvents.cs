using UnityEngine;
using System.Collections;

public class HUDTextAnimationEvents : MonoBehaviour 
{
	public HUDText hudText;

	public void onShowEnded(){
//		hudText.deinit();
	}

	public void onIdleEnded(){
		hudText.deinit();
	}
}
