using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class GUIGame : MonoBehaviour
{
	public Transform cam;
	public Vector3 startCamPos;
	public LevelBuilder levelBuilder;
	public Transform world;
	public Text timer;
	public Text scoreText;
	public Text levelText;
	public List<LevelSettings> levels;
	public SoundManager soundManager;
	public AudioClip backgroundMusic;
	public AudioSource sharkMusic;
	public TapController tapController;
	public Flyer butterfly;
	public LivesBar livesBar;
	public AudioClip loseLifeSound;
	public MovingBackground[] backgroundParts;
	float timeSpend = 0f;
	bool playing = false;
	LevelSettings currentLevel;
	StateGame stateG;

	void Start ()
	{
		stateG = States.State<StateGame>();
		int l = stateG.currentLevel - 1;
		currentLevel = levels [l];
		levelText.text = "Level " + (l + 1);
	}

	void FixedUpdate ()
	{
		if (!playing)
			return;
		timeSpend += Time.deltaTime;
		updateTimer ();
		if(timeSpend >= stateG.levelTime){
			isPlaying = false;
			stateG.endLevel();
		}
	}

	public void restart ()
	{
		cam.transform.position = startCamPos;
		int l = stateG.currentLevel - 1;
		currentLevel = levels [l];
		levelText.text = "Level " + (l + 1);
		timeSpend = 0;
		livesBar.restart ();
		startSpawn ();
//		updateScores (0);
		updateTimer ();
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
		States.State<StateGame>().onCloseClick();
	}

	public void onRestartClick ()
	{
		restart ();
	}

	public void updateScores (int scores)
	{
		scoreText.text = scores.ToString();
	}

	void startSpawn ()
	{
		playing = true;
		if (backgroundMusic != null) {
			Global.instance.soundManager.playMusic (backgroundMusic, true);
		}
		resetBackground ();
		clearWorld ();
//		timeSpend = States.State<StateGame> ().levelTime;
		tapController.init ();
		butterfly.play (currentLevel);
		levelBuilder.init (currentLevel);
	}

	public void stopImmidiately ()
	{
		playing = false;
		tapController.stop ();
		butterfly.stop ();
		levelBuilder.deinit ();
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
		timer.text = "TIME: " + sMin + ":" + sSec;
	}

	void resetBackground ()
	{
		foreach (MovingBackground mB in backgroundParts) {
			mB.reset ();
		}
	}

	void clearWorld ()
	{
		int ch = world.childCount;
		for (int i = 0; i < ch; i++) {
//			IClearable o = world.GetChild (i).GetComponent<IClearable> ();
//			if (o != null) {
//				o.deinit ();
//			} else {
//				BonusPattern b = world.GetChild (i).GetComponent<BonusPattern> ();
//				if (b != null) {
//					b.deinit ();
//				} else {
//					Balloon ba = world.GetChild (i).GetComponent<Balloon> ();
//					if (ba != null) {
//						ba.deinit ();
//					} else {
//						Fruit fr = world.GetChild (i).GetComponent<Fruit> ();
//						if (fr != null) {
//							fr.deinit ();
//						} else {
//							Debug.LogError ("unnown object on world");
//						}
//					}
//
//
//				}
//			}
			Destroy(world.GetChild (i).gameObject);
		}
	}
}
