using UnityEngine;
using System;
using System.Collections;

public class Cloud : MonoBehaviour
{
	public Camera cam;
	public GameObject prefab;
	public float maxWidth = 0.5f;
	public float speed = 0.01f;
	float timer;
	float currentInterval;
	float moveMax;
	float currentTarget;
	bool moveToRight = false;
	bool isPlaying = false;
	bool goAway = false;
	CloudSettings settings;

	void Awake ()
	{
		if (cam == null) {
			cam = Camera.main;
		}
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float dropWidth = GetComponent<Renderer>().bounds.extents.x / 2;
		moveMax = targetWidth.x - dropWidth;
	}

	void Start ()
	{
		currentInterval = UnityEngine.Random.Range (settings.intervalRange.x, settings.intervalRange.y);
		currentTarget = UnityEngine.Random.Range (-moveMax, moveMax);
		moveToRight = transform.position.x > currentTarget;
	}

	void Update ()
	{
		//облако выключено
		if(!isPlaying) return;
		if(goAway){
			// облако уходит с экрана
			if(transform.position.x < 10){
				float newXd = Mathf.MoveTowards (transform.position.x, 10, speed);
				transform.position = new Vector3 (newXd, transform.position.y, 0);
			}else{
				goAway = false;
				isPlaying = false;
			}
			return;
		}
		//облако продолжает движение и спавн
		timer += Time.deltaTime;
		if (timer > currentInterval) {
			timer = 0;
			currentInterval = UnityEngine.Random.Range (settings.intervalRange.x, settings.intervalRange.y);
			create ();
		}
		if (moveToRight) {
			if (currentTarget - transform.position.x < speed * 0.5f) {
				currentTarget = UnityEngine.Random.Range (-moveMax, moveMax);
				moveToRight = transform.position.x > currentTarget;
			}
		} else {
			if (transform.position.x - currentTarget < speed * 0.5f) {
				currentTarget = UnityEngine.Random.Range (-moveMax, moveMax);
				moveToRight = transform.position.x < currentTarget;
			}
		}

		float newX = Mathf.MoveTowards (transform.position.x, currentTarget, speed);
		transform.position = new Vector3 (newX, transform.position.y, 0);
	}

	public void startDrop(CloudSettings s, bool isContinue){
		settings = s;
		if(isContinue){
			isPlaying = true;
		}else{
			StartCoroutine(showAtOnce());
		}

	}

	public void stopDrop(){
		isPlaying = false;
	}

	void create ()
	{
		GameObject go = Instantiate (prefab) as GameObject;
		go.transform.position = new Vector3 (UnityEngine.Random.Range (-maxWidth, maxWidth) + transform.position.x, transform.position.y, 0);

		if(UnityEngine.Random.value <= 0.75f){
			go.transform.localScale *= 0.5f;
		}
	}

	IEnumerator showAtOnce(){
		transform.position = new Vector3((-maxWidth - 7), transform.position.y, 0);
		yield return new WaitForSeconds(UnityEngine.Random.Range(5, 10));
		isPlaying = true;
		yield return new WaitForSeconds(UnityEngine.Random.Range(5, 10));
		goAway = true;
	}
}
