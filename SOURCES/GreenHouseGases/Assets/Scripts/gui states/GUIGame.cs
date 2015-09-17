using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class GUIGame : MonoBehaviour
{
	[HideInInspector]
	public delegate void onStopSpawnerDelegate();
	[HideInInspector]
	public static onStopSpawnerDelegate onRemoveAllDucks;

	public Spawner spawner;
	public Vector2 standardGravity = new Vector3 (0.0f, 9.81f);
	public float levelDuration = 90.0f;
	float timeLeft;
	public Text timer;
	public Text minutes;
	public Text secundes;
	public SunflowerController sunflower;
	public SunflowerController sunflowerBouquet;
	public Text totalScores;
	public Text scoreText;
	public Text levelText;
	public List<LevelSettings> levels;
	public VehicleController vehiclesController;
	public SoundManager soundManager;
	public AudioClip backgroundMusic;
	public AudioClip domeSound;
	bool playing = false;
	LevelSettings currentLevel;
	public SunflowerController currentSunFlower;
	public Image domeSprite;
//	public DuckSpawner leftDuckSpawner;
//	public DuckSpawner rightDuckSpawner;
	public DuckSpawner duckSpawner;
	public GameObject arrows;
	public GameObject road;

	int domeValue;

	void FixedUpdate ()
	{
		if (!playing)
			return;
		timeLeft -= Time.deltaTime;
		if (timeLeft < 0) {
			timeLeft = 0;
			StartCoroutine (stopSpawn ());
		}
		updateTimer ();
	}

	public void restart ()
	{
		int l = States.State<StateGame> ().currentLevel - 1;
		currentLevel = levels [l];
		levelText.text = "Level " + (l + 1);
		Physics2D.gravity = standardGravity * currentLevel.speedModif;
		levelDuration = States.State<StateGame>().levelTime;
		if(levelDuration <= 0){
			levelDuration = currentLevel.duration;
		}
		spawner.spawnPeriod = currentLevel.spawnInterval;
		sunflower.gameObject.SetActive (false);
		sunflowerBouquet.gameObject.SetActive (false);
		if (currentLevel.bouckuet) {
			currentSunFlower = sunflowerBouquet;
		} else {
			currentSunFlower = sunflower;
		}
		domeValue = 0;
		domeSprite.fillAmount = 0;
		domeSprite.gameObject.SetActive (levelSettings.smogDome > 0);
		currentSunFlower.gameObject.SetActive (true);
		startSpawn ();
		updateScores (0);
	}

	public bool isPlaying {
		get {
			return playing;
		}
		set {
			playing = value;
		}
	}

	public LevelSettings levelSettings {
		get {
			return currentLevel;
		}
	}

	public void onExitClick ()
	{
		States.Change<StateMainMenu> ();
	}

	public void onRestartClick ()
	{
		restart ();
	}

//	public void onNextClick ()
//	{
//		States.State<StateGame> ().stageCompleted ();
//	}

	public void updateScores (int scores)
	{
		scoreText.text = scores.ToString ();
		totalScores.text = Global.instance.totalScores.ToString ();
	}

	public void addSmoge (int smog)
	{
		if (isPlaying && levelSettings.smogDome > 0) {
			domeValue += smog;
			domeSprite.fillAmount = (float)domeValue / levelSettings.smogDome;
			if (domeValue >= levelSettings.smogDome) {
				States.State<StateGame> ().onDomeFull ();
			}
		}
	}

	public void stopImmidiately ()
	{
		playing = false;
		spawner.stopSpawn ();
		currentSunFlower.gameObject.SetActive (false);
		vehiclesController.stopSpawn ();
		currentSunFlower.toggleControl (false);
		duckSpawner.stopSpawn();
//		rightDuckSpawner.stopSpawn();
		if (backgroundMusic != null) {
			Global.instance.soundManager.stopMusic (backgroundMusic);
		}
		road.SetActive(false);
		domeSprite.gameObject.SetActive(false);
	}

	void startSpawn ()
	{
		playing = true;
		if (backgroundMusic != null) {
			Global.instance.soundManager.playMusic (backgroundMusic, true);
		}
		timeLeft = levelDuration;
		currentSunFlower.gameObject.SetActive (true);
		currentSunFlower.toggleControl (true);
		spawner.startSpawn ();
	
//		removeDucks();

		if(levelSettings.duck){
			duckSpawner.startSpawn(levelSettings.duckSpawn, levelSettings.duckForce, levelSettings.duckSpeed);
//			rightDuckSpawner.startSpawn(levelSettings.duckSpawn, levelSettings.duckForce, levelSettings.duckSpeed);
		}
		vehiclesController.startSpawn ();
		if(currentLevel.level == 1){
			arrows.SetActive(true);
		}
		road.SetActive(true);
		domeSprite.gameObject.SetActive(true);
	}

	IEnumerator  stopSpawn ()
	{
		playing = false;
		spawner.stopSpawn ();
		vehiclesController.stopSpawn ();
		duckSpawner.stopSpawn();
//		rightDuckSpawner.stopSpawn();
		yield return new WaitForSeconds (3.0f);
		if (backgroundMusic != null) {
			Global.instance.soundManager.stopMusic (backgroundMusic);
		}
		currentSunFlower.gameObject.SetActive (false);
		currentSunFlower.toggleControl (false);
		States.State<StateGame> ().endLevel ();
		road.SetActive(false);
		domeSprite.gameObject.SetActive(false);
		removeDucks();
	}
	
	void updateTimer ()
	{
		timer.text = "Time left:\n" + Mathf.RoundToInt (timeLeft);
		int t = Mathf.RoundToInt (timeLeft);
		int sec = t % 60;
		int min = t / 60;
		minutes.text = "0 " + min;
		string s = sec.ToString ();
		if (sec >= 10) {
			secundes.text = s [0] + " " + s [1];
		} else {
			secundes.text = "0 " + s [0];
		}

	}

	void removeDucks(){
		if(onRemoveAllDucks != null){
			onRemoveAllDucks();
		}
	}
}
