using UnityEngine;
using System.Collections;

public class LifeReward : Reward
{
	protected override void onTriggerAction(){
		States.State<StateGame>().catchLifeBonus(this);
	}
}
