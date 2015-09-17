using UnityEngine;
using System.Collections;

public class MainMenuExampleState : BaseState {

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
		if (GUILayout.Button("Go to Battle")) {
			States.Change<BattleExampleState>();
		}
		if (GUILayout.Button("Settings")) {
			States.Push<SettingsExampleState>();
		}
	}
}
