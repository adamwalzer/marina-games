using UnityEngine;
using System.Collections;

public class Tornado : MonoBehaviour
{
	public Vector2 spawnPos;
	public float speedMod = 0.95f;
	public Transform butterfly;
	public float maxDist;
	Vector2 speed;
	bool isMoving = false;
	Rigidbody2D rig;
	LevelSettings settings;
	Collider2D[] colliders;

	void Awake ()
	{
		rig = GetComponent<Rigidbody2D> ();
		colliders = GetComponents<Collider2D> ();
	}

	void FixedUpdate ()
	{
		if (!isMoving)
			return;

		if (butterfly.transform.position.x - transform.position.x > maxDist) {
			speed.x = settings.speedMode * 2;
		} else {
			speed.x = settings.speedMode * speedMod;
		}

		rig.velocity = speed;
	}

	void OnTriggerEnter2D (Collider2D c)
	{
		if (c.tag == "Player") {
			States.State<StateGame> ().inTornado ();
			StartCoroutine (wait ());
		}
	}

	public void play (LevelSettings settings_)
	{
		settings = settings_;
		speed.x = settings.speedMode * speedMod;
		transform.position = spawnPos;
		isMoving = true;
	}
	
	public void stop ()
	{
		isMoving = false;
		rig.velocity = Vector2.zero;
		StopAllCoroutines ();
	}

	IEnumerator wait ()
	{
		float f = speedMod;
		speedMod = 0;
		foreach (Collider2D c in colliders) {
			c.enabled = false;
		}
		yield return new WaitForSeconds (3);
		foreach (Collider2D c in colliders) {
			c.enabled = true;
		}
		speedMod = f;
	}
}
