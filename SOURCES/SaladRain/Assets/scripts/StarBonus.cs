using UnityEngine;
using System.Collections;

public class StarBonus : MonoBehaviour 
{
	public SpriteRenderer sprite;
	public int reward;
	public AudioClip sound;
	public GameObject effect;
	
	void OnCollisionEnter2D(Collision2D col){
		if(col.collider.tag == "Floor"){
			gameObject.SetActive(false);
		}
	}

	public void OnTriggerEnter2D(Collider2D col){
		if(col.tag == "Player"){
			Global.instance.soundManager.playSound(sound);
			States.State<StateGame>().onStarCatched();
			Global.instance.soundManager.playSound(sound);
			Instantiate(effect, transform.position, Quaternion.identity);
			gameObject.SetActive(false);
		}
	}
}
