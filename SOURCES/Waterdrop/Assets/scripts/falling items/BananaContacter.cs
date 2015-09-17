using UnityEngine;
using System.Collections;

public class BananaContacter : Contacter
{
	void Awake ()
	{
		float m = States.State<StateGame> ().bananaMod;
		GetComponent<Rigidbody2D>().gravityScale *= m;
	}
	
	public override void onFallInBucket ()
	{
		States.State<StateGame> ().addBanana ();
		Destroy (gameObject);
	}

	public override void onFallPast ()
	{
		GetComponent<Animator> ().SetTrigger ("Fall");
	}
	
	public void onAnimationEnd ()
	{
		Destroy (gameObject);
	}
}
