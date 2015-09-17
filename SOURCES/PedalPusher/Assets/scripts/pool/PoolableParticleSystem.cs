using UnityEngine;
using System.Collections;

public class PoolableParticleSystem : MonoBehaviour
{
	ParticleSystem pS;
	bool activated = false;

	void Update ()
	{
		if (!activated)
			return;
		if (pS != null) {
			if (!pS.IsAlive (true)) {
				gameObject.SetActive (false);
				activated = false;
			}
		} 
	}

	public void activate ()
	{
		pS = GetComponent<ParticleSystem> ();
		if(pS != null){
			activated = true;
			gameObject.SetActive (true);	
		}
	}	
}
