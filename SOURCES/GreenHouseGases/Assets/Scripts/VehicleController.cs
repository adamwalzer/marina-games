using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class VehicleController : MonoBehaviour
{
	public Vector2 spawnInterval = new Vector3 (2.0f, 5.0f);
	public GameObject vehiclePrefab;
	public Transform road;
	public float maxX;
	public Vector2 yRange;
	public GameObject buildingPrefab;
	public Vector2 buildingsCount;
	bool playing = false;
	float currentInterval;
	List<GameObject> buildings;

	public void startSpawn ()
	{
		playing = true;
		createBuildings();
		StartCoroutine (Spawn ());
	}

	public void stopSpawn ()
	{
		playing = false;
		if (buildings != null && buildings.Count > 0) {
			foreach (GameObject g in buildings) {
				Destroy (g);
			}
			buildings.Clear ();
		}
	}

	IEnumerator Spawn ()
	{
		while (playing) {
			GameObject go = Instantiate (vehiclePrefab) as GameObject;
			go.transform.parent = road;
			Vector3 pos = new Vector3 ();
			float r1 = UnityEngine.Random.value;
			float yMiddle = (yRange.x + yRange.y) / 2;
			float r2;
			if (r1 > 0.5f) {
				//to the right
				r2 = UnityEngine.Random.Range (yMiddle, yRange.y);
				pos = new Vector3 (-maxX, r2, r2);
			} else {
				//to the left
				r2 = UnityEngine.Random.Range (yRange.x, yMiddle);
				pos = new Vector3 (maxX, r2, r2);
			}
			go.transform.localPosition = pos;
			go.GetComponent<Vehicle> ().init ();
			yield return new WaitForSeconds (UnityEngine.Random.Range (spawnInterval.x, spawnInterval.y));
		}
	}

	void createBuildings ()
	{
		if (buildings == null) {
			buildings = new List<GameObject> ();
		}
		int count = (int)UnityEngine.Random.Range (buildingsCount.x, buildingsCount.y);
		for (int i = 0; i < count; i++) {
			float r1 = UnityEngine.Random.Range (-maxX, maxX);
			float r2 = UnityEngine.Random.Range (yRange.x, yRange.y);
			GameObject go = Instantiate (buildingPrefab) as GameObject;
			buildings.Add (go);
			go.transform.parent = road;
			go.transform.localPosition = new Vector3 (r1, r2, r2);
		}
	}
}
