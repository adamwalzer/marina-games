using UnityEngine;
using System.Collections;

public class BucketBody : MonoBehaviour
{
	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject != this && coll.gameObject.tag == "Water") {
			coll.gameObject.GetComponent<Contacter> ().onFallPast ();
		}
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		Contacter c = other.gameObject.GetComponent<Contacter> ();
		if (c != null) {
			c.onFallInBucket ();
		}
	}
}
