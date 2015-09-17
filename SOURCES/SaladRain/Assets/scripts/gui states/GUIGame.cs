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
	public IngredientsPanel ingredientsPanel;
	public AudioClip backgroundMusic;
	public Animator colorSprite;
	public Image recipeImage;

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
		Physics2D.gravity = currentLevel.grevity;
		recipeImage.overrideSprite = currentLevel.recipe.image;
		timer.GetComponent<Animator> ().SetTrigger ("Restart");
		tapController.play ();
		ingredientsPanel.init (currentLevel.recipe);
		saladbowl.reset ();
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

	public void updateScores (int scores)
	{
//		scoreText.text = scores.ToString();
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
		StopCoroutine("colorBlick");
		resetColorSprite();
	}

	public void showBlick (float param)
	{
		StartCoroutine (colorBlick (param));
	}

	public void playSound(AudioClip sound){
		Global.instance.soundManager.playSound(sound);
	}

	void updateTimer ()
	{
		int t = Mathf.RoundToInt (timeLeft);
		int sec = t % 60;
		int min = t / 60;
		if (min == 0 && sec == 10) {
			timer.GetComponent<Animator> ().SetTrigger ("LastTime");
		}
		string sSec = sec >= 10 ? sec.ToString () : "0" + sec;
		string sMin = min >= 10 ? min.ToString () : "0" + min;
		timer.text = "TIME: " + sMin + ":" + sSec;
	}

	void resetColorSprite ()
	{
		colorSprite.SetFloat ("Color", 0);
	}

	IEnumerator colorBlick (float param)
	{
		colorSprite.SetFloat ("Color", param);
		Global.instance.soundManager.playSound(meterFilledSound);
		yield return new WaitForSeconds (3);
		resetColorSprite ();
	}
}
