using UnityEngine;
using System.Collections;

public class GrassDestroyer : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D coll)
	{
		coll.gameObject.GetComponent<Contacter> ().onFallPast ();
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		other.gameObject.GetComponent<Contacter> ().onFallPast ();
	}
}
