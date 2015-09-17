using UnityEngine;
using System;
using System.Collections;

public class BucketController : MonoBehaviour
{
	public Camera cam;
	public AudioSource audioSource;
	public ParticleEmitter waterBoolbFX;
	public ParticleEmitter waterSplashFX;
	public ParticleEmitter saltSplashFX;
	public ParticleEmitter greenSplashFX;
	public ParticleEmitter rainbowSplashFX;

	public AudioClip waterDropSound;
	public AudioClip bananaSplashSound;
	public AudioClip saltSplashSound;
	public AudioClip crazyDropSound;
	public AudioClip snowflakeSound;
	public AudioClip grasshopperSplashSound;
	public AudioClip waterpourSound;

	bool canControl = false;
	private float maxWdth;
	ParticleEmitter[] crazyEmmiters;
	
	void Start ()
	{
		if (cam == null) {
			cam = Camera.main;
		}
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float hatWidth = GetComponent<Renderer>().bounds.extents.x;
		maxWdth = targetWidth.x - hatWidth;
		crazyEmmiters = rainbowSplashFX.GetComponentsInChildren<ParticleEmitter>();
	}
	
	// Update is called once per phusics timestep
	void FixedUpdate ()
	{
		if (!canControl)
			return;
		Vector3 rawPos = cam.ScreenToWorldPoint (Input.mousePosition);
		Vector3 targetPos = new Vector3 (rawPos.x, 0.0f, 0.0f);
		float targetWidth = Mathf.Clamp (targetPos.x, -maxWdth, maxWdth);
		targetPos = new Vector3 (targetWidth, targetPos.y, targetPos.z);
//		GetComponent<Rigidbody2D>().MovePosition (targetPos);
		transform.position = targetPos;
	}

	public void toggleControl (bool toggle)
	{
		canControl = toggle;
	}

	public void onBanana ()
	{
		StartCoroutine (showSplashEffect (waterSplashFX, 0.6f, bananaSplashSound));
	}

	public void onWater ()
	{
		StartCoroutine (showSplashEffect (waterBoolbFX, 0.3f, waterDropSound));
		Global.instance.soundManager.playSound(waterDropSound);
	}

	public void onSalt ()
	{
		StartCoroutine (showSplashEffect (saltSplashFX, 0.5f, saltSplashSound));
	}

	public void onGrasshopper ()
	{
		StartCoroutine (showPourEffect ());
	}

	public void onCrazyDrop(){

		StartCoroutine(showCrazySplashEffect());
	}

	public void onSnowflake(){
		Global.instance.soundManager.playSound(snowflakeSound);
	}

	public void startCrazyDrop(){
		foreach(ParticleEmitter pE in crazyEmmiters){
			pE.emit = true;
		}
	}

	public void stopCrazyDrop(){
		foreach(ParticleEmitter pE in crazyEmmiters){
			pE.emit = false;
		}
	}

	IEnumerator showSplashEffect (ParticleEmitter effect, float duration, AudioClip clip)
	{
		effect.emit = true;
		Global.instance.soundManager.playSound(clip);
		yield return new WaitForSeconds (duration);
		effect.emit = false;
	}

	IEnumerator showPourEffect(){
		Global.instance.soundManager.playSound(grasshopperSplashSound);
		greenSplashFX.emit = true;
		canControl = false;
		var v = transform.localEulerAngles;
		v.z = -30;
		transform.localEulerAngles = v;
		yield return new WaitForSeconds(0.3f);
		audioSource.clip = waterpourSound;
		audioSource.Play();
		yield return new WaitForSeconds (1.5f);
		audioSource.Stop();
		greenSplashFX.emit = false;
		v.z = 0;
		transform.localEulerAngles = v;
		canControl = true;
	}

	IEnumerator showCrazySplashEffect(){
		foreach(ParticleEmitter pE in crazyEmmiters){
			pE.emit = true;
		}
		Global.instance.soundManager.playSound(crazyDropSound);
		yield return new WaitForSeconds(1.0f);
		foreach(ParticleEmitter pE in crazyEmmiters){
			pE.emit = false;
		}

	}
}
