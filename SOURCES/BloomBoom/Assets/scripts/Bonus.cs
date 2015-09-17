using UnityEngine;
using System.Collections;

public class Bonus : IClearable 
{
	public enum BonusType{
		Heart,
		Lightning,
		Flower
	}

	Transform came;
	public Bonus.BonusType type;
	public GameObject catchEffect;
	public AudioClip catchSound;

	void OnTriggerEnter2D (Collider2D c)
	{
		if (c.tag == "Player") {
			States.State<StateGame>().catchedBonus(this);
		}
	}

	public void init(){
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
