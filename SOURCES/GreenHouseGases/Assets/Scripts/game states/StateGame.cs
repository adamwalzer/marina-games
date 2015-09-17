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
	public const int CATCHED_CARBON_POINTS = 25;
	int level = 1;
	bool _spawnCompleted = false;
	public int scores;
	public int levelTime = 0;
	GUIGame gui;
	bool domeIsFull = false;

	public override void OnEntered ()
	{
		GameObject go = GameObject.FindGameObjectWithTag ("MainObject");
		gui = go.GetComponent<GUIGame> ();
		Global.instance.soundManager = gui.soundManager;
		base.OnEnter ();
#if (UNITY_WEBPLAYER || UNITY_WEBGL) && !UNITY_EDITOR
		Application.ExternalCall ("OnUnityInited", 1);
#else
		level = 1;
		scores = 0;
		showSplashscreen ();
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
			showSplashscreen ();
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

	void showSplashscreen ()
	{
		States.Push<PSplashscreenState> ();
	}

	public void endLevel ()
	{
		States.Push<PStateEndLevel> ();
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
		gui.updateScores (scores);
		if (level <= Global.instance.levelsCount)
			restartLevel ();
	}

	void restartLevel ()
	{
		scores = 0;
		domeIsFull = false;
		gui.restart ();
	}

	public int currentLevel {
		get {
			return level;
		}
	}

	public void goToNextLevel ()
	{
		if (level == Global.instance.levelsCount) {
			States.Push<PStateWin> ();
			return;
		}
#if (UNITY_WEBPLAYER || UNITY_WEBGL) && !UNITY_EDITOR
		Application.ExternalCall ("GoToNextLevel", scores);
#else
		level++;
		Global.instance.totalScores += scores;
		startBriefing ();

#endif
	}

	public void completeGame ()
	{
#if (UNITY_WEBPLAYER || UNITY_WEBGL) && !UNITY_EDITOR
		Application.ExternalCall ("GameCompleted", scores);
#else
		Application.Quit ();

#endif
	}

	public void replayGame ()
	{
#if (UNITY_WEBPLAYER || UNITY_WEBGL) && !UNITY_EDITOR
		Application.ExternalCall ("ReplayGame", scores);
#else
		level = 1;
		showSplashscreen ();

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

	public void quitGame ()
	{
		#if (UNITY_WEBPLAYER || UNITY_WEBGL) && !UNITY_EDITOR
		Application.ExternalCall ("QuitGame", scores);
#else
		Application.Quit ();

#endif
	}

	public void failLevel ()
	{
		if (States.Current == States.State<StateGame> ()) {
			gui.stopImmidiately ();
			States.Push<PStateFail> ();
		}
	}

	public float currentSpeedMod {
		get {
			return gui.levelSettings.speedModif;
		}
	}

	public void addCarbon (int index)
	{
		scores += (int)(CATCHED_CARBON_POINTS * (index + 1));
		gui.currentSunFlower.onCarbon (index);
		gui.updateScores (scores);
	}

	public void addSmog (float index)
	{
		if (domeIsFull)
			return;
		gui.addSmoge ((int)(CATCHED_CARBON_POINTS * (index + 1)));
		Global.instance.soundManager.playSound (gui.domeSound);
	}

	public void onDomeFull ()
	{
		domeIsFull = true;
		failLevel ();
	}
}
