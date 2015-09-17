using UnityEngine;
using System.Collections;

public class RainbowContacter : Contacter
{	
	public override void onFallInBucket ()
	{
		States.State<StateGame> ().addRainbow ();
		Destroy(gameObject);
	}

	public override void onFallPast ()
	{
		Destroy (gameObject);
	}
}
