using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FailWindow : PopWindow 
{
	public Text scores;
	public Text time;

	public override void show(){
		time.text = "Time: " + States.State<StateGame>().getTimeResult;
		base.show();
	}

	public override void hide(){
		base.hide();
	}

	public void onReplayClick(){
		States.State<PStateFail>().onRestartClick();
	}

	public void onExitClick(){
		States.State<PStateFail>().onExitClick();
	}
}
