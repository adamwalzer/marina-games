using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FailWindow : PopWindow 
{
	public Text resultText;
	public Text questText;
	public AudioClip backgroundMusic;



	public void show(float result, float quest){
		base.show();
		resultText.text = result.ToString("0.00");
		questText.text = quest.ToString("0.00");
		Global.instance.soundManager.stopMusic(backgroundMusic);
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
