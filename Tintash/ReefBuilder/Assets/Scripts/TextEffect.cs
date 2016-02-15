using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour {

	public Text _text;
	
	public Color a =Color.red;
	Color b =Color.black;
	public float changeTime; 
	public bool isWhite;
	public bool run;
	// Use this for initialization
	void OnEnable () {
		
		run = false;
		_text.color =b;
		isWhite = true;
	
		
		
	}
	void OnDisable()
	{
		_text.color =b;
		CancelInvoke ("ChangingColor");
	}
	// Update is called once per frame
	void ChangingColor () {
		if (isWhite) {
			_text.color = a;
			isWhite = false;
		}
		else {
			_text.color=b;
			isWhite=true;
		}
		
	}
	
	
	
	public void Change()
	{
		
		
			InvokeRepeating ("ChangingColor", changeTime, changeTime);
	}
}
