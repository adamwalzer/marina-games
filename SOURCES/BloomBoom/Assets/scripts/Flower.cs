using UnityEngine;
using System.Collections;

public class Flower : IClearable 
{
	Transform came;
	public GameObject catchEffect;
	public AudioClip catchSound;
	
	void OnTriggerEnter2D (Collider2D c)
	{
		if (c.tag == "Player") {
			States.State<StateGame>().onCatchedFlower(this);
		}
	}
	
	public void init(LevelSettings settings){
		gameObject.SetActive(true);
	}
	
	public override void deinit(){
		gameObject.SetActive(false);
	}
	
	public virtual void onCatched(){
		GameObject go = Global.instance.pool.get(catchEffect, transform.position);
		go.GetComponent<PoolableParticleSystem>().activate();
		Global.instance.soundManager.playSound(catchSound);
		deinit();
	}
}
