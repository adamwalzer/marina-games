using UnityEngine;
using System.Collections;

public class HeartGate : MonoBehaviour
{
	public Animator gate;
	public Collider2D gateCol;
	public GameObject lifeBonus;
	public Transform bonusPlace;
	GameObject bonus;
	bool isOpen = false;
	Transform hero;
	Transform trfm;

	void Update(){
		if(isOpen) return;
		if(Vector3.Distance(transform.position, hero.position) < 5f){
			open();
		}
	}

	public void init ()
	{
		gameObject.SetActive (true);
			bonus = Global.instance.pool.get (lifeBonus, bonusPlace.position);
			bonus.transform.parent = transform;
			bonus.SetActive (true);
			trfm = transform;
			hero = GameObject.FindObjectOfType<Hero>().transform;
	}

	public void deinit ()
	{
		isOpen = false;
		gate.SetTrigger ("Close");
		Destroy(bonus);
		gameObject.SetActive (false);
	}

	void open ()
	{
		gate.SetTrigger ("Open");
		isOpen = true;
	}
}
