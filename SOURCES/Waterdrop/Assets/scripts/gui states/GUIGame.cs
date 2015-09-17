using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class GUIGame : MonoBehaviour
{
	public Spawner spawner;
	public Vector2 standardGravity = new Vector3(0.0f, -9.81f);
	public float levelDuration;
	float timeLeft;
	public Text timer;
	public BucketController bucket;
	public Text scoreText;
	public Cloud rainCloud;
	public Cloud snowCloud;
	public Lightning lightning;
	public GrasshopperSpawner grasshopperSpawnerLeft;
	public GrasshopperSpawner grasshopperSpawnerRight;
	public List<LevelSettings> levels;
	public SoundManager soundManager;
	public AudioClip backgroundMusic;
	public AudioClip levelEndSound;
	public GameObject[] levelHeaders;
	GameObject currentLvlHeader;
	bool playing = false;
	LevelSettings currentLevel;
	float tornadoMoment;

	void FixedUpdate ()
	{
		if (!playing)
			return;
		timeLeft -= Time.deltaTime;
		if (timeLeft < 0) {
			timeLeft = 0;
			StartCoroutine (stopSpawn ());
		}
		if(tornadoMoment < levelDuration){
			if(tornadoMoment > timeLeft){
				tornadoMoment = levelDuration;
				States.State<StateGame>().onTornado();
			}
		} 
		updateTimer ();
	}

	public void restart ()
	{
		int l = States.State<StateGame> ().currentLevel - 1;
		currentLevel = levels [l];
		if(currentLvlHeader != null){
			currentLvlHeader.SetActive(false);
		}
		currentLvlHeader = levelHeaders[l];
		currentLvlHeader.SetActive(true);
		Physics2D.gravity = standardGravity * currentLevel.speedModif;
		spawner.dropPrefabs = currentLevel.dropItems;
//		levelDuration = currentLevel.duration;
		spawner.spawnPeriod = currentLevel.spawnInterval;
		//будет ли торнадо, определяю случайным образом
		//если будет, то через 20 с с начаа уровня
		if(currentLevel.tornado){
			tornadoMoment = levelDuration - 20.0f;
		}
		else{
			tornadoMoment = levelDuration;
		}
		startSpawn ();
		updateScores(0);
	}

	public bool isPlaying {
		get {
			return playing;
		}
		set {
			playing = value;
		}
	}

	public LevelSettings levelSettings{
		get{
			return currentLevel;
		}
	}

	public void onExitClick ()
	{
		States.State<StateGame> ().onQuitClick ();
	}

	public void onRestartClick ()
	{
		restart ();
	}

	public void onNextClick ()
	{
		States.State<StateGame> ().stageCompleted ();
	}

	public void updateScores (int scores)
	{
		scoreText.text = scores.ToString ();
//		totalScores.text = Global.instance.totalScores.ToString();
	}

	public void pauseRain(bool state){
		if(!state && isPlaying){
			spawner.pauseSpawn(false);
		}else{
			spawner.pauseSpawn(true);
		}
	}

	void startSpawn ()
	{
		playing = true;
		if(backgroundMusic != null){
			Global.instance.soundManager.playMusic(backgroundMusic, true);
		}
		timeLeft = levelDuration;
		bucket.toggleControl (true);
		if (currentLevel.rainSettings != null) {
			rainCloud.gameObject.SetActive (true);
			rainCloud.startDrop (currentLevel.rainSettings, currentLevel.rainIsContinue);
		}
		if (currentLevel.snowSettings != null) {
			snowCloud.gameObject.SetActive (true);
			snowCloud.startDrop (currentLevel.snowSettings, currentLevel.rainIsContinue);
		}
		lightning.gameObject.SetActive (currentLevel.lightnings);
		grasshopperSpawnerLeft.gameObject.SetActive(currentLevel.grasshoppers);
		grasshopperSpawnerRight.gameObject.SetActive(currentLevel.grasshoppers);

		spawner.startSpawn ();
	}

	IEnumerator  stopSpawn ()
	{
		playing = false;
		spawner.stopSpawn ();
		rainCloud.stopDrop ();
		rainCloud.gameObject.SetActive (false);
		snowCloud.stopDrop ();
		snowCloud.gameObject.SetActive (false);
		lightning.gameObject.SetActive(false);
		grasshopperSpawnerLeft.gameObject.SetActive(false);
		grasshopperSpawnerRight.gameObject.SetActive(false);
		yield return new WaitForSeconds(3.0f);
		if(backgroundMusic != null){
			Global.instance.soundManager.stopMusic(backgroundMusic);
		}
		Global.instance.soundManager.playSoundOnChannel(SoundManager.Start_END_CHANNEL, levelEndSound, false);
		States.State<StateGame>().endLevel();
	}
	
	void updateTimer ()
	{
		timer.text = "Time left: " + Mathf.RoundToInt (timeLeft);
	}
}
