using UnityEngine;
using System.Collections;

public class Balloon : IClearable 
{
	public GameObject catchEffect;
	public AudioClip catchSound;
	public float width;

	bool isWorking = false;
	Transform came;
	float invisDistance = 10f;
	bool isCathced = false;

	void Update(){
		if(!isWorking) return;
		if(came == null){
			came = Camera.main.transform;
		}
		if(came.position.x > transform.position.x + invisDistance){
			deinit();
		}
	}

	
	void OnTriggerEnter2D (Collider2D c)
	{
		if (!isCathced && c.tag == "Player") {
			GameObject go = Global.instance.pool.get(catchEffect, transform.position);
			go.GetComponent<PoolableParticleSystem>().activate();
			Global.instance.soundManager.playSound(catchSound);
			onCatched();
			isCathced = true;
		}
	}

	public void init(){
		gameObject.SetActive(true);
		GetComponent<Collider2D>().enabled = true;
		isWorking = true;
		isCathced = false;
	}
	
	public override void deinit(){
		GetComponent<Collider2D>().enabled = false;
		gameObject.SetActive(false);
		isWorking = false;
	}

	public virtual void onCatched(){
		States.State<StateGame>().balloonCatched();
		deinit();
	}
}
