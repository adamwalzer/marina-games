using UnityEngine;
using System.Collections.Generic;

public class BackgroundGrass : MonoBehaviour
{
	public GameObject plantPrefab;
	public float grassPosY = -3.85f;
	GameObject[] allPlants;

	public void init (int count)
	{
		float ex = GetComponent<SpriteRenderer> ().bounds.extents.x;
		allPlants = new GameObject[count];
		for (int i = 0; i < count; i++) {
			GameObject go = Instantiate (plantPrefab) as GameObject;
			Vector2 pos = new Vector2 (Random.Range (-ex, ex), grassPosY);
			pos.x += transform.position.x;
			go.transform.position = pos;
			allPlants [i] = go;
		}
	}

	public void deinit ()
	{
		if(allPlants == null) return;
		foreach (GameObject g in allPlants) {
			if(g != null)
			Destroy (g);
		}
		allPlants = null;
	}
}
