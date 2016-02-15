using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameCompleteWindow : MonoBehaviour {

public 	Text []_text;
	// Use this for initialization
	void OnEnable() {
		int j=1;
		int sum=0;
		for(int i=0;i<5;i++)
		{
			_text[i].text ="Level "+j.ToString()+"  : "+Controller.GetInstance().levelScores[i]; 
			sum=sum+Controller.GetInstance().levelScores[i];
			j++;
		}
		
		_text[5].text ="Total     : "+sum.ToString();
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
