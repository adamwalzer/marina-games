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
	float specialTime = 0f;

	float timeSpend = 0;
	
	void Update ()
	{
		if (!isWorking)
			return;
		timeSpend += Time.deltaTime;
		checkTerrain ();
	}
	
	public void init (LevelSettings settings)
	{
		_settings = settings;
		x = startX;
		timeSpend = 0;
		isWorking = true;
		firstTerrain = true;
		createSpecials();
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
			if(specials.Count > 0 && timeSpend >= specialTime){
				tP.init (_settings, getSpecial());
				specialTime += Random.Range(0.8f, 1f) * specalPeriod;
			}else{
				tP.init (_settings, Bonus.BonusType.None);
			}

		}else{
			tP.init (_settings, Bonus.BonusType.None);
		}
	}



	void placeTerrain (GameObject go, float halfWidth)
	{
		go.transform.localPosition = new Vector3 (x + halfWidth, 0f, 0f);		
		x += 2 * halfWidth;
	}

	Bonus.BonusType getSpecial(){

		Bonus.BonusType result = specials[Random.Range(0, specials.Count)];
		specials.Remove(result);
		return result;
	}

	void createSpecials(){
		int i = 0;
		specials = new List<Bonus.BonusType>();
		for(i = 0; i < _settings.orbMax; i++){
			specials.Add(Bonus.BonusType.Orb);
		}
		for(i = 0; i < _settings.orangeMax; i++){
			specials.Add(Bonus.BonusType.SpiralOrange);
		}
		for(i = 0; i < _settings.violetMax; i++){
			specials.Add(Bonus.BonusType.SpiralViolet);
		}
		specalPeriod = (float)States.State<StateGame>().levelDuration / (specials.Count + 1);// * hero.gameObject.GetComponent<Robot>().speed;
		Debug.Log("count= " + specials.Count + " period+ " + specalPeriod.ToString());
		specialTime = Random.Range(0.8f, 1f) * specalPeriod;
	}
}
