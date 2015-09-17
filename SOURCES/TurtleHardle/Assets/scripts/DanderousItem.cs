using UnityEngine;
using System.Collections;

public class DanderousItem : Reward 
{
	bool isCathced = false;

	void Update(){
		if(!isWorking) return;
		if(cam == null){
			cam = Camera.main.transform;
		}
		if(cam.position.x > transform.position.x + hideDistanse){
			deinit();
		}
	}

	public override void init(){
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

	public override void onCatched(){
		States.State<StateGame>().dangareousItemCatched();
		deinit();
	}

	protected override void onTriggerAction ()
	{
		if(isCathced) return;
		GameObject go = Global.instance.pool.get(catchEffect, transform.position);
		go.GetComponent<PoolableParticleSystem>().activate();
		Global.instance.soundManager.playSound(catchSound);
		onCatched();
		isCathced = true;
	}
}
