using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour 
{
	public GameObject reward;

	void OnEnable(){
		reward.SetActive(true);
	}
	
	void OnTriggerEnter2D(Collider2D c){
		if(c.gameObject.tag == "Player"){
			reward.SetActive(false);
		}
	}
}
