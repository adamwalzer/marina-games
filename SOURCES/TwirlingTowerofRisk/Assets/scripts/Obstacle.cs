using UnityEngine;

//using System;
using System.Collections.Generic;

public class Obstacle : MonoBehaviour
{
	public GameObject[] bonusPatterns;
	public BonusePlace[] places;
	public GameObject[] plants;
	public float width;
	LevelSettings _settings;
	Transform _camera;
	float _invisDistance = 15f;
	bool isViewed = false;
	
	void Update ()
	{
		if (!isViewed)
			return;
		if (transform.position.x < _camera.position.x - _invisDistance) {
//			deinit ();
			Destroy(gameObject);
		}
	}
	
	public void init (LevelSettings settings_)
	{
		_settings = settings_;
		_camera = Camera.main.transform;
		gameObject.SetActive (true);
		isViewed = true;
		addPlants ();
		if (transform.position.x > 5) {
			addBonuses ();
		}		
	}
	
//	public override void deinit ()
//	{
//		isViewed = false;
//		
//		gameObject.SetActive (false);
//		foreach (GameObject p in plants) {
//			p.SetActive (false);
//		}
//	}

	void addBonuses ()
	{
		List<BonusePlace> arr = new List<BonusePlace> (places);

		float chance = _settings.bonuseChance;
		for (int i = 0; i < places.Length; i++) {
			float f = Random.value;
			if (f < chance) {
				int r = Random.Range (0, arr.Count);
				Vector2 v = arr [r].getPoint ();
				arr.RemoveAt (r);
				if(f < _settings.fruitChance){
					GameObject go = Global.instance.pool.get(_settings.fruits[Random.Range(0, _settings.fruits.Length)], transform.position);
					go.transform.position = v;
					go.transform.parent = transform.parent;
					Fruit s = go.GetComponent<Fruit> ();
					s.init ();
				}else{
					GameObject go = Global.instance.pool.get (getPattern (), transform.position);
					go.transform.position = v;
					go.transform.parent = transform.parent;
					BonusPattern s = go.GetComponent<BonusPattern> ();
					s.init (_settings);
				}

				chance *= 0.5f;
			}
		}
	}
	
	void addPlants ()
	{
		foreach (GameObject p in plants) {
			if (UnityEngine.Random.value < _settings.plantsChance) {
				p.SetActive (true);
			}
		}
	}

	GameObject getPattern ()
	{
		return bonusPatterns [Random.Range (0, bonusPatterns.Length)];
	}
}
