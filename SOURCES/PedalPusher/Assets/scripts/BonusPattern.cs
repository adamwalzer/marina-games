using UnityEngine;
using System.Collections.Generic;

public class BonusPattern : MonoBehaviour
{
	public GameObject starPrefab;
	public GameObject heartPrefab;
	public GameObject coinPrefab;
	public Transform[] places;
	protected bool isWorking = false;
	protected Transform came;
	protected float invisDistance = 10f;
	protected LevelSettings settings;


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
		foreach (Transform t in places) {
			GameObject bo = Global.instance.pool.get(getBonus(), t.position);
			bo.transform.parent = transform.parent;
			bo.GetComponent<Bonus>().init();
		}
		gameObject.SetActive (true);
		isWorking = true;
	}

	public void deinit ()
	{
		gameObject.SetActive (false);
		isWorking = false;
	}

	protected virtual GameObject getBonus ()
	{
		if (Random.value < settings.heartChance) {
			if(States.State<StateGame> ().needCreateHeart())
			return heartPrefab;
		}
		if (Random.value < settings.starChance) {
			return starPrefab;
		}
		return coinPrefab;
	}
}
