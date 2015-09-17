using UnityEngine;
using System.Collections;

public class Flyer : MonoBehaviour 
{
	public Vector2 spawnPos;
	
	public AudioClip[] plantSound;
	public AudioClip damageSound;

	public Vector2 respawnPos = new Vector2(-3f, 0f);

	public GameObject dustEffect;

	public float speed = 1f;
	public float maxVelocityY = 10f;

	public int jumpEnergy = 1;
	
	Transform cachedTrfm;
	Rigidbody2D rig;
	bool isMoving = false;
	int maxX = 100;
	StateGame stateG;
	LevelSettings settings;
	float currentSpeed;
	Animator anima;
	
	float speedyTime = 0f;
	float slowlyTime = 0f;
	
	void Awake ()
	{
		cachedTrfm = transform;
		rig = GetComponent<Rigidbody2D> ();
		spawnPos = cachedTrfm.position;
		anima = GetComponent<Animator>();
	}
		
	void FixedUpdate ()
	{
		if (!isMoving)
			return;
		if(slowlyTime > 0){
			slowlyTime -= Time.fixedDeltaTime;
			currentSpeed = speed / 2;
		}else if(speedyTime > 0){
			speedyTime -= Time.fixedDeltaTime;
			currentSpeed = speed * 2;
		}else{
			currentSpeed = speed;
		}
			Vector2 v = rig.velocity;
			v.x = currentSpeed;
			if(v.y > maxVelocityY){
				v.y = maxVelocityY;
			}
			rig.velocity = v;
	}

	public void play (LevelSettings settings_)
	{
		settings = settings_;
		maxX = settings.levelLenght;
		speed = settings.speedMode;
		currentSpeed = speed;
		cachedTrfm.position = spawnPos;
		stateG = States.State<StateGame>();
		anima.SetTrigger("Restart");
		isMoving = true;
		rig.isKinematic = false;
		speedyTime = 0f;
		slowlyTime = 0f;
	}
	
	public void stop ()
	{
		isMoving = false;
		rig.velocity = Vector2.zero;
		rig.isKinematic = true;
	}
	
	public void push (Vector2 force, ForceMode2D mode)
	{
		Vector2 v = rig.velocity;
		v.y = 0;
		rig.velocity = v;
		rig.AddForce (force, mode);
	}
	
	public void slowDown(Vector2 pos){
		GameObject go = Global.instance.pool.get(dustEffect, pos);
		Global.instance.soundManager.playSoundOnChannel(SoundManager.PLANT_RUSTLE, plantSound[Random.Range(0, plantSound.Length)], false);
		go.GetComponent<PoolableParticleSystem>().activate();
		StopAllCoroutines();
		slowlyTime = 1f;
	}

	public void speedUp(){
		StopAllCoroutines();
		speedyTime = 5f;
	}

	public void onDamage(){
		anima.SetTrigger("Damaged");
		Global.instance.soundManager.playSound(damageSound);
	}

	public void onKilled(){
		anima.SetTrigger("Killed");
		Global.instance.soundManager.playSound(damageSound);
	}
}
