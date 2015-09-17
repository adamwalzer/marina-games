using UnityEngine;
using System.Collections;

// Copyright (c) 2011-2013 MihaPro

public class StatesStarter : MonoBehaviour
{
	public States statesPrefab;
	public string startStateType; // state for this scene, if started from editor

	protected virtual void Start()
	{
		Init();
	}

	void Init()
	{
		if(statesPrefab == null)
		{
			throw new System.Exception("States Prefab in StatesStarter not setted");
		}
		
		if(string.IsNullOrEmpty(startStateType))
		{
			throw new System.Exception("startStateType in StatesStarter not setted");
		}
		
		States states = GameObject.FindObjectOfType(typeof(States)) as States;
		States.PrepareAutoStartState(startStateType);
		if(states == null)
		{
			States.Spawn(statesPrefab);
		}

		// delete spawner, we don`t need it
		Destroy(gameObject);
	}
}
