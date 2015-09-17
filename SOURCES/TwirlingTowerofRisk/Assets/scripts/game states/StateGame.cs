using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StateGame : BaseState
{
	public enum GameMode
	{
		StageMode,
		LevelMode
	}

	const int MAX_LIVES_COUNT = 3;
	const int STAR_POINTS_COUNT = 100;
	int level = 1;
	int lives;
	bool _spawnCompleted = false;
	public int scores;
	int levelDuration = 90;
	GUIGame gui;
	GameMode mode = GameMode.StageMode;

	public override void OnEntered ()
	{
		if(gui == null){
			gui = GameObject.FindObjectOfType<GUIGame> ();
		}
		base.OnEnter ();
#if UNITY_WEBPLAYER
		Application.ExternalCall ("OnUnityInited", 1);
#else
		level = 1;
		scores = 0;
//		showSplashScreen();
		startBriefing ();
#endif
	}

	public void JSCallFromPageMode (int m)
	{
		if (m == 0) {
			mode = GameMode.StageMode;
		} else {
			mode = GameMode.LevelMode;
		}
	}

	public void JSCallFromPageScores (int s)
	{
		Global.instance.totalScores = s;
	}

	public void JSCallFromPageTime (int t)
	{
		levelDuration = t;
	}

	public void JSCallFromPageLevel (int l)
	{
		level = l;
	}

	public void JSCallFromPageStart (int needSpScreen)
	{
		if (needSpScreen == 1) {
			showSplashScreen ();
		} else {
			startBriefing ();
		}
	}

	public override void OnExit ()
	{
		base.OnExit ();
		Global.instance.soundManager.stopMusic(gui.backgroundMusic);
	}

	void showSplashScreen ()
	{
		States.Push<PSplashScreenState> ();
	}

	public void endLevel ()
	{
		if (currentLevel < gui.levels.Count) {
			States.State<PStateEndLevel> ().scores = scores;
			States.Push<PStateEndLevel> ();
		} else {
			States.Push<PStateWin> ();
		}
		gui.stopImmidiately ();
	}

	public bool spawnCompleted {
		get {
			return _spawnCompleted;
		}
	}

	public void startBriefing ()
	{
		States.Push<PStateStartLevel> ();
	}

	public void startLevel ()
	{
		lives = MAX_LIVES_COUNT;
		gui.livesBar.restart ();
		gui.updateScores (scores);
		StopAllCoroutines();
		isLightnong = false;
		if (level <= gui.levels.Count)
			restartLevel ();
	}

	void restartLevel ()
	{
		scores = 0;
		gui.restart ();
		setLightningPeriod ();
	}

	public int currentLevel {
		get {
			return level;
		}
	}

	public int levelTime {
		get {
			return levelDuration;
		}
	}

	public string getTimeResult {
		get {
			return gui.getTimeString;
		}
	}

	public void goToNextLevel ()
	{
#if UNITY_WEBPLAYER
		Application.ExternalCall ("GoToNextLevel", scores);
#else
		level++;
		Global.instance.totalScores += scores;
		startBriefing();
#endif
	}

//	public void completeGame ()
//	{
//#if UNITY_WEBPLAYER
//		Application.ExternalCall ("GameCompleted", scores);
//#else
//		States.Change<StateMainMenu>();
//#endif
//	}

	public void exitGame ()
	{
#if UNITY_WEBPLAYER
		Application.ExternalCall ("ExitGame", scores);
#else
		States.Change<StateMainMenu>();
#endif
	}

	public void replayGame ()
	{
#if UNITY_WEBPLAYER
		Application.ExternalCall ("ReplayGame", scores);
#else
		level = 1;
		startBriefing ();
#endif

		scores = 0;
	}

	public void onCloseClick(){
		States.Push<PStatePause>();
	}

	public void replayLevel ()
	{

#if UNITY_WEBPLAYER
		Application.ExternalCall ("ReplayLevel", scores);
#else
		startBriefing ();
#endif
		scores = 0;
	}

	public void failLevel ()
	{
		if (States.Current == States.State<StateGame> ()) {
			gui.stopImmidiately ();
			States.State<PStateFail> ().scores = scores;
			States.Push<PStateFail> ();
		}
	}

	public LevelSettings settings {
		get {
			if(gui == null){
				gui = GameObject.FindObjectOfType<GUIGame> ();
			}
			return gui.levelSettings;
		}
	}

	public bool damaged {
		get {
			return lives < MAX_LIVES_COUNT;
		}
	}

	public void catchSpeedBonus (Bonus bo)
	{
		bo.onCatched ();
		gui.butterfly.speedUp ();
	}

	public void catchLifeBonus (Bonus bo)
	{
		bo.onCatched ();
		if (lives < MAX_LIVES_COUNT) {
			lives++;
			gui.livesBar.addLife ();
		}
	}

	public void catchPointsBonus (Bonus bo)
	{
		bo.onCatched ();
		scores += STAR_POINTS_COUNT;
		gui.updateScores (scores);
	}

	public void catchFruit (Fruit fr)
	{
		fr.onCatched ();
		scores += fr.reward;
		gui.updateScores (scores);
	}

	public void balloonCatched ()
	{
		lives--;
		gui.livesBar.removeLife ();
		if (lives > 0) {
			gui.butterfly.onDamage ();
		} else {
			gui.butterfly.onKilled ();
			failLevel ();
		}
	}

	public void inTornado ()
	{
		if (lives == 0)
			return;
		lives--;
		gui.livesBar.removeLife ();
		if (lives > 0) {
			gui.butterfly.onDamage ();
		} else {
			gui.butterfly.onKilled ();
			failLevel ();
		}
	}

	public Bonus.BonusType getBonusType ()
	{
		if (damaged && Random.value < gui.levelSettings.heartChance) {
			return Bonus.BonusType.Heart;
		}
		if (isLightnong) {
			StartCoroutine (lightningCoroutine ());
			isLightnong = false;
			return Bonus.BonusType.Lightning;
		}
		return Bonus.BonusType.Star;
	}

	#region lightning
	Vector2 lSpawnPeriod;
	bool isLightnong = false;

	void setLightningPeriod ()
	{
		float period = States.State<StateGame>().levelTime / (gui.levelSettings.maxLightningsCount + 1);
		lSpawnPeriod = new Vector2 (period + 1, period + 3);
		StartCoroutine (lightningCoroutine ());
	}

	IEnumerator lightningCoroutine ()
	{
		float r = Random.Range (lSpawnPeriod.x, lSpawnPeriod.y);
		yield return new WaitForSeconds (r);
		isLightnong = true;
	}

	#endregion
}
