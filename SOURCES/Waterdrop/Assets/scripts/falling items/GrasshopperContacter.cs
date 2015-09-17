using UnityEngine;
using System.Collections;

public class GrasshopperContacter : Contacter
{
	public Vector2 jumpForce = new Vector2 (10, 10);

	void Start(){
		jumpForce *= States.State<StateGame>().currentSpeedMod;
	}

	public override void onFallInBucket ()
	{
		States.State<StateGame> ().addGrasshopper ();
		Destroy(gameObject);
	}

	public override void onFallPast ()
	{
		GetComponent<Rigidbody2D>().AddForce (jumpForce, ForceMode2D.Impulse);
	}
}
