using UnityEngine;
using System.Collections;

public class Shark : DanderousItem 
{
	public float width;
	public SharkMotor motor;
	public float speed;
	public float soundDistance;
	Transform target;
	bool near = false;

	void Update(){
		if(target == null) return;
		if(near){
			if(Vector2.Distance(target.position, transform.position) > soundDistance){
				States.State<StateGame>().changeShark(false);
				near = false;
			}
		}else{
			if(Vector2.Distance(target.position, transform.position) <= soundDistance){
				States.State<StateGame>().changeShark(true);
				near = true;
			}
		}

	}

	public override void init ()
	{
		base.init ();
		target = GameObject.FindObjectOfType<Flyer>().transform;
	}

	public override void deinit ()
	{
		base.deinit ();
		target = null;
	}

	public override void onCatched ()
	{
		StartCoroutine(wait());
		States.State<StateGame>().dangareousItemCatched();
	}

	IEnumerator wait(){
		GetComponent<Collider2D>().enabled = false;
		yield return new WaitForSeconds(1.5f);
		GetComponent<Collider2D>().enabled = true;
	}
}
