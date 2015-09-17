using UnityEngine;
using System.Collections;

public class Fruit : IClearable
{
	public AudioClip catchSound;
	public GameObject catchEffect;
	public GameObject hudText;

	public int reward;

	void OnTriggerEnter2D (Collider2D c){
		if(c.tag == "Player"){
			States.State<StateGame>().catchFruit(this);
		}
	}

	public void init(){
		gameObject.SetActive(true);
	}

	public override void deinit(){
		gameObject.SetActive(false);
	}
	
	public void onCatched(){
		GameObject go = Global.instance.pool.get(catchEffect, transform.position);
		go.GetComponent<PoolableParticleSystem>().activate();
		Global.instance.soundManager.playSound(catchSound);
		GameObject hud = Global.instance.pool.get(hudText, transform.position);
		hud.GetComponent<HUDText>().init(reward);
		deinit();
	}
}
