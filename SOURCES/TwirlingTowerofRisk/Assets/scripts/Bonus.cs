using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour 
{
	public enum BonusType{
		Star,
		Heart,
		Lightning
	}

	public GameObject catchEffectStar;
	public GameObject catchEffectHeart;
	public GameObject catchEffectLightning;
	public AudioClip catchSoundStar;
	public AudioClip catchSoundHeart;
	public AudioClip catchSoundLightning;
	public BonusType type;
	public Sprite starSp;
	public Sprite heartSp;
	public Sprite lightningSp;
	
	protected Transform came;
	protected GameObject catchEffect;
	protected AudioClip catchSound;

	void OnTriggerEnter2D (Collider2D c)
	{
		if (c.tag == "Player") {
			switch(type){
			case BonusType.Star :
				States.State<StateGame>().catchPointsBonus(this);
				break;
			case BonusType.Heart :
				States.State<StateGame>().catchLifeBonus(this);
				break;
			case BonusType.Lightning :
				States.State<StateGame>().catchSpeedBonus(this);
				break;
			default :
				States.State<StateGame>().catchPointsBonus(this);
				break;
			}
		}
	}

	public virtual void init(BonusType t){
		type = t;
		switch(type){
		case BonusType.Star :
			GetComponent<SpriteRenderer>().sprite = starSp;
			catchEffect = catchEffectStar;
			catchSound = catchSoundStar;
			break;
		case BonusType.Lightning :
			GetComponent<SpriteRenderer>().sprite = lightningSp;
			catchEffect = catchEffectLightning;
			catchSound = catchSoundLightning;
			break;
		case BonusType.Heart :
			GetComponent<SpriteRenderer>().sprite = heartSp;
			catchEffect = catchEffectHeart;
			catchSound = catchSoundHeart;
			break;
		}
		gameObject.SetActive(true);
	}
	
	public void deinit(){
		gameObject.SetActive(false);
	}

	public virtual void onCatched(){
		GameObject go = Global.instance.pool.get(catchEffect, transform.position);
		go.GetComponent<PoolableParticleSystem>().activate();
		Global.instance.soundManager.playSound(catchSound);
		deinit();
	}
}
