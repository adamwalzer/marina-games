using UnityEngine;
using System.Collections;

public class SpeedReward : Reward
{
	protected override void onTriggerAction(){
		States.State<StateGame>().catchSpeedBonus(this);
	}
}
