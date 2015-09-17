using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour 
{
	public float duration = 3;

	public Vector2 intervalRange = new Vector2(0.1f, 0.8f);
//	public Vector2 sizeRange = new Vector2(0.2f, 0.6f);
	public Vector2 showRange = new Vector2(5.0f, 10.0f);

	public Camera cam;
	public GameObject prefab;
	public float maxWidth = 2.0f;

	public AudioClip lightingSound;

	float timer;
	float showTimer;
	float currentInterval;
	float currentShowInterval;
	float maxPosX = 1.0f;
	float maxPosY = 1.0f;
	bool isPlaying = false;
	Vector2 currentTarget;

	
	void Awake ()
	{
		if (cam == null) {
			cam = Camera.main;
		}
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float dropWidth = 2.0f;
		maxPosX = targetWidth.x - dropWidth;
	}

	void OnDisable(){
		if(isPlaying)
		animate(false);
	}
	
	void Start ()
	{
		currentInterval = Random.Range (intervalRange.x, intervalRange.y);
		transform.position = new Vector2(Random.Range (-maxPosX, maxPosX), transform.position.y + Random.Range (-maxPosY, maxPosY));
		currentShowInterval = Random.Range (showRange.x, showRange.y);
	}
	
	void Update ()
	{
		showTimer += Time.deltaTime;
		if(showTimer >= currentShowInterval){
			showTimer = 0;
			currentShowInterval = Random.Range (showRange.x, showRange.y);
			isPlaying = !isPlaying;
			animate(isPlaying);
		}
		if(!isPlaying) return;
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
		go.transform.position = new Vector3 (Random.Range (-maxWidth, maxWidth) + transform.position.x, 9 , 0);
		if(Random.value <= 0.5f){
//			float size = Random.Range (sizeRange.x, sizeRange.y);
			go.transform.localScale *= 0.5f;
		}
	}

	void animate(bool state){
		transform.position = new Vector2(Random.Range (-maxPosX, maxPosX), transform.position.y + Random.Range (-maxPosY, maxPosY));
		GetComponent<Animator>().SetBool("Flash", state);
		if(state){
			Global.instance.soundManager.playSoundOnChannel(SoundManager.LIGHTING_CHANNEL, lightingSound, true);
		}else{
			Global.instance.soundManager.stopSoundOnChannel(SoundManager.LIGHTING_CHANNEL);
		}
		States.State<StateGame>().onLigting(state);
	}
}
