using UnityEngine;
using System.Collections;

public class BattleExampleState : BaseState {

	public override void OnEnter() {
		base.OnEnter();
	}

	public override void OnEntered() {
		base.OnEntered();
	}

	public override void OnExit() {
		base.OnExit();
	}

	void OnGUI() {
		if (GUILayout.Button("Back to Main Menu")) {
			States.Change<MainMenuExampleState>();
		}
		if (GUILayout.Button("Settings")) {
			States.Push<SettingsExampleState>();
		}
	}
}
