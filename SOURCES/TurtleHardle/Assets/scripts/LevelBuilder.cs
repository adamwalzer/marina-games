using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelBuilder : MonoBehaviour
{
	public Transform world;
	public Transform flyer;
	public float outrunning = 10f;
	public GameObject sharkPrefab;
	public GameObject baloonPrefab;
	LevelSettings _settings;
	float x;
	Vector2 y = new Vector2 (-0.7f, 1.1f);
	float initX = 20f;
	float startX = 5f;
	bool isWorking = false;
	
	void Update ()
	{
		if (!isWorking)
			return;
		checkTerrain ();
	}
	
	public void init (LevelSettings settings)
	{
		_settings = settings;
		x = 5f;
		isWorking = true;
	}
	
	public void deinit ()
	{
		isWorking = false;
		x = startX;
	}
	
	void createTerrain ()
	{
		GameObject go = Instantiate(_settings.terrains.list[Random.Range(0, _settings.terrains.list.Length)]) as GameObject;
		Obstacle tP = go.GetComponent<Obstacle> ();
		go.transform.parent = world;
		placeTerrain (go, tP.width / 2);

		tP.init (_settings);

		if(_settings.hasShark &&  Random.value < _settings.sharkChance){
			createShark(sharkPrefab, false);
		}
	}

	void createShark (GameObject prefab, bool randomArea)
	{
		GameObject go = Global.instance.pool.get (prefab, transform.position);
		go.transform.parent = world;
		Shark tP = go.GetComponent<Shark> ();
		tP.init ();
			
		if(randomArea){
			go.transform.localPosition = new Vector3 (x + tP.width/2, Random.Range (2*y.x, 2*y.y), 0f);	
			x += tP.width;
		}else{
			go.transform.position = new Vector3 (x + Random.Range(-tP.width, tP.width), Random.Range (3*y.x, 3*y.y), 0f);	
		}
	}

	void checkTerrain ()
	{
		if (x < initX) {
			while (x < initX) {
				createTerrain ();
			}
		} 
		else {
			if (x - flyer.position.x < outrunning) {
//				float f = Random.value;
//				if(_settings.hasBalloons && f < _settings.balloonChance){
//					createBalloon(baloonPrefab, true);
//				}else{
					createTerrain ();
				}
//			}
		}
	}

	void placeTerrain (GameObject go, float halfWidth)
	{
		go.transform.localPosition = new Vector3 (x + halfWidth, Random.Range (y.x, y.y), 0f);		
		x += 2 * halfWidth + Random.Range(_settings.emptySpace.x, _settings.emptySpace.y);
	}
}
