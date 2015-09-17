using UnityEngine;

//using System;
using System.Collections.Generic;

public class Obstacle : MonoBehaviour
{
//	public GameObject[] bonusPatterns;
//	public GameObject orangeSpiralPrefab;
	public GameObject violetSpiralPrefab;
	public GameObject orbPrefab;
	public BonusePlace[] places;
	public GameObject[] plants;
	public GameObject[] rocks;
	public GameObject[] fires;
	public BonusePlace[] specialPlaces;
	public float width;
	LevelSettings _settings;
	Transform _camera;
	float _invisDistance = 30f;
	bool isViewed = false;
	
	void Update ()
	{
		if (!isViewed)
			return;
		if (transform.position.x < _camera.position.x - _invisDistance) {
			deinit ();
			
		}
	}
	
	public void init (LevelSettings settings_, Bonus.BonusType type)
	{
		_settings = settings_;
		_camera = Camera.main.transform;
		gameObject.SetActive (true);
		isViewed = true;
		addPlants ();
		if (transform.position.x > 5) {
			addBonuses ();
			if (rocks.Length > 0) {
				addRock ();
			}
			if(fires.Length > 0){
				addFire();
			}
		}	
		if(type != Bonus.BonusType.None){
			addSpecial(type);
		}	
	}
	
	public void deinit ()
	{
		isViewed = false;
		
		gameObject.SetActive (false);
		foreach (GameObject p in plants) {
			p.SetActive (false);
		}
		foreach (GameObject r in rocks) {
			r.SetActive (false);
		}
	}

	void addBonuses ()
	{
		List<BonusePlace> arr = new List<BonusePlace> (places);

		float chance = _settings.bonuseChance;
		for (int i = 0; i < places.Length; i++) {
			if (Random.value <= chance) {
				int r = Random.Range (0, arr.Count);
				Vector2 v = arr [r].getPoint ();
				arr.RemoveAt (r);
				GameObject go = Global.instance.pool.get (getPattern (), transform.position);
				go.transform.position = v;
				go.transform.parent = transform.parent;
				BonusPattern s = go.GetComponent<BonusPattern> ();
				s.init (_settings);
//				chance *= 0.5f;
			}
		}
	}

	void addRock ()
	{
		foreach(GameObject r in rocks){
			if (Random.value <= _settings.rockChance) {
				r.SetActive (true);
			}
		}
	}

	void addFire ()
	{
		foreach(GameObject f in fires){
			if (Random.value <= _settings.fireCance) {
				f.SetActive (true);
			}
		}
	}
	
	void addPlants ()
	{
		foreach (GameObject p in plants) {
			if (UnityEngine.Random.value <= _settings.plantsChance) {
				p.SetActive (true);
			}
		}
	}

	void addSpecial(Bonus.BonusType type){
		GameObject go = null;
		switch(type){
		case Bonus.BonusType.Orb :
			go = Global.instance.pool.get(orbPrefab, transform.position);
			break;
		case Bonus.BonusType.SpiralOrange :
			go = Global.instance.pool.get(violetSpiralPrefab, transform.position);
			break;
		case Bonus.BonusType.SpiralViolet :
			go = Global.instance.pool.get(violetSpiralPrefab, transform.position);
			break;
		}
		Bonus b = go.GetComponent<Bonus>();
		b.transform.parent = transform.parent;
		b.transform.position = specialPlaces[Random.Range(0, specialPlaces.Length)].getPoint();
		b.init();

	}

	GameObject getPattern ()
	{
		return _settings.bonuses.list[Random.Range (0, _settings.bonuses.list.Length)];
	}
}
