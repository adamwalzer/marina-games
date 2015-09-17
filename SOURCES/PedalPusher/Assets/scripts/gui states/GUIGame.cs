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
	public Robot robot;
	public LivesBar livesBar;
	public AudioClip loseLifeSound;
	public BackgroundTransporter[] backgrounds;
	float timeSpend = 0f;
	bool playing = false;
	LevelSettings currentLevel;
	int levelDuration = 30;

	void Start ()
	{
		int l = States.State<StateGame> ().currentLevel - 1;
		currentLevel = levels [l];
		levelText.text = "Level " + (l + 1);
	}

	void FixedUpdate ()
	{
		if (!playing)
			return;
		timeSpend += Time.deltaTime;
		updateTimer ();
		if(timeSpend >= levelDuration){
			States.State<StateGame> ().endLevel ();
		}
	}

	public void restart ()
	{
		StateGame sGame = States.State<StateGame> ();
		cam.transform.position = startCamPos;
		int l = sGame.currentLevel - 1;
		levelDuration = sGame.levelDuration;
		currentLevel = levels [l];
		totalScoreText.text = "Total: " + Global.instance.totalScores;
		levelText.text = "Level " + (l + 1);
		timeSpend = 0;
		livesBar.restart ();
		startSpawn ();
		updateScores ();
		updateTimer ();
	}

	public void erzRestartLevel(){
		cam.transform.position = startCamPos;
		int l = States.State<StateGame> ().currentLevel - 1;
		currentLevel = levels [l];
		totalScoreText.text = "Total: " + Global.instance.totalScores;
		levelText.text = "Level " + (l + 1);
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
			return timeSpend;
		}
	}

	public string getTimeString {
		get {
			return timer.text;
		}
	}

	public void onExitClick ()
	{
		stopMusic();
		States.Change<StateMainMenu> ();
	}

	public void onRestartClick ()
	{
		restart ();
	}

	public void updateScores ()
	{
		scoreText.text = States.State<StateGame>().scores.ToString ();
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
	}

	public void onCloseClick ()
	{
		States.State<StateGame> ().onQuitClick ();
	}

	public void stopMusic(){
		if (backgroundMusic != null) {
			Global.instance.soundManager.stopMusic (backgroundMusic);
		}
	}

	void updateTimer ()
	{
		int t = Mathf.RoundToInt (timeSpend);
		int sec = t % 60;
		int min = t / 60;
		string sSec = sec >= 10 ? sec.ToString () : "0" + sec;
		string sMin = min >= 10 ? min.ToString () : "0" + min;
		timer.text = sMin + ":" + sSec;
	}

	void resetBackground ()
	{
		foreach (BackgroundTransporter bT in backgrounds) {
			bT.init ();
		}
	}

	void clearWorld ()
	{
		int ch = world.childCount;
		for (int i = 0; i < ch; i++) {
			Obstacle o = world.GetChild (i).GetComponent<Obstacle> ();
			if (o != null) {
				o.deinit ();
			} else {
				BonusPattern b = world.GetChild (i).GetComponent<BonusPattern> ();
				if (b != null) {
					b.deinit ();
				} else {
					Bonus bo = world.GetChild(i).GetComponent<Bonus>();
					if(bo != null){
						bo.deinit();
					}else{
						Debug.LogError ("unnown object on world");
					}

				}

			}
		}
	}
}
