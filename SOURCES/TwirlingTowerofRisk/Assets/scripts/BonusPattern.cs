using UnityEngine;
using System.Collections.Generic;

public class BonusPattern : IClearable
{
	bool isWorking = false;
	Transform came;
	float invisDistance = 10f;
	LevelSettings settings;
	List<Bonus> bonuses;

	static int lightingsCount;

	void Awake ()
	{
		bonuses = new List<Bonus> (gameObject.GetComponentsInChildren<Bonus> ());
	}

	void Update ()
	{
		if (!isWorking)
			return;
		if (came == null) {
			came = Camera.main.transform;
		}
		if (came.position.x > transform.position.x + invisDistance) {
			deinit ();
		}
	}

	public void init (LevelSettings settings_)
	{
		settings = settings_;
		foreach (Bonus b in bonuses) {
			Bonus.BonusType t = States.State<StateGame>().getBonusType();
			b.init (t);
		}
		gameObject.SetActive (true);
		isWorking = true;
	}

	public override void deinit ()
	{
		gameObject.SetActive (false);
		isWorking = false;
		foreach (Bonus b in bonuses) {
			b.deinit ();
		}
	}

//	Bonus.BonusType getBonus ()
//	{
//		float r = Random.value;
//		if (r <= settings.lightningChance) {
//			return Bonus.BonusType.Lightning;
//		} else if (r <= settings.lightningChance + settings.heartChance && States.State<StateGame> ().damaged) {
//			return Bonus.BonusType.Heart;
//		}
//		return  Bonus.BonusType.Star;
//	}
}
