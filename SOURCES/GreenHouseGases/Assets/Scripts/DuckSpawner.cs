using UnityEngine;
using System.Collections;

public class DuckSpawner : MonoBehaviour 
{


	public Camera cam;
	public GameObject duckPrefab;
	public float delay = 5.0f;

	//система с двумя спавнерами
	public float[] ySpawn;
//	public bool moveRight = false;
//	public Transform spawnerLeft;
	public float leftX;
	public float[] leftY;
//	public Transform spawnerRight;
	public float rightX;
	public float[] rightY;

	Vector2 spawnPeriod;
	bool playing = false;
	float windForce;
	float duckSpeed;
	
	public void startSpawn (Vector2 spawnPeriod_, float duckForce, float duckSpeed_)
	{
		spawnPeriod = spawnPeriod_;
		windForce = duckForce;
		duckSpeed = duckSpeed_;
		playing = true;
		StartCoroutine(Wait());
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
			float posX;
			float posY;
			if(Random.value < 0.5f){
				posX = leftX;
				posY = leftY[UnityEngine.Random.Range(0, leftY.Length)];
			}else{
				posX = rightX;
				posY = rightY[UnityEngine.Random.Range(0, rightY.Length)];
			}
			Vector3 spawnPos = new Vector3 (posX, posY, 0.0f);
			Quaternion spawnRot = Quaternion.identity;
			GameObject go = Instantiate (duckPrefab, spawnPos, spawnRot) as GameObject;
			go.GetComponent<Duck>().init(windForce, duckSpeed);
			yield return new WaitForSeconds (UnityEngine.Random.Range (spawnPeriod.x, spawnPeriod.y));
		}
	}

	IEnumerator Wait(){
		yield return new WaitForSeconds(delay);
		StartCoroutine (Spawn ());
	}
}
