using UnityEngine;
using System;
using System.Collections;

public class Spawner : MonoBehaviour 
{
	public Camera cam;
	public float fallMod;
	public GameObject starPrefab;

	Vector2 spawnPeriod;
	float maxWdth;
	int sumWeight;

	bool playing = false;
	bool waitingForFinish = false;

	LevelSettings _settings;
	
	void Awake ()
	{
		if (cam == null) {
			cam = Camera.main;
		}
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float dropWidth = 1f;
		maxWdth = targetWidth.x - dropWidth;
	}

	void Update(){
		if(waitingForFinish){
			if(GameObject.FindObjectOfType<Reward>() == null && GameObject.FindObjectOfType<Obstacle>() == null){
				waitingForFinish = false;
				StartCoroutine(waitAndStop());

			}
		}
	}

	public void startSpawn(LevelSettings settings_){
		_settings = settings_;
		spawnPeriod = _settings.spawnPeriod;
		playing = true;
		StartCoroutine (spawn ());
		StartCoroutine(spawnBonus());
	}

	public void stopSpawn ()
	{
		playing = false;
		waitingForFinish = true;
		StopAllCoroutines();
	}

	public void pauseSpawn(bool state){
		playing = !state;
		if(playing) StartCoroutine (spawn ());
	}

	public void boostSpawn(){
		if(playing) StartCoroutine (boost ());
	}

	IEnumerator spawn ()
	{
		while (playing) {
			Vector3 spawnPos = new Vector3 (UnityEngine.Random.Range (-maxWdth, maxWdth), transform.position.y, 0.0f);
			GameObject prefab = Global.getRandomPrefab(_settings.fallingItems);
			GameObject go = Global.instance.pool.get(prefab, spawnPos);
			go.name = prefab.name;
			//т.к. один из двух классов: Reward и Obstacle, обращаюс просто к объекту, без инита
			go.SetActive(true);
			yield return new WaitForSeconds (UnityEngine.Random.Range (spawnPeriod.x, spawnPeriod.y));
		}
	}

	IEnumerator spawnBonus(){
		int i = UnityEngine.Random.Range(1, 4);
		Vector2 period = new Vector2(5, States.State<StateGame>().levelTime / i - 3);
		while(i > 0){
			i --;
			float t = UnityEngine.Random.Range(period.x, period.y);
			yield return new WaitForSeconds(t);
			Vector3 spawnPos = new Vector3 (UnityEngine.Random.Range (-maxWdth, maxWdth), transform.position.y, 0.0f);
			GameObject go = Global.instance.pool.get(starPrefab, spawnPos);
			go.GetComponent<Rigidbody2D>().gravityScale *= fallMod;
		}
	}

	IEnumerator boost(){
		spawnPeriod *= 0.5f;
		yield return new WaitForSeconds(7);
		spawnPeriod = _settings.spawnPeriod;
	}

	IEnumerator waitAndStop(){
		yield return new WaitForSeconds(1);
		States.State<StateGame>().endLevel();
	}
}
