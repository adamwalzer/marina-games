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
	const int COIN_POINTS_COUNT = 200;
	const int STAR_POINTS_COUNT = 50;
	const int FLIP_POINTS_CONT = 500;
	const int ROCK_LOST_POINTS = 250;
	const int MAGIC_STAR_POINTS = 500;
	const int SPEED_ORB_POINTS = 250;
	const int STACK_OF_ROCKS_POINTS = 350;
	int level = 1;
	int lives;
	bool _spawnCompleted = false;
	public int scores;
	int levelTime = 30;
	GUIGame gui;
	GameMode mode = GameMode.StageMode;
	bool needHeart = false;

//	void Update ()
//	{
//		if (Input.GetKeyDown (KeyCode.Escape)) {
//			onQuitClick ();
//		}
//	}

	public override void OnEntered ()
	{
		gui = GameObject.FindObjectOfType<GUIGame> ();
		base.OnEnter ();
#if (UNITY_WEBPLAYER || UNITY_WEBGL) && !UNITY_EDITOR
		Application.ExternalCall ("OnUnityInited", 1);
#else
		level = Global.instance.currentLevel;
		scores = 0;
//		levelTime = 60;
//		showSplashScreen ();
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
		levelTime = t;
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

	public void JSCallFromPageCompleteGame ()
	{
		States.Push<PStateWin> ();
	}

	public override void OnExit ()
	{
		base.OnExit ();
	}

	void showSplashScreen ()
	{
		States.Push<PSplashScreenState> ();
	}

	public void endLevel ()
	{
		if (currentLevel <= gui.levels.Count) {
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
		gui.updateScores ();
		if (level <= gui.levels.Count)
			restartLevel (true);
	}

	void restartLevel (bool afterRock)
	{
		if (afterRock) {
			scores = 0;
			gui.restart ();
		} else {
			scores -= ROCK_LOST_POINTS;
			if (scores < 0)
				scores = 0;
			gui.erzRestartLevel ();
		}

	}

	public int currentLevel {
		get {
			return level;
		}
	}

	public string getTimeResult {
		get {
			return gui.getTimeString;
		}
	}

	public int levelDuration {
		get {
			return levelTime;
		}
	}

	public void goToNextLevel ()
	{
		#if (UNITY_WEBPLAYER || UNITY_WEBGL) && !UNITY_EDITOR
		if(currentLevel == gui.levels.Count){
			States.Push<PStateWin> ();
		}else{
			Application.ExternalCall ("GoToNextLevel", scores);
		}
#else
		if(currentLevel == gui.levels.Count){
			States.Push<PStateWin> ();
		}else{
			level++;
			Global.instance.totalScores += scores;
			Global.instance.currentLevel = level;
			startBriefing ();
		}

#endif
	}

//	public void completeGame ()
//	{
//		#if (UNITY_WEBPLAYER || UNITY_WEBGL) && !UNITY_EDITOR
//		Application.ExternalCall ("GameCompleted", scores);
//#else
//		gui.stopMusic();
//		States.Change<StateMainMenu> ();
//#endif
//	}

	public void exitGame ()
	{
		#if (UNITY_WEBPLAYER || UNITY_WEBGL) && !UNITY_EDITOR
		Application.ExternalCall ("ExitGame", scores);
#else
//		Application.Quit ();
		gui.stopMusic();
		States.Change<StateMainMenu> ();
#endif
	}

	public void replayGame ()
	{
		#if (UNITY_WEBPLAYER || UNITY_WEBGL) && !UNITY_EDITOR

		Application.ExternalCall ("ReplayGame", scores);
#else
//		level = 1;
		Global.instance.currentLevel = 1;
//		startBriefing ();
		States.Change<StateMainMenu> ();
#endif
		scores = 0;
	}

	public void replayLevel ()
	{
		#if (UNITY_WEBPLAYER || UNITY_WEBGL) && !UNITY_EDITOR

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

	public void restartAfterWater ()
	{
		gui.stopImmidiately ();
		restartLevel (false);
	}

	public void onQuitClick ()
	{
		States.Push<PStateQuitWarning> ();
	}

	public LevelSettings settings {
		get {
			return gui.levelSettings;
		}
	}

	public bool needCreateHeart ()
	{
		if (lives < MAX_LIVES_COUNT && needHeart) {
			needHeart = false;
			return true;
		}
		return false;
	}

	public void catchedBonus (Bonus bonus)
	{
		switch (bonus.type) {
		case Bonus.BonusType.Heart:
			catchLifeBonus (bonus);
			break;
		case Bonus.BonusType.Coin:
			catchPointsBonus (COIN_POINTS_COUNT, bonus);
			break;
		case Bonus.BonusType.Star:
			catchPointsBonus (STAR_POINTS_COUNT, bonus);
			break;
		case Bonus.BonusType.SpiralOrange:
		case Bonus.BonusType.SpiralViolet:
			catchImmunityBonus (bonus);
			scores += MAGIC_STAR_POINTS;
			break;
		case Bonus.BonusType.Orb:
			catchSpeedBonus (bonus);
			scores += SPEED_ORB_POINTS;
			break;
		}
	}

	void catchLifeBonus (Bonus bo)
	{
		bo.onCatched ();
		if (lives < MAX_LIVES_COUNT) {
			lives++;
			gui.livesBar.addLife ();
		}
	}

	void catchPointsBonus (int points, Bonus bo)
	{
		bo.onCatched ();
		scores += points;
		gui.updateScores ();
	}

	public void catchImmunityBonus (Bonus bo)
	{
		bo.onCatched ();
		gui.robot.setImmunity ();
	}

	public void catchSpeedBonus (Bonus bo)
	{
		bo.onCatched ();
		gui.robot.speedUp ();
	}

	public void jumpOnRockStack ()
	{
		scores += STACK_OF_ROCKS_POINTS;
		gui.updateScores ();
	}

	public void onRock ()
	{
		lives--;
		needHeart = true;
		gui.livesBar.removeLife ();
		if (lives == 0) {
			failLevel ();
		}
	}

	public void onFlip ()
	{
		scores += FLIP_POINTS_CONT;
		gui.updateScores ();
	}

	public void addScore(int count){
		scores += count;
	}
}
