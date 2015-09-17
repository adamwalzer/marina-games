using UnityEngine;
using System.Collections;

public class PGUIPause : MonoBehaviour {

	public PauseWindow window;
	
	public void show(){
		window.show();
	}
	
	public void hide(){
		window.hide();
	}
}
