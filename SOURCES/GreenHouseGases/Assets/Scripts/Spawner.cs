using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
	public Camera cam;
	public GameObject dropPrefab;
	public Vector2 spawnPeriod;
	private float maxWdth;
	bool playing = false;
	Vector2 currentSpawnPeriod;
	int colorVariety;

	void Start ()
	{
		if (cam == null) {
			cam = Camera.main;
		}
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
//		Debug.Log ("targetWidth : " + targetWidth);
		float dropWidth = dropPrefab.GetComponent<Renderer>().bounds.extents.x;
		maxWdth = targetWidth.x - dropWidth;
	}

	public void startSpawn ()
	{
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

	IEnumerator Spawn ()
	{
		while (playing) {
			Vector3 spawnPos = new Vector3 (UnityEngine.Random.Range (-maxWdth, maxWdth), transform.position.y, 0.0f);
			Quaternion spawnRot = Quaternion.identity;
			Instantiate (dropPrefab, spawnPos, spawnRot);
//			Debug.Log ("Spwan: " + dropPrefab.name + " pos: " + spawnPos + " rot: " + spawnRot);
			yield return new WaitForSeconds (UnityEngine.Random.Range (currentSpawnPeriod.x, currentSpawnPeriod.y));
		}
	}
}
