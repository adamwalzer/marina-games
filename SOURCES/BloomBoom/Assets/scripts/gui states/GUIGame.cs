using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class GUIGame : MonoBehaviour
{
	public Transform cam;
	public Vector3 startCamPos;
	public LevelBuilderBricks levelBuilder;
	public Transform world;
	public Text timer;
	public Text scoreText;
	public Text totalScoreText;
	public Text levelText;
	public List<LevelSettings> levels;
	public SoundManager soundManager;
	public AudioClip backgroundMusic;
	public TapController tapController;
	public Hero robot;
	public LivesBar livesBar;
	public AudioClip loseLifeSound;
	public BackgroundTransporter[] backgrounds;
	public ProgressBar pollenMeter;
	public GameObject hudText;
	public GameObject lostPollenEffect; 
	public GameObject arrows;
	float timeLeft = 0f;
	bool playing = false;
	LevelSettings currentLevel;
	StateGame stateGame;

	void Start ()
	{
		stateGame = States.State<StateGame> ();
		int l = stateGame.currentLevel - 1;
		currentLevel = levels [l];
	}

	void FixedUpdate ()
	{
		if (!playing)
			return;
		timeLeft -= Time.deltaTime;
		updateTimer ();
		if(timeLeft <= 0){
			isPlaying = false;
			stateGame.endLevel();
		}
	}

//	void Update(){
//		if(Input.GetKeyDown(KeyCode.Escape)){
//			stateGame.completeGame();
//		}
//	}

	public void restart ()
	{
		cam.transform.position = startCamPos;
		int l = stateGame.currentLevel - 1;
		currentLevel = levels [l];
//		stateGame.levelDuration = currentLevel.levelDuration;
		totalScoreText.text = "Total: " + Global.instance.totalScores;
		timeLeft = stateGame.levelDuration;
		livesBar.restart ();
		if(currentLevel.number == -1){
			levelText.text = "Bonus Round";
		}else{
			levelText.text = "Level " + currentLevel.number;
		}
		startSpawn ();
		updateScores ();
		updateTimer ();
		if(currentLevel.number == 1){
			StartCoroutine(showArrows());
		}
	}

	public void erzRestartLevel(){
		cam.transform.position = startCamPos;
		int l = stateGame.currentLevel - 1;
		currentLevel = levels [l];
		totalScoreText.text = "Total: " + Global.instance.totalScores;
		startSpawn ();
		updateScores ();
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

	public float getTime {
		get {
			return timeLeft;
		}
	}

	public string getTimeString {
		get {
			return timer.text;
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

	public void updateScores ()
	{
		scoreText.text = stateGame.scores.ToString ();
		pollenMeter.changeValue(stateGame.pollen);
	}

	void startSpawn ()
	{
		playing = true;
		if (backgroundMusic != null) {
			Global.instance.soundManager.playMusic (backgroundMusic, true);
		}
		resetBackground ();
		clearWorld ();
		tapController.init ();
		robot.play (currentLevel);
		pollenMeter.init(stateGame.pollen, currentLevel.pollenMeterMax, currentLevel.heroIndex);
		levelBuilder.init (currentLevel);
	}

	public void stopImmidiately ()
	{
		playing = false;
		tapController.stop ();
		robot.stop ();
		levelBuilder.deinit ();
		if (backgroundMusic != null) {
			Global.instance.soundManager.stopMusic (backgroundMusic);
		}
		StopAllCoroutines();
	}

	public void onCloseClick ()
	{
		stateGame.onQuitClick ();
	}

	public void showHUDText(int count, Vector3 position){
		GameObject go = Global.instance.pool.get(hudText, position);
		go.GetComponent<HUDText>().init(count);
	}

	public void showLostPollen(Vector3 pos){
		Global.instance.pool.get(lostPollenEffect, pos);
	}

	void updateTimer ()
	{
		int t = Mathf.RoundToInt (timeLeft);
		int sec = t % 60;
		int min = t / 60;
		string sSec = sec >= 10 ? sec.ToString () : "0" + sec;
		string sMin = min >= 10 ? min.ToString () : "0" + min;
		timer.text = sMin + ":" + sSec;
	}

	void resetBackground ()
	{
		foreach (BackgroundTransporter bT in backgrounds) {
			bT.init (currentLevel.isDay);
		}
	}

	void clearWorld ()
	{
		int ch = world.childCount;
		for (int i = 0; i < ch; i++) {
//			IClearable icl =  world.GetChild (i).GetComponent<IClearable>();
//			if(icl != null){
//				icl.deinit();
//			}
			Destroy(world.GetChild (i).gameObject);
		}
	}

	IEnumerator showArrows(){
		arrows.SetActive(true);
		yield return new WaitForSeconds(2f);
		arrows.SetActive(false);
	}
}
