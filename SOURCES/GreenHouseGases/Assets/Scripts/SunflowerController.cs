using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SunflowerController : MonoBehaviour
{
	public Camera cam;
	public ParticleEmitter oxygenFX;
	public ParticleEmitter oxygenFX2;
	public float width;

	public List<AudioClip> oxygenSounds;

	bool canControl = true;
	private float maxWdth;
	Animator anima;
	
	void Start ()
	{
		if (cam == null) {
			cam = Camera.main;
		}
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
//		float hatWidth = renderer.bounds.extents.x;
		maxWdth = targetWidth.x - width;
		anima = GetComponent<Animator>();
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
		GetComponent<Rigidbody2D>().MovePosition (targetPos);
	}

	public void toggleControl (bool toggle)
	{
//		canControl = toggle;
	}

	public void onCarbon (int index)
	{
		anima.SetTrigger("Catch");
		if(oxygenFX.emit){
			StartCoroutine (showSplashEffect (oxygenFX2, 0.2f *(1 + index), index));
		}else{
			StartCoroutine (showSplashEffect (oxygenFX, 0.2f * (1 + index), index));
		}	
		Global.instance.soundManager.playSound(oxygenSounds[index]);
	}

	IEnumerator showSplashEffect (ParticleEmitter effect, float duration, AudioClip clip)
	{
		effect.emit = true;
		Global.instance.soundManager.playSound(clip);
		yield return new WaitForSeconds (duration);
		effect.emit = false;
	}

	IEnumerator showSplashEffect (ParticleEmitter effect, float duration, int index)
	{
		effect.emit = true;
		effect.maxSize = 0.3f + 0.05f * index;
		yield return new WaitForSeconds (duration);
		effect.emit = false;
	}
}
