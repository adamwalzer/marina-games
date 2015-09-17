using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class GUIGame : MonoBehaviour
{
	public Spawner spawner;
	public Text timer;
	public List<LevelSettings> levels;
	public Saladbowl saladbowl;
	public TapController tapController;
	public SoundManager soundManager;
	public AudioClip backgroundMusic;
	public Image levelImage;
	public Sprite[] levelImages;
	public Text quest;
	public Text result;
	public GameObject greenHUDText;
	public GameObject redHUDText;
	public Animator logoAnim;

	public AudioClip meterFilledSound;

	float timeLeft;
	bool playing = false;
	LevelSettings currentLevel;
	StateGame stateG;

	void Start ()
	{
		stateG = States.State<StateGame> ();
		int l = stateG.currentLevel - 1;
		currentLevel = levels [l];
	}

	void FixedUpdate ()
	{
		if (!playing)
			return;
		timeLeft -= Time.deltaTime;
		if (timeLeft < 0)
			timeLeft = 0;
		if (timeLeft == 0) {
			isPlaying = false;
			stateG.endSpawn ();
		}
		updateTimer ();
	}

	public void restart ()
	{
		int l = stateG.currentLevel - 1;
		currentLevel = levels [l];
		timeLeft = stateG.levelTime;
		quest.text = "$" + currentLevel.quest.ToString(".00");
		levelImage.overrideSprite = levelImages[l];
		logoAnim.SetTrigger("Show");
		tapController.play ();
		startSpawn ();
		updateScores (0);
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
		States.State<StateGame> ().onCloseClick ();
	}

	public void onRestartClick ()
	{
		restart ();
	}

	public void updateScores (float scores)
	{
		result.text = scores.ToString("0.00");
	}

	void startSpawn ()
	{
		playing = true;
		if (backgroundMusic != null) {
			Global.instance.soundManager.playMusic (backgroundMusic, true);
		}
		spawner.startSpawn (currentLevel);
	}

	public void stopImmidiately ()
	{
		playing = false;
		spawner.stopSpawn ();
//		if (backgroundMusic != null) {
//			Global.instance.soundManager.stopMusic (backgroundMusic);
//		}
	}

	public void playSound(AudioClip sound){
		Global.instance.soundManager.playSound(sound);
	}

	void updateTimer ()
	{
		int t = Mathf.RoundToInt (timeLeft);
		int sec = t % 60;
		timer.text = sec.ToString("00");
	}

	public void showText(string text, bool isGood, Vector3 pos){
		GameObject go;
		if(isGood){
			go = Global.instance.pool.get(greenHUDText, pos);
		}else{
			go = Global.instance.pool.get(redHUDText, pos);
		}
		HUDText hT = go.GetComponent<HUDText>();
		hT.init(text, isGood);
	}
}
