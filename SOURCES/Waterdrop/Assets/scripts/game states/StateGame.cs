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

	public GameMode mode = GameMode.StageMode;
	public const int WATER_DROP_POINTS = 100;
	public const int SMALL_DROP_POINTS = 50;
	public const int CRAZY_DROP_POINTS = 1000;
	public const int SNOWFLAKE_POINTS = 500;
	public const int BANANA_POINTS = -35;
	public const int SALT_POINTS = -200;
	public const int GRASSHOPPER_POINTS = -200;
	int level = 1;
	bool _spawnCompleted = false;
	public int scores;
	GUIGame gui;
	public float bananaMod = 1.0f;

	public override void OnEntered ()
	{
		GameObject go = GameObject.FindGameObjectWithTag ("MainObject");
		gui = go.GetComponent<GUIGame> ();
		Global.instance.soundManager = gui.soundManager;
		base.OnEnter ();
#if UNITY_WEBPLAYER && !UNITY_EDITOR
		Application.ExternalCall ("OnUnityInited", 1);
#else
		level = 1;
		scores = 0;
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

	public void JSCallFromPageTime(int time){
		gui.levelDuration = time;
	}

	public void JSCallFromPageLevel (int l)
	{
		level = l;
		startBriefing ();
	}

	public override void OnExit ()
	{
		base.OnExit ();
	}

	public void endLevel ()
	{
		if (level == 9) {
			States.Push<PStateWin> ();
		} else {
			States.Push<PStateEndLevel> ();
		}
	}

	public bool spawnCompleted {
		get {
			return _spawnCompleted;
		}
	}

	public void startBriefing ()
	{
		//окна начала уровня показываю только на трех уровнях: 1, 4, 7
//		if (mode == GameMode.StageMode && (level == 1 || level == 4 || level == 7)) {
//			States.Push<PStateStartLevel> ();
//		} else {
//			startLevel ();
//		}
		States.Push<PStateStartLevel>();
	}

	public void startLevel ()
	{
		gui.updateScores (scores);
		if (level < 10)
			restartLevel ();
	}

	void restartLevel ()
	{
		scores = 0;
		gui.restart ();
	}

	public int currentLevel {
		get {
			return level;
		}
	}

	public void onQuitClick(){
		States.Push<PStateQuitWarning> ();
	}

	public void goToNextLevel ()
	{
#if UNITY_WEBPLAYER && !UNITY_EDITOR
		Application.ExternalCall ("GoToNextLevel", scores);
#else
		level++;
		Global.instance.totalScores += scores;
		startBriefing ();
#endif
	}

	public void stageCompleted ()
	{
#if UNITY_WEBPLAYER && !UNITY_EDITOR
		Application.ExternalCall ("StageCompleted", scores);
#else
		level++;
		startBriefing ();
#endif
	}

	public void gameCompleted ()
	{
#if UNITY_WEBPLAYER && !UNITY_EDITOR
		Application.ExternalCall ("ExitGame", scores);
#else
		Application.Quit();
#endif
	}

	public void replayLevel ()
	{
#if UNITY_WEBPLAYER && !UNITY_EDITOR
		Application.ExternalCall ("ReplayLevel", scores);
#else
		//		level--;
		startBriefing ();
#endif
		scores = 0;
	}

	public void exitGame(){
#if UNITY_WEBPLAYER && !UNITY_EDITOR
		Application.ExternalCall("ExitGame", scores);
#else
//		States.Change<StateMainMenu>();
		Application.Quit();
#endif
	}

	public void toggleBucket (bool state)
	{
		gui.bucket.toggleControl (state);
	}

	public float currentSpeedMod {
		get {
			return gui.levelSettings.speedModif;
		}
	}

	public void addWater (float size)
	{
		scores += (int)(WATER_DROP_POINTS * size);
		gui.bucket.onWater ();
		gui.updateScores (scores);
	}

	public void addBanana ()
	{
		scores += BANANA_POINTS;
		gui.bucket.onBanana ();
		gui.updateScores (scores);
	}

	public void addSalt ()
	{
		scores += SALT_POINTS;
		gui.bucket.onSalt ();
		gui.updateScores (scores);
	}

	public void addSnow (float size)
	{
		scores += SNOWFLAKE_POINTS;
		gui.updateScores (scores);
		gui.spawner.onSnowflake ();
		gui.bucket.onSnowflake ();
		StartCoroutine (slowBanana ());
	}

	public void addRainbow ()
	{
		scores += CRAZY_DROP_POINTS;
		gui.updateScores (scores);
		StartCoroutine (downpour ());
	}

	public void addGrasshopper ()
	{
		scores += GRASSHOPPER_POINTS;
		gui.bucket.onGrasshopper ();
		gui.updateScores (scores);
	}

	public void onTornado ()
	{
		StartCoroutine (tornado ());
	}

	public void onLigting (bool state)
	{
		gui.pauseRain (state);
	}

	IEnumerator slowBanana ()
	{
		bananaMod = 0.5f;
		yield return new WaitForSeconds (10.0f);
		bananaMod = 1.0f;
	}

	IEnumerator downpour ()
	{
		gui.spawner.startDownpour ();
		gui.bucket.onCrazyDrop ();
		yield return new WaitForSeconds (10.0f);
		gui.spawner.stopDownpour ();
	}

	IEnumerator tornado ()
	{
		gui.spawner.startTornado ();
		yield return new WaitForSeconds (10.0f);
		gui.spawner.stopTornado ();
	}
}
