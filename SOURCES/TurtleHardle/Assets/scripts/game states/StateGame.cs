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
	bool needLife = false;
	int sharksNear = 0;

	public override void OnEntered ()
	{
		if(gui == null){
			gui = GameObject.FindObjectOfType<GUIGame> ();
		}
		base.OnEnter ();
#if UNITY_WEBPLAYER && !UNITY_EDITOR
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
		scores = Global.instance.totalScores;
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

	public void JSCallFromPageCompleteGame ()
	{
		States.Push<PStateWin> ();
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
		scores = scores = Global.instance.totalScores;
		gui.updateScores (scores);
		StopAllCoroutines();
		isSpeed = false;
		if (level <= gui.levels.Count)
			restartLevel ();
	}

	void restartLevel ()
	{
//		scores = scores = Global.instance.totalScores;
		sharksNear = 0;
		Global.instance.soundManager.muteMusic = false;
		gui.restart ();
		setSpeedPeriod ();
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

	public void changeShark(bool add){
		if(add){
			if(sharksNear == 0){			
				Global.instance.soundManager.muteMusic = true;
			}
			sharksNear++;
		}else{
			sharksNear--;
			if(sharksNear <= 0){
				sharksNear = 0;
				Global.instance.soundManager.muteMusic = false;
			}
		}
	}

	public void goToNextLevel ()
	{
#if UNITY_WEBPLAYER && !UNITY_EDITOR
		Application.ExternalCall ("GoToNextLevel", scores);
#else
		level++;
		Global.instance.totalScores += scores;
		startBriefing();
#endif
	}

	public void exitGame (bool addScores)
	{
#if UNITY_WEBPLAYER && !UNITY_EDITOR
//		States.Push<PStateBackground>();
		Application.ExternalCall ("ExitGame", addScores? scores : 0);
#else

		States.Change<StateMainMenu>();
#endif
	}

	public void replayGame ()
	{
#if UNITY_WEBPLAYER && !UNITY_EDITOR
		Application.ExternalCall ("ReplayGame", scores);
#else
		level = 1;
		startBriefing ();
#endif

		scores = Global.instance.totalScores;
	}

	public void onCloseClick(){
		States.Push<PStatePause>();
	}

	public void replayLevel ()
	{

#if UNITY_WEBPLAYER && !UNITY_EDITOR
		Application.ExternalCall ("ReplayLevel", scores);
#else
		startBriefing ();
#endif
		scores = scores = Global.instance.totalScores;
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
			if(needLife){
				if(lives < MAX_LIVES_COUNT){
					needLife = false;
					return true;
				}else{
					return false;
				}
			}else{
				return false;
			}

		}
	}

	public void catchSpeedBonus (Reward re)
	{
		re.onCatched ();
		gui.butterfly.speedUp ();
	}

	public void catchLifeBonus (Reward re)
	{
		re.onCatched ();
		if (lives < MAX_LIVES_COUNT) {
			lives++;
			gui.livesBar.addLife ();
		}
	}

	public void catchReward (Reward fr)
	{
		fr.onCatched ();
		scores += fr.reward;
		if(scores < 0){
			scores = 0;
		}
		gui.updateScores (scores);
	}

	public void dangareousItemCatched ()
	{
		lives--;
		gui.livesBar.removeLife ();
		if (lives > 0) {
			gui.butterfly.onDamage ();
			needLife = true;
		} else {
			gui.butterfly.onKilled ();
			failLevel ();
		}
	}

	public Reward.RewardType getBonusType ()
	{
		if (Random.value < gui.levelSettings.heartChance && damaged) {
			return Reward.RewardType.Life;
		}
		if (isSpeed) {
			StartCoroutine (speedSpawnCoroutine ());
			isSpeed = false;
			return Reward.RewardType.Speed;
		}
		return Reward.RewardType.Points;
	}

	#region lightning
	Vector2 lSpawnPeriod;
	bool isSpeed = false;

	void setSpeedPeriod ()
	{
		float period = States.State<StateGame>().levelTime / (gui.levelSettings.maxSpeedCount + 1);
		lSpawnPeriod = new Vector2 (period + 1, period + 3);
		StartCoroutine (speedSpawnCoroutine ());
	}

	IEnumerator speedSpawnCoroutine ()
	{
		float r = Random.Range (lSpawnPeriod.x, lSpawnPeriod.y);
		yield return new WaitForSeconds (r);
		isSpeed = true;
	}

	#endregion
}
