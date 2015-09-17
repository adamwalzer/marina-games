using UnityEngine;
using System.Collections;

public class Barrel : MonoBehaviour 
{
	public Animator anima;
	public float startX;
	float speed = 2f;
	bool isMoving = false;
	float currentSpeed;

	void Update(){
		if(!isMoving) return;

		Vector2 pos = transform.position;
		pos.x -= Time.deltaTime * currentSpeed;
		transform.position = pos;
	}

	public void init(){
		Vector2 pos = transform.localPosition;
		pos.x = startX;
		currentSpeed = speed * Random.Range(0.9f, 1.1f);
		transform.localPosition = pos;
		isMoving = true;
		gameObject.SetActive(true);
	}

	public void deinit(){
		isMoving = false;
		gameObject.SetActive(false);
	}
}
