using UnityEngine;
using System.Collections;

// Copyright (c) 2011-2013 MihaPro

// base state for all States
public class BaseState : MonoBehaviour
{
	public string stateScene; // scene to load before activate state
	public LoadSceneState loadingState;
	
	[HideInInspector]
	public bool isPopupState; 	// state can be popup

	// parent state in hierarchy
	public BaseState Parent
	{
		get { return transform.parent ? transform.parent.GetComponent<BaseState>() : null; }
	}
	
	public bool IsCurrent
	{
		get {return States.Current == this; }
	}

	private static void LogState(BaseState state, string message)
	{
		if(States.IsLogEnabled())
		{
			Debug.Log(string.Format("[States] \t{0}: {1}", message, state));
		}
	}

	public BaseState FindParentStateInStack(BaseState state)
	{
		if(IsConnectedState(state))
		{
			return this;
		}
		
		BaseState p = this.Parent;
		if(p == state)
		{
			return p;
		}
		
		return p != null ? p.FindParentStateInStack(state) : null;
	}

	public bool IsConnectedState(BaseState state)
	{
		foreach(Transform t in transform)
		{
			BaseState s = t.GetComponent<BaseState>();
			if(s == state)
			{
				return true;
			}
		}
		return false;
	}
	
	public static BaseState FindChildState(Transform root, string name)
	{
		Transform child = root.Find(name);
		return child ? child.GetComponent<BaseState>() : null;
	}
	
	// state functions!
	public virtual void OnEnter()
	{
		LogState(this, "ENTER");
	}
	public virtual void OnEntered()
	{
		LogState(this, "ENTERED");
	}

	public virtual void OnExit()
	{
		LogState(this, "EXIT");
	}

	public virtual void OnPaused()
	{
		LogState(this, "PAUSED");
	}

	public virtual void OnResume()
	{
		LogState(this, "RESUME");
	}

	public virtual void OnUpdate()
	{
		//LogState(this, "UPDATE");
	}
	
	public override string ToString()
	{
		int indexCurTag = name.IndexOf(States.CURRENT_TAGNAME);
		string sname = indexCurTag != -1 ? name.Substring(0, indexCurTag) : name;
		return (string.Format("{0} ({1})", sname, GetType()));
	}
}
