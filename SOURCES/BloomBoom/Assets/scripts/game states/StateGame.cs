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
	const int ROCK_LOST_POINTS = 150;
	const int CATCH_FLOWER_POINTS = 15;
	const int POLLEN_FLOWER_POINTS = 150;
	int level = 1;
	int lives;
	public int scores;
	public int pollen;
	int levelTime = 90;
	GUIGame gui;
	GameMode mode = GameMode.StageMode;
	bool needHeart = false;

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			onQuitClick ();
		}
	}

	public override void OnEntered ()
	{
		gui = GameObject.FindObjectOfType<GUIGame> ();
		base.OnEnter ();
#if UNITY_WEBPLAYER && !UNITY_EDITOR
		Application.ExternalCall ("OnUnityInited", 1);
#else
		level = Global.instance.currentLevel;
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
		if (currentLevel < gui.levels.Count) {
			States.State<PStateEndLevel> ().scores = scores;
			States.Push<PStateEndLevel> ();
		} else {
			States.Push<PStateWin> ();
		}
		gui.stopImmidiately ();
	}

//	public bool spawnCompleted {
//		get {
//			return _spawnCompleted;
//		}
//	}

	public void startBriefing ()
	{
		States.Push<PStateStartLevel> ();
	}

	public void startLevel ()
	{
		lives = MAX_LIVES_COUNT;
		gui.livesBar.restart ();
		gui.updateScores ();
		StopAllCoroutines ();
		isLightnong = false;
		if (level <= gui.levels.Count)
			restartLevel (true);
	}

	void restartLevel (bool afterRock)
	{
		if (afterRock) {
			scores = 0;
			gui.restart ();
			setLightningPeriod ();
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

	public int levelDuration {
		get {
			return levelTime;
		}
//		set
//		{
//			levelTime = value;
//		}
	}

	public string getTimeResult {
		get {
			return gui.getTimeString;
		}
	}

	public void goToNextLevel ()
	{
		#if UNITY_WEBPLAYER && !UNITY_EDITOR
		Application.ExternalCall ("GoToNextLevel", scores);
#else
		level++;
		Global.instance.totalScores += scores;
		Global.instance.currentLevel = level;
		startBriefing ();
#endif
	}

//	public void completeGame ()
//	{
//		#if UNITY_WEBPLAYER && !UNITY_EDITOR
//		Application.ExternalCall ("GameCompleted", scores);
//#else
//		States.Change<StateMainMenu> ();
//#endif
//	}

	public void exitGame ()
	{
		#if UNITY_WEBPLAYER && !UNITY_EDITOR
		Application.ExternalCall ("ExitGame", scores);
#else
		States.Change<StateMainMenu> ();
#endif
	}

	public void replayGame ()
	{
		#if UNITY_WEBPLAYER && !UNITY_EDITOR
		Application.ExternalCall ("ReplayGame", scores);
#else
		Global.instance.currentLevel = 1;
		States.Change<StateMainMenu> ();
#endif
		scores = 0;
	}

	public void replayLevel ()
	{
		#if UNITY_WEBPLAYER && !UNITY_EDITOR
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

	public void restartAfterRock ()
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

	public bool damaged {
		get {
			if(lives < MAX_LIVES_COUNT){
				if(needHeart){
					needHeart = false;
					return true;
				}else{
					return false;
				}
			}else{
				return false;
			}
		}
	}

	public void catchedBonus (Bonus bonus)
	{
		switch (bonus.type) {
		case Bonus.BonusType.Heart:
			catchLifeBonus (bonus);
			break;
		case Bonus.BonusType.Lightning:
			catchSpeedBonus (bonus);
			break;
		}
	}

	public void onCatchedFlower (Flower f)
	{
		f.onCatched ();
		scores += CATCH_FLOWER_POINTS;
		if (pollen < settings.pollenMeterMax) {
			pollen++;
		}
		gui.updateScores ();
		gui.showHUDText (CATCH_FLOWER_POINTS, f.transform.position);
	}

	public void onPollenFlower (FlowerFoliage ff)
	{
		ff.onCatched ();
		scores += POLLEN_FLOWER_POINTS;
		pollen--;
		gui.updateScores ();
		gui.showHUDText (POLLEN_FLOWER_POINTS, ff.transform.position);
		if (pollen <= 0) {
			failLevel ();
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

	public void catchSpeedBonus (Bonus bo)
	{
		bo.onCatched ();
		gui.robot.speedUp ();
	}

//	public void onBarrelTouch(Vector2 position){
//		pollen--;
//		gui.updateScores ();
//		gui.showHUDText (-ROCK_LOST_POINTS, position);
//		gui.showLostPollen (position);
//	}

	public void onGroundTouch (Vector2 position)
	{
		pollen--;
		if(scores < ROCK_LOST_POINTS){
			scores = 0;
		}else{
			scores -= ROCK_LOST_POINTS;
		}
		gui.updateScores ();
		gui.showHUDText (-ROCK_LOST_POINTS, position);
		gui.showLostPollen (position);
	}

	public void lostLife ()
	{
		lives--;
		gui.livesBar.removeLife ();
		if (lives == 0 || pollen == 0) {
			failLevel ();
		}else{
			needHeart = true;
		}
	}

	public Bonus.BonusType getBonusType ()
	{
		if (isLightnong) {
			StartCoroutine (lightningCoroutine ());
			isLightnong = false;
			return Bonus.BonusType.Lightning;
		}
		return Bonus.BonusType.Flower;
	}

	#region lightning
	Vector2 lSpawnPeriod;
	bool isLightnong = false;
	
	void setLightningPeriod ()
	{
		if (gui.levelSettings.ligtningMax > 0) {
			float period = States.State<StateGame> ().levelTime / (gui.levelSettings.ligtningMax + 1);
			lSpawnPeriod = new Vector2 (period - 2, period + 2);
			Debug.Log (lSpawnPeriod.ToString ());
			StartCoroutine (lightningCoroutine ());
		}
	}
	
	IEnumerator lightningCoroutine ()
	{
		float r = Random.Range (lSpawnPeriod.x, lSpawnPeriod.y);
		yield return new WaitForSeconds (r);
		isLightnong = true;
	}
	
	#endregion
}
