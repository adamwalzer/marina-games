using UnityEngine;
using System.Collections;

public class SunflowerBody : MonoBehaviour
{
	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject != this && coll.gameObject.tag == "Carbon") {
			coll.gameObject.GetComponent<Contacter> ().onFallInBucket ();
		}
	}
}
