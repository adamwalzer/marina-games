using UnityEngine;
using System.Collections;

public class PStateBackground : BaseState {
	PGUIBackground gui;

	public override void OnEntered(){
		gui = GameObject.FindObjectOfType<PGUIBackground> ();
		base.OnEntered ();
		gui.background.SetActive (true);
		gui.buttons.SetActive (false);
//		Global.instance.soundManager.muteSound = true;
		Shark[] sharks = GameObject.FindObjectsOfType<Shark> ();
		foreach (Shark sh in sharks) {
			Destroy(sh.gameObject);
		}
	}

	public override void OnExit ()
	{
		gui.background.SetActive (false);
//		Global.instance.soundManager.muteSound = false;
		base.OnExit ();
	}
}
