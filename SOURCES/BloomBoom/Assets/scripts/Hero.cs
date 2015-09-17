using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hero : MonoBehaviour
{
	public Rigidbody2D rig2D;
	public Animator anima;
	public float speed = 300f;
	public float immunityTime = 1f;
	public GameObject immunityEffect;
	public float speedUpTime = 6f;
	public float doubleSpeed = 500f;
	public GameObject speedUpEffect;
	public AudioClip crasSound;
	bool isMoving = false;
	public Vector2	startPos;
	Transform trfm;
	LevelSettings settings;
	bool isDestroyed = false;
	GameObject view;
	bool secondState = false;
	float currentSpeed;
	bool isImmunity = false;

	void Awake ()
	{
		trfm = transform;
	}

	void Update ()
	{
		if (! isMoving)
			return;

		Vector2 v = rig2D.velocity;
		v.x = Time.deltaTime * currentSpeed;
		rig2D.velocity = v;
	}

	void OnTriggerEnter2D (Collider2D c)
	{
		if (isDestroyed)
			return;
		if (!secondState && c.tag == "Pollen" && settings.pollenHero != null) {
			secondState = true;
			setView (settings.pollenHero);
		}
	}

	public void OnCollisionEnter2D (Collision2D c)
	{
		if (isDestroyed)
			return;
		if (c.gameObject.tag == "Ground") {
			Barrel b = c.gameObject.GetComponent<Barrel> ();
			if (b != null) {
				onCrash (c.contacts [0].point);
				currentSpeed = 0;
			} else {
				if (!isImmunity) {
					lostPollen (c.contacts [0].point);
				}
			}

		} else if (c.gameObject.tag == "Floor") {
			currentSpeed = 0;
		}
	}

	public void OnCollisionExit2D (Collision2D c)
	{
		if (c.gameObject.tag == "Floor") {
			currentSpeed = speed;
		}
	}

	public void play (LevelSettings s)
	{
		trfm.position = startPos;
		settings = s;
		currentSpeed = speed;
		if (settings.pollenHero != null && States.State<StateGame> ().pollen > 0) {
			setView (s.pollenHero);
			secondState = true;
		} else {
			setView (s.hero);
			secondState = false;
		}

		isMoving = true;
		isImmunity = false;
		isDestroyed = false;
		rig2D.isKinematic = false;
		anima.SetTrigger ("Restart");
	}

	public void stop ()
	{
		StopAllCoroutines ();
		isMoving = false;
		rig2D.velocity = Vector2.zero;
		rig2D.isKinematic = true;
		speedUpEffect.SetActive (false);
		Destroy (view);
	}

	public Rigidbody2D rigidBody {
		get {
			return rig2D;
		}
	}

	public void push (Vector2 force, ForceMode2D mode)
	{
		if (!isMoving)
			return;
		rig2D.AddForce (force, mode);
		anima.SetTrigger ("Swing");
	}

	public void speedUp ()
	{
		StartCoroutine (speedCoroutine ());
	}

	public void onRockEnded ()
	{
		States.State<StateGame> ().lostLife ();
		currentSpeed = speed;
		isDestroyed = false;
	}

	//жизнь теряет по событию в анимации
	void onCrash (Vector3 position)
	{
		isDestroyed = true;
		anima.SetTrigger ("Crash");
		Global.instance.soundManager.playSound (crasSound);
		States.State<StateGame> ().onGroundTouch (position);
	}

	void lostPollen (Vector3 position)
	{
		StartCoroutine(afterLostImmunity());
		Global.instance.soundManager.playSound (crasSound);
		States.State<StateGame> ().onGroundTouch (position);
	}

	IEnumerator speedCoroutine ()
	{
		currentSpeed = doubleSpeed;
		speedUpEffect.SetActive (true);
		if (!secondState && settings.speedyHero != null) {
			secondState = true;
			setView (settings.speedyHero);
		}
		yield return new WaitForSeconds (speedUpTime);
		speedUpEffect.SetActive (false);
		currentSpeed = speed;
		if (secondState && settings.speedyHero != null) {
			setView (settings.hero);
			secondState = false;
		}
	}

	public void setView (GameObject prefab)
	{
		if (view != null) {
			Destroy (view);
		}
		view = Instantiate (prefab) as GameObject;
		view.transform.parent = transform;
		view.transform.localPosition = Vector3.zero;
		anima = view.GetComponent<Animator> ();
		view.GetComponent<HeroAnimationEvents> ().parent = this;
	}

	IEnumerator afterLostImmunity ()
	{
		isImmunity = true;
		yield return new WaitForSeconds (immunityTime);
		isImmunity = false;
	}
}
