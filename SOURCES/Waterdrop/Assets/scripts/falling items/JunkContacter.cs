using UnityEngine;
using System.Collections;

public class JunkContacter : Contacter
{
	public Sprite umbrella;
	public Sprite sneaker;
	public SpriteRenderer view;

	void Awake ()
	{
		float m = States.State<StateGame> ().bananaMod;
		GetComponent<Rigidbody2D>().gravityScale *= m;
		float r = Random.value;
		if(r > 0 && r < 0.5f){
			view.sprite = sneaker;
		}
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
