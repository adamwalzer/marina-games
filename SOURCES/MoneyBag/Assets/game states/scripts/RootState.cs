using UnityEngine;
using System.Collections;

// Root state, shold`t be current
public class RootState : BaseState
{
	public override void OnUpdate()
	{
		throw new System.Exception("Root not allowed to be current!");
	}	
}
