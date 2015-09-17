using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
	public enum FallingItems
	{
		Water,
		Banana,
		Rainbow,
		Salt,
		Snowflake,
		Grasshopper
	}

	public Camera cam;
	public GameObject dropPrefab;
	public GameObject colorDropPrefab;
	public GameObject vareigatedDrop;
	public List<FallingPrefs> dropPrefabs;
	public Vector2 spawnPeriod;
	public Animator sky;
	public Animator tornado;
	public AudioClip tornadoSound;
	private float maxWdth;
	bool playing = false;
	bool boosted = false;
	int sumWeight;
	Vector2 currentSpawnPeriod;
	int colorVariety;

	void Awake ()
	{
		if (cam == null) {
			cam = Camera.main;
		}
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float dropWidth = 0.7f;//dropPrefab.GetComponentInChildren<Renderer>().bounds.extents.x;
		maxWdth = targetWidth.x - dropWidth;
	}

	public void startSpawn ()
	{
		sumWeight = 0;
		foreach (FallingPrefs fP in dropPrefabs) {
			sumWeight += fP.weight;
		}
		currentSpawnPeriod = spawnPeriod;
		playing = true;
		StartCoroutine (Spawn ());
	}

	public void stopSpawn ()
	{
		playing = false;
	}

	public void pauseSpawn(bool state){
		playing = !state;
		if(playing) StartCoroutine (Spawn ());
	}

	public void startDownpour ()
	{
		if (!boosted) {
			FallingPrefs fP = dropPrefabs [0];
			fP.prefab = vareigatedDrop;
			colorVariety = fP.weight;
			fP.weight = 100;
			dropPrefabs[0] = fP;
			boostRain(true);
			sky.SetTrigger ("ChangeColor");
		}
	}

	public void stopDownpour ()
	{
		FallingPrefs fP = dropPrefabs [0];
		fP.prefab = dropPrefab;
		fP.weight = colorVariety;
		dropPrefabs[0] = fP;
		sky.SetTrigger ("ConstColor");
		boostRain(false);
	}

	public void startTornado(){
		tornado.SetBool("Move", true);
		sky.SetTrigger("Tornado");
		boostRain(true);
		Global.instance.soundManager.playSoundOnChannel(SoundManager.TORNADO_CHANNEL, tornadoSound, true);
	}

	public void stopTornado(){
		sky.SetTrigger ("ConstColor");
		tornado.SetBool("Move", false);
		boostRain(false);
		Global.instance.soundManager.stopSoundOnChannel(SoundManager.TORNADO_CHANNEL);
	}

	public void onSnowflake(){
		sky.SetTrigger ("Snowflake");
	}

	void boostRain(bool state){
		FallingPrefs fP = dropPrefabs [0];
		if(state){
			boosted = true;
			currentSpawnPeriod = new Vector2 (0.1f, 0.3f);
			fP.weight *= 100;
			dropPrefabs [0] = fP;
		}else{
			currentSpawnPeriod = spawnPeriod;
			fP.weight /= 100;
			dropPrefabs [0] = fP;
			boosted = false;
		}
	}

	IEnumerator Spawn ()
	{
		while (playing) {
			Vector3 spawnPos = new Vector3 (UnityEngine.Random.Range (-maxWdth, maxWdth), transform.position.y, 0.0f);
			Quaternion spawnRot = Quaternion.identity;
			Instantiate (getRandomPrefab (), spawnPos, spawnRot);
			yield return new WaitForSeconds (UnityEngine.Random.Range (currentSpawnPeriod.x, currentSpawnPeriod.y));
		}
	}

	GameObject getRandomPrefab ()
	{
		int r = UnityEngine.Random.Range (0, sumWeight);
		int sum = 0;
		for (int i = 0; i < dropPrefabs.Count; i++) {
			sum += dropPrefabs [i].weight;
			if (r < sum) {
				return dropPrefabs [i].prefab;
			}
		}
		return dropPrefab;
	}
}

[Serializable]
public struct FallingPrefs
{
	public GameObject prefab;
	public int weight;
}
