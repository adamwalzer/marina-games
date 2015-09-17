using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour 
{
	public enum BonusType
	{
		Lightning,
		MoneyBag
	}
	public SpriteRenderer sprite;
	public BonusType type;
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
			if(type == BonusType.Lightning){
				States.State<StateGame>().onLightningCatched();
			}else if(type == BonusType.MoneyBag){
				States.State<StateGame>().onMoneyBagCatched(transform.position);
			}

			Global.instance.soundManager.playSound(sound);
			Instantiate(effect, transform.position, Quaternion.identity);
			gameObject.SetActive(false);
		}
	}
}
