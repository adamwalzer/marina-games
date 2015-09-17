using UnityEngine;
using System.Collections;

public class SettingsExampleState : BaseState {

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
		if (GUILayout.Button("Back")) {
			States.Pop();
		}
	}
}
