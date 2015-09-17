using UnityEngine;
using System.Collections;

public class RockReward : MonoBehaviour 
{
	public AudioClip rockSound;
	void OnEnable(){
		GetComponent<Collider2D>().enabled = true;
	}

	void OnTriggerEnter2D(Collider2D c){
		if(c.gameObject.tag == "Player"){
			States.State<StateGame>().jumpOnRockStack();
			GetComponent<Collider2D>().enabled = false;
			Global.instance.soundManager.playSound (rockSound);
		}
	}
}
