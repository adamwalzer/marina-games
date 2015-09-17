using UnityEngine;
using System;
using System.Collections;

public class DestroyOnContact : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other){
		CarbonContacter cC = other.gameObject.GetComponent<CarbonContacter>();
		if(cC != null && cC.gameObject.tag == "Carbon"){
			States.State<StateGame>().addSmog(cC.size);
		}
		Destroy(other.gameObject);
	}

	void OnCollisionEnter2D(Collision2D other){
		Destroy(other.gameObject);
	}
}