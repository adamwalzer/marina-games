using UnityEngine;
using System.Collections;

public class Goose : Balloon 
{
	public override void onCatched ()
	{
		States.State<StateGame>().balloonCatched();
	}
}
