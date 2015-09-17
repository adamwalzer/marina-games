using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Robot : MonoBehaviour
{
	public float speed;
	public float maxXVelocity;
	public float maxYVelocity;
	public AudioClip flipSound;
	public AudioClip waterSound;
	public AudioClip rockSound;
	public Rigidbody2D rig2D;
	public Animator anima;
	public float maxZRotation = 35f;
	public float immunityTime = 7f;
	public GameObject immunityEffect;
	public ParticleSystem waterEffect;
	public float speedUpTime = 4f;
	public float speedUpVelocity;
	public GameObject speedUpEffect;
	public GameObject fireEffect;

	bool isMoving = false;
	Vector2	startPos;
	Transform trfm;
	Vector3 startRot;
	List<Collider2D> groundColl;
public	bool isInWater = false;
	LevelSettings settings;
	bool isFlipping = false;
	bool canFlip = false;
	bool immunity = false;
	float currentVelocity;
	bool isOnRock = false;

	void Start ()
	{
		startPos = new Vector2 (transform.position.x, transform.position.y);
		startRot = transform.rotation.eulerAngles;
		trfm = transform;
	}

	void FixedUpdate ()
	{
		if (!isMoving)
			return;
		if (trfm.rotation.eulerAngles.z > maxZRotation && trfm.rotation.eulerAngles.z < 180) {
			Vector3 q = startRot;
			q.z = maxZRotation;
			trfm.rotation = Quaternion.Euler (q);
		} else if (trfm.rotation.eulerAngles.z < 360f - maxZRotation && trfm.rotation.eulerAngles.z > 180) {
			Vector3 q = startRot;
			q.z = -maxZRotation;
			trfm.rotation = Quaternion.Euler (q);
		}
		rig2D.AddForce (speed * trfm.right, ForceMode2D.Impulse);
		if (rig2D.velocity.x > currentVelocity) {
			Vector2 v = rig2D.velocity;
			v.x = currentVelocity;
			rig2D.velocity = v;
		}
		if (rig2D.velocity.y > maxYVelocity) {
			Vector2 v = rig2D.velocity;
			v.y = maxYVelocity;
			rig2D.velocity = v;
		}
		if (trfm.position.y > 2 && rig2D.velocity.y > -2f && rig2D.velocity.y < 2f) {
			canFlip = true;
		} else {
			canFlip = false;
		}

	}

	void OnTriggerEnter2D (Collider2D c)
	{
		if (c.tag == "Ground") {
			if (groundColl.IndexOf (c) == -1) {
				groundColl.Add (c);
				rig2D.fixedAngle = false;
				jumpFinished ();
			}
		}

		if (!isInWater && !immunity) {
			if (c.tag == "Water") {
				inWater ();
			}
		}

		if(!isOnRock && c.tag == "Rock"){
			if (!immunity) {
				onRock ();
			}
		}

		if(!isOnRock && c.tag == "Fire"){
			if (!immunity) {
				onFire ();
				c.gameObject.SetActive(false);
			}
		}
	}

	void OnTriggerStay2D(Collider2D c){
		if (!isInWater && !immunity) {
			if (c.tag == "Water") {
				inWater ();
			}
		}
	}

	void OnTriggerExit2D (Collider2D c)
	{
		if (c.tag == "Ground") {
			if (groundColl.IndexOf (c) != -1) {
				groundColl.Remove (c);
			}
		}
		if (groundColl.Count == 0) {
			rig2D.fixedAngle = true;
			jumpStarted ();
		}
	}

	public void play (LevelSettings s)
	{
		settings = s;
		currentVelocity = maxXVelocity;
		groundColl = new List<Collider2D> ();
		isMoving = true;
		rig2D.isKinematic = false;
		isFlipping = false;
		isInWater = false;
		isOnRock = false;
		immunity = false;
		anima.SetTrigger ("Restart");
	}

	public void stop ()
	{
		StopAllCoroutines();
		isMoving = false;
		rig2D.velocity = Vector2.zero;
		rig2D.isKinematic = true;
		trfm.position = startPos;
		speedUpEffect.SetActive(false);
		immunityEffect.SetActive(false);
		fireEffect.SetActive(false);
	}

	public void flip ()
	{
		if (!isMoving)
			return;
		if (!isFlipping && canFlip && settings.number > 1) {
			rig2D.AddForce(new Vector2(1.5f, 0f), ForceMode2D.Impulse);
			anima.SetTrigger ("Flip");
			isFlipping = true;
			Global.instance.soundManager.playSound (flipSound);
			States.State<StateGame> ().onFlip ();
			currentVelocity = speedUpVelocity;
		}
	}

	void jumpStarted ()
	{
		anima.SetBool ("IsJumping", true);
	}

	void jumpFinished ()
	{
		anima.SetBool ("IsJumping", false);
	}

	public Rigidbody2D rigidBody {
		get {
			return rig2D;
		}
	}

	public void push (Vector2 force, ForceMode2D mode)
	{
		if (!isMoving || isInWater)
			return;
		if (groundColl.Count > 0) {
			rig2D.AddForce (force, mode);
		}
	}

	public void setImmunity ()
	{
		StartCoroutine(immunityCoroutine());
	}

	public void speedUp(){
		StartCoroutine(speedCoroutine());
	} 

	public void onRockEnded ()
	{
		isOnRock = false;
	}

	public void onFlipEnded ()
	{
		isFlipping = false;
		currentVelocity = maxXVelocity;
	}

	public void onWaterEnded ()
	{
		States.State<StateGame> ().restartAfterWater ();
	}

	void inWater ()
	{
		isInWater = true;
		Global.instance.soundManager.playSound (waterSound);
		transform.rotation = Quaternion.identity;
		anima.SetTrigger ("InWater");
		waterEffect.Play();
	}

	void onRock ()
	{
		isOnRock = true;
		anima.SetTrigger ("OnRock");
		rig2D.AddForce (new Vector2 (0f, 1f), ForceMode2D.Impulse);
		Global.instance.soundManager.playSound (rockSound);

		States.State<StateGame> ().onRock ();
	}

	void onFire(){
		anima.SetTrigger("OnFire");
		Global.instance.soundManager.playSound (rockSound);
		StartCoroutine(fireCoroutine());
		States.State<StateGame> ().onRock ();
	}
	

	IEnumerator immunityCoroutine(){
		immunity = true;
		immunityEffect.SetActive(true);
		yield return new WaitForSeconds(immunityTime);
		immunityEffect.SetActive(false);
		immunity = false;
	}

	IEnumerator speedCoroutine(){
		currentVelocity = speedUpVelocity;
		speedUpEffect.SetActive(true);
		yield return new WaitForSeconds(speedUpTime);
		speedUpEffect.SetActive(false);
		currentVelocity = maxXVelocity;
	}

	IEnumerator fireCoroutine(){
		fireEffect.SetActive(true);
		yield return new WaitForSeconds(speedUpTime);
		fireEffect.SetActive(false);
	}
}
