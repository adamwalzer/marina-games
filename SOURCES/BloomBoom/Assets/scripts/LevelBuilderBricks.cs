using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelBuilderBricks : MonoBehaviour
{
	public Transform world;
	public Transform hero;
	public float outrunning = 10f;
	public GameObject startPlatformPrefab;
	LevelSettings _settings;
	float x;
	Vector2 y = new Vector2 (-0.7f, 1.1f);
	float startX = -9f;
	bool isWorking = false;
	bool firstTerrain = true;
	
	List<Bonus.BonusType> specials;
	float specalPeriod = 0f;
	float specialX = 0f;
	
	void Update ()
	{
		if (!isWorking)
			return;
		checkTerrain ();
	}
	
	public void init (LevelSettings settings)
	{
		_settings = settings;
		x = startX;
		isWorking = true;
		firstTerrain = true;
//		createSpecials();
	}
	
	public void deinit ()
	{
		isWorking = false;
		x = startX;
	}

	void checkTerrain ()
	{
		if (x - hero.position.x < outrunning) {
			createTerrain ();
		}
	}
	
	void createTerrain ()
	{
		GameObject go;
		if(firstTerrain){
			go = Global.instance.pool.get (startPlatformPrefab, transform.position);
			firstTerrain = false;
		}else{
			go = Global.instance.pool.get (_settings.terrains.list[Random.Range(0, _settings.terrains.list.Length)], transform.position);
		}

		Obstacle tP = go.GetComponent<Obstacle> ();
		go.transform.parent = world;
		placeTerrain (go, tP.width / 2);

		if(tP.specialPlaces.Length > 0){
//			if(specials.Count > 0 && Time.time >= specialX){
//				tP.init (_settings, getSpecial());
//				specialX += specalPeriod;
//			}else{
				tP.init (_settings, States.State<StateGame>().getBonusType());
//			}

		}else{
			tP.init (_settings, Bonus.BonusType.Flower);
		}
	}

	void placeTerrain (GameObject go, float halfWidth)
	{
		go.transform.localPosition = new Vector3 (x + halfWidth, 0f, 0f);		
		x += 2 * halfWidth;
	}

//	Bonus.BonusType getSpecial(){
//
//		Bonus.BonusType result = specials[Random.Range(0, specials.Count)];
//		specials.Remove(result);
//		Debug.Log("x = " + x + "type = " + result);
//		return result;
//	}
//
//	void createSpecials(){
//		specials = new List<Bonus.BonusType>();
//		for(int i = 0; i < _settings.ligtningMax; i++){
//			specials.Add(Bonus.BonusType.Lightning);
//		}
//		specalPeriod = (float) States.State<StateGame>().levelDuration / (specials.Count + 1);
//		specialX = specalPeriod;
//	}
}
