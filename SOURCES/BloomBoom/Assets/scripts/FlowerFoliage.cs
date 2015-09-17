using UnityEngine;
using System.Collections;

public class FlowerFoliage : IClearable 
{
	Transform came;
	public GameObject catchEffect;
	public AudioClip catchSound;
	
	void OnTriggerEnter2D (Collider2D c)
	{
		if (c.tag == "Player") {
			States.State<StateGame>().onPollenFlower(this);
			GetComponent<Collider2D>().enabled = false;
		}
	}
	
	public void init(LevelSettings settings){
		gameObject.SetActive(true);
		GetComponent<Collider2D>().enabled = true;
		GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
	}
	
	public override void deinit(){
		gameObject.SetActive(false);
	}
	
	public virtual void onCatched(){
		GameObject go = Global.instance.pool.get(catchEffect, transform.position);
		go.GetComponent<PoolableParticleSystem>().activate();
		Global.instance.soundManager.playSound(catchSound);
//		deinit();
		GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
	}
}
