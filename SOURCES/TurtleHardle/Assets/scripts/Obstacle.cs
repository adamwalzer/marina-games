using UnityEngine;

//using System;
using System.Collections.Generic;

public class Obstacle : IClearable
{
	public BonusePlace[] places;
	public GameObject[] plants;
	public BonusePlace urchinPlace;
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
			deinit ();			
		}
	}
	
	public void init (LevelSettings settings_)
	{
		_settings = settings_;
		_camera = Camera.main.transform;
		isViewed = true;
		addPlants ();
		if (transform.position.x > 5) {
			addBonuses ();
		}		
	}
	
	public override void deinit ()
	{
		isViewed = false;
		Destroy (gameObject);
	}

	void addBonuses ()
	{
		List<BonusePlace> arr = new List<BonusePlace> (places);
		StateGame gState = States.State<StateGame>();

		float chance = _settings.rewardChance;
		for (int i = 0; i < places.Length; i++) {
			if (Random.value < chance) {
				int r = Random.Range (0, arr.Count);
				Vector2 v = arr [r].getPoint ();
				arr.RemoveAt (r);
				Reward.RewardType type = gState.getBonusType();
				switch(type){
				case Reward.RewardType.Life :
					addReward(Instantiate(_settings.lifeBonus) as GameObject, urchinPlace.getPoint());
					break;
				case Reward.RewardType.Speed :
					addReward(Instantiate(_settings.speedBonus) as GameObject, v);
					break;
				case Reward.RewardType.Points :
					addReward(Global.instance.pool.get (Global.getRandomPrefab (_settings.fallingItems), transform.position), v);
					break;
				}
			}
		}
	}

	void addReward(GameObject go, Vector2 pos){
		go.transform.position = pos;
		go.transform.parent = transform.parent;
		Reward s = go.GetComponent<Reward> ();
		s.init ();
	}
	
	void addPlants ()
	{
		foreach (GameObject p in plants) {
			if (UnityEngine.Random.value < _settings.plantsChance) {
				p.SetActive (true);
			}
		}
	}
}
