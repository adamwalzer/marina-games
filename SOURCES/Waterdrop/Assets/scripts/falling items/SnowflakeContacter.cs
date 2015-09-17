using UnityEngine;
using System.Collections;

public class SnowflakeContacter : Contacter
{
	public int count;
	
	public override void onFallInBucket ()
	{
		count = (int)(count * transform.localScale.x);
		if(count == 0) count = 1;
		States.State<StateGame> ().addSnow (count);
		Destroy (gameObject);
	}

	public override void onFallPast ()
	{
		Destroy (gameObject);
	}
}
