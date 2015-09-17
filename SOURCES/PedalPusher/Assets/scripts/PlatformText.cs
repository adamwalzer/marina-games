using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlatformText : MonoBehaviour 
{
	public Text text;

	void OnEnable(){
#if UNITY_WEBPLAYER
		text.text = "Use spacebar to make your bike spin.";
#else
		text.text = "Tap with two fingers to make your bike spin.";
#endif
	}
}
