using UnityEngine;
using System.Collections;

public class GrasshopperSpawner : MonoBehaviour 
{
	public float duration = 3;

	public float delay = 2;
	
	public Vector2 intervalRange = new Vector2(0.1f, 0.8f);
	public Vector2 showRange = new Vector2(5.0f, 10.0f);
	public Vector2 forceRange = new Vector2(1.0f, 2.0f);
	public Vector2 forceDir = new Vector2(0.5f, 0.5f);
	
	public Camera cam;
	public GameObject prefab;

	float timer;
	float currentInterval;
	
	void Awake ()
	{
		if (cam == null) {
			cam = Camera.main;
		}
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
	}
	
	void Start ()
	{
		currentInterval = Random.Range (intervalRange.x, intervalRange.y) + delay;
	}
	
	void Update ()
	{
		timer += Time.deltaTime;
		if (timer > currentInterval) {
			timer = 0;
			currentInterval = Random.Range (intervalRange.x, intervalRange.y);
			create ();
		}
	}
	
	void create ()
	{
		GameObject go = Instantiate (prefab) as GameObject;
		go.transform.position = transform.position;
		Vector2 force = forceDir * Random.Range(forceRange.x, forceRange.y);
		go.GetComponent<GrasshopperContacter>().jumpForce = force;
		go.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
	}
}
