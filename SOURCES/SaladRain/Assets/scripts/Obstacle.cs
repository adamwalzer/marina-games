using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{
	public AudioClip sound;

	void OnCollisionEnter2D(Collision2D col){
		if(col.collider.tag == "Floor"){
			gameObject.SetActive(false);
		}
	}

	public void onCatched(){
		Global.instance.soundManager.playSound(sound);
		gameObject.SetActive(false);
	}
}
