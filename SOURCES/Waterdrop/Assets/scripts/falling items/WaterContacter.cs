using UnityEngine;
using System.Collections;

public class WaterContacter : Contacter
{
	public ParticleEmitter dropletEffect;
	public GameObject view;

	//когда попадает в ведро - мелкие брызги и увеличение счетчика воды
	//когда ударяется об ведро - крупные брызги и ничего не начисляется
	public override void onFallInBucket ()
	{
		States.State<StateGame> ().addWater (transform.localScale.x);
		Destroy (gameObject);
	}

	public override void onFallPast ()
	{
		StartCoroutine (showEffect ());
	}

	IEnumerator showEffect ()
	{
		view.SetActive(false);
		dropletEffect.emit = true;
		yield return new WaitForSeconds (0.5f);
		dropletEffect.emit = false;
		Destroy (gameObject);
	}
}
