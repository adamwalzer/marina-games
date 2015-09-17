using UnityEngine;
using System.Collections;

public class SaltContacter : Contacter
{
	public override void onFallInBucket ()
	{
		States.State<StateGame> ().addSalt ();
		Destroy(gameObject);
	}

	public override void onFallPast ()
	{
		GetComponent<Animator>().SetTrigger("Fall");
	}
	
	public void onAnimationEnd(){
		Destroy(gameObject);
	}
}
