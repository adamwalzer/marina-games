using UnityEngine;
using System.Collections;

public class Jellyfish : MonoBehaviour 
{
	public AudioSource jellySound;
	bool isMoving = false;

	Transform target;

	void Update () {
		if(isMoving || target == null) return;
		if(Vector2.Distance(transform.position, target.position) < 10){
			GetComponent<Animator>().SetTrigger("Move");
			jellySound.Play();
			isMoving = true;
		}
	}

	void OnEnable(){
		target = GameObject.FindObjectOfType<Flyer>().transform;
		isMoving = false;
	}

	public void onUpAnimEnded(){
		GetComponent<Animator>().SetTrigger("Show1");
		jellySound.Stop();
		GetComponent<Reward>().deinit();
	}
}
