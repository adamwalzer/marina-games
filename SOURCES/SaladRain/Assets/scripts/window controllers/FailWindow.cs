using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FailWindow : PopWindow 
{
	public override void show(){
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
