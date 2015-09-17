using UnityEngine;
using System.Collections;

public class FrogContacter : Contacter 
{
	public Sprite frog1;
	public Sprite frog2;
	public Sprite frog3;

	void Awake ()
	{
		float m = States.State<StateGame> ().bananaMod;
		GetComponent<Rigidbody2D>().gravityScale *= m;
		float r = Random.value;
		if(r > 0 && r < 0.33f){
			GetComponent<SpriteRenderer>().sprite = frog2;
		}else if(r >= 0.33f && r < 0.66f){
			GetComponent<SpriteRenderer>().sprite = frog3;
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
