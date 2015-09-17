using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FailWindow : PopWindow 
{
	public Text scores;
	public Text time;
	bool readyToGo = false;

	public override void show(){
		time.text = States.State<StateGame>().getTimeResult;
		readyToGo = false; 
		base.show();
		gameObject.SetActive (true);
		StartCoroutine(wait());
	}

	public override void hide(){
		base.hide();
	}

	public void onReplayClick(){
		if(!readyToGo) return;
		States.State<PStateFail>().onRestartClick();
	}

	public void onExitClick(){
		if(!readyToGo) return;
		States.State<PStateFail>().onExitClick();
	}

	IEnumerator wait(){
		yield return new WaitForSeconds(0.5f);
		readyToGo = true;
	}
}
