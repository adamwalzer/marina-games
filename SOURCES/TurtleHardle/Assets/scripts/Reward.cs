using UnityEngine;
using System.Collections;

public class Reward : MonoBehaviour
{
	public enum RewardType{
		Points,
		Life,
		Speed
	}

	public AudioClip catchSound;
	public GameObject catchEffect;
	public GameObject hudText;
	public string[] idleTriggers;

	protected bool isWorking = false;
	protected Transform cam;
	protected float hideDistanse = 15;
	public int reward;

	void Update(){
		if(!isWorking) return;
		if(cam.position.x - transform.position.x > hideDistanse){
			deinit();
		}
	}

	void OnTriggerEnter2D (Collider2D c){
		if(c.tag == "Player"){
			onTriggerAction();
		}
	}

	public virtual void init(){
		isWorking = true;
		cam = Camera.main.transform;
		gameObject.SetActive(true);
		GetComponent<Animator>().SetTrigger(idleTriggers[Random.Range(0, idleTriggers.Length)]);
	}

	public virtual void deinit(){
		isWorking = false;
		StopAllCoroutines();
		gameObject.SetActive(false);
	}

	protected virtual void onTriggerAction(){
		States.State<StateGame>().catchReward(this);
	}
	
	public virtual void onCatched(){
		GameObject go = Global.instance.pool.get(catchEffect, transform.position);
		go.GetComponent<PoolableParticleSystem>().activate();
		Global.instance.soundManager.playSound(catchSound);
		if(reward != 0){
			GameObject hud = Global.instance.pool.get(hudText, transform.position);
			hud.transform.SetParent(transform.parent, false);
			hud.GetComponent<HUDText>().init(reward);
		}
		deinit();
	}
}
