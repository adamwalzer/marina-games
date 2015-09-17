using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
	
public class Duck : MonoBehaviour
{
	public ParticleEmitter particles;
	public AudioClip[] duckSounds;
	float step;
	Vector3 _destination;
	bool _moveToLeft;
	bool isMoving = false;
	float _step;
	
	float maxDistance = 10;
	float force = 10;

	void OnTriggerEnter2D(Collider2D c){
		if(c.gameObject.tag == "Carbon"){
			float distance = Vector3.Distance(transform.position, c.transform.position);
			Vector3 dir = Vector3.Normalize(c.transform.position - transform.position);
			float cForce = force * distance / maxDistance;
			c.GetComponent<Rigidbody2D>().AddForce(dir * cForce, ForceMode2D.Impulse);
			c.GetComponent<Rigidbody2D>().AddTorque(0.3f);
		}
	}
		
	void Start ()
	{
		_step = step * (1.0f + UnityEngine.Random.value);
		GUIGame.onRemoveAllDucks += removeAllDucks;

	}

	void OnDestroy(){
		GUIGame.onRemoveAllDucks -= removeAllDucks;
	}
		
	void Update ()
	{
		if (!isMoving)
			return;
			
		if ((_moveToLeft && transform.localPosition.x <= _destination.x + _step) || (!_moveToLeft && transform.localPosition.x >= _destination.x - _step)) {
			//arrived
			isMoving = false;
			StopCoroutine(playVoice());
			Destroy (gameObject);
		} else {
			//move
			Vector3 newPos = Vector3.MoveTowards (transform.localPosition, _destination, _step);
			transform.localPosition = newPos;
		}
	}
		
	public void init (float windForce, float speed_)
	{
		force = windForce;
		step = speed_;
		_destination = transform.localPosition;
		_destination.x *= -1;
		_moveToLeft = _destination.x < 0;
		if (_moveToLeft) {
			transform.localScale = new Vector3 (-1, 1, 1);
			Vector3 particlesVel = particles.localVelocity;
			particlesVel.x *= -1;
			particles.localVelocity = particlesVel;
		}
		isMoving = true;
		StartCoroutine(playVoice());
	}

	void removeAllDucks(){
//		Global.instance.soundManager.stopSoundOnChannel(SoundManager.DUCK_CHANNEL);
		StopCoroutine(playVoice());
		Destroy(gameObject);
	}

	IEnumerator playVoice(){
		while(isMoving){
			Global.instance.soundManager.playSound(duckSounds[UnityEngine.Random.Range(0, duckSounds.Length)]);
			yield return new WaitForSeconds(2.0f + UnityEngine.Random.Range(1, 3));
		}
	}
}
