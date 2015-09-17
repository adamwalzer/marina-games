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
	
	const float MONEYBAG_COST = 1f;

	GameMode mode;
	int level = 1;
	bool _spawnCompleted = false;
	int levelDuration = 90;
	float _result = 0;
	GUIGame gui;
	float mistakeCost;

	public override void OnEntered ()
	{
		if (gui == null) {
			gui = GameObject.FindObjectOfType<GUIGame> ();
		}
		base.OnEnter ();
#if UNITY_WEBPLAYER && !UNITY_EDITOR
		Application.ExternalCall ("OnUnityInited", 1);
#else
		level = 1;
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
//		Global.instance.totalScores = s;
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
		Global.instance.soundManager.stopMusic (gui.backgroundMusic);
	}

	void showSplashScreen ()
	{
		States.Push<PSplashScreenState> ();
	}

	public void endSpawn ()
	{
		gui.stopImmidiately ();
	}

	public void endLevel ()
	{
		gui.tapController.stop ();
		if ((gui.levelSettings.type == RewardTypes.RewardType.None && Mathf.Abs(_result - gui.levelSettings.quest) <= 0.005f) || 
		    (gui.levelSettings.type != RewardTypes.RewardType.None && _result > (gui.levelSettings.quest - 0.005f))) {
			if (currentLevel < gui.levels.Count) {
				States.Push<PStateEndLevel> ();
			} else {
				States.Push<PStateWin> ();
			}
		} else {
			failLevel ();
		}

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
		StopAllCoroutines ();
		if (level <= gui.levels.Count)
			restartLevel ();
	}

	public float result {
		get {
			return _result;
		}
	}

	public LevelSettings levelSettings {
		get {
			return gui.levelSettings;
		}
	}

	void restartLevel ()
	{
		Global.instance.soundManager.muteMusic = false;
		_result = 0;
		gui.restart ();
		mistakeCost = gui.levelSettings.mistakeValue;
	}

	public int currentLevel {
		get {
			return level;
		}
	}

	public int levelTime {
		get {
#if UNITY_WEBPLAYER && !UNITY_EDITOR
			return levelDuration;
#else
			return settings.levelTime;
#endif
		}
	}

	public string getTimeResult {
		get {
			return gui.getTimeString;
		}
	}

	public void goToNextLevel ()
	{
#if UNITY_WEBPLAYER && !UNITY_EDITOR
		Application.ExternalCall ("GoToNextLevel", 0);
#else
		level++;
//		Global.instance.totalScores += scores;
		startBriefing ();
#endif
	}

//	public void completeGame ()
//	{
//#if UNITY_WEBPLAYER && !UNITY_EDITOR
//		Application.ExternalCall ("GameCompleted", 0);
//#else
//		States.Change<StateMainMenu> ();
//#endif
//	}

	public void exitGame ()
	{
#if UNITY_WEBPLAYER && !UNITY_EDITOR
		Application.ExternalCall ("ExitGame", 0);
#else
		States.Change<StateMainMenu> ();
#endif
	}

	public void replayGame ()
	{
#if UNITY_WEBPLAYER && !UNITY_EDITOR
		Application.ExternalCall ("ReplayGame", 0);
#else
		level = 1;
		startBriefing ();
#endif

//		scores = 0;
	}

	public void onCloseClick ()
	{
		States.Push<PStatePause> ();
	}

	public void replayLevel ()
	{

#if UNITY_WEBPLAYER && !UNITY_EDITOR
		Application.ExternalCall ("ReplayLevel", 0);
#else
		startBriefing ();
#endif
//		scores = 0;
	}

	public void failLevel ()
	{
		if (States.Current == States.State<StateGame> ()) {
			gui.stopImmidiately ();
			States.Push<PStateFail> ();
		}
	}

	public LevelSettings settings {
		get {
			if (gui == null) {
				gui = GameObject.FindObjectOfType<GUIGame> ();
			}
			return gui.levelSettings;
		}
	}

	public void catchReward (Reward rew)
	{
		bool isRight = (gui.levelSettings.type == RewardTypes.RewardType.None || gui.levelSettings.type == rew.type);
		if (isRight) {
			_result += rew.value;
			gui.showText("+" + rew.value.ToString("$0.00"), true, rew.transform.position);
		} else {
			_result += mistakeCost;
			gui.showText(mistakeCost.ToString("$0.00"), false, rew.transform.position);
		}
		if (_result < 0) {
			_result = 0;
		}
		gui.updateScores (_result);
		rew.onCatched ();

		if(gui.levelSettings.type == RewardTypes.RewardType.None && _result - gui.levelSettings.quest > 0.005f){
			Debug.Log(_result + "    " + gui.levelSettings.quest);
			failLevel();
		}
	}

	public void catchObstacle (Obstacle obs)
	{
		_result += mistakeCost;
		if (_result < 0) {
			_result = 0;
		}
		gui.showText(gui.levelSettings.mistakeValue.ToString("$0.00"), false, obs.transform.position);
		gui.updateScores (_result);
		obs.onCatched ();
	}

	public void onLightningCatched ()
	{
		gui.spawner.boostSpawn ();
	}

	public void onMoneyBagCatched (Vector2 pos)
	{
		_result += MONEYBAG_COST;
		gui.showText(MONEYBAG_COST.ToString("$0.00"), true, pos);
		gui.updateScores (_result);
	}
}
