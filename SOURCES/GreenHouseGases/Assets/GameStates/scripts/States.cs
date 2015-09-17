using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Copyright (c) 2011-2013 MihaPro

// States API
public class States: MonoBehaviour
{
	public bool debugLog;
	public bool callOnUpdate = true;
	private Transform popupRoot;
	private RootState root;
	private BaseState current;
	//private BaseState[] statesArray;
	private Dictionary<System.Type, BaseState> states = new Dictionary<System.Type, BaseState>();
	private BaseState[] popupStates;
	private bool inited = false;
	private static States instance; // singletorn
	private static string autoStartState;
	
	public static States Instance
	{
		get { return instance; }
	}

	public static BaseState Current
	{
		get { return instance.current; }
	}

	public static T State<T>() where T:BaseState
	{
		System.Type type = typeof(T);
		T s = instance.GetStateForType(type) as T;
		if(s)
		{
			return s;
		}
		// search in popup
		s = instance.FindPopupState(type) as T;
		return s;
	}
	
	public static bool IsInited()
	{
		return instance != null;
	}

	public static void PrepareAutoStartState(string startStateType)
	{
		autoStartState = startStateType;
	}

	// manualy update current state
	public static void UpdateCurrentState()
	{
		if(instance.current)
		{
			instance.current.OnUpdate();
		}
	}
	
	public static bool IsLogEnabled()
	{
		return instance && instance.debugLog;
	}
	
	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		States.Init();
	}
	
	static void Init()
	{
		if(instance != null)
		{
			throw new System.Exception("States allready inited!");
		}
		
		instance = GameObject.FindObjectOfType(typeof(States)) as States;
		if(instance == null)
		{
			throw new System.Exception("States not found in scene!");
		}
		
		instance.InitStates();
		
	}

	public static void Change<TState>(bool forceReload) where TState : BaseState
	{
		instance.ChangeState(typeof(TState), forceReload);
	}
	
	public static void Change<TState>() where TState : BaseState
	{
		instance.ChangeState(typeof(TState), false);
	}
	
	public static void Change(BaseState state)
	{
		instance.ChangeState(state.GetType(), false);
	}
	
	public static void Push<TState>() where TState : BaseState
	{
		instance.PushState(typeof(TState));
	}
	public static void Push(BaseState state) {
		instance.PushState(state.GetType());
	}

	public static void Pop()
	{
		instance.PopState();
	}
	
	private bool LoadStateScene(BaseState stateToLoad, bool forceReload)
	{
		if(stateToLoad is LoadSceneState || string.IsNullOrEmpty(stateToLoad.stateScene) || Application.loadedLevelName == stateToLoad.stateScene && !forceReload)
		{
			return false;
		}

		if (stateToLoad.loadingState == null) {
			Application.LoadLevel(stateToLoad.stateScene);
			return false;
		} else {
			LoadSceneState ls = stateToLoad.loadingState as LoadSceneState;
			ls.stateToLoad = stateToLoad;
			ChangeState(ls, false);
			return true;
		}
	}

	public static void Spawn(States prefab)
	{
		if(States.IsInited() == false)
		{
			States states = Instantiate(prefab) as States;
			
			states.name = states.name.Substring(0, states.name.IndexOf('(')); // remove clone word
		}
	}
	
	void ChangeState(string stateType)
	{
		// if empty state name, return
		if(string.IsNullOrEmpty(stateType))
		{
			return;
		}
		
		// find type
#if UNITY_WEBGL
		System.Type type = Types.GetType(stateType, "Assembly-CSharp");
#else
		System.Type type = System.Type.GetType(stateType, false);
#endif

		if(type == null)
		{
			Debug.LogError(string.Format("[States] Type for state \"{0}\" not found!", stateType));
			return;
		}
		
		// change state
		ChangeState(type, false);
	}
	
	void OnEnable()
	{
		// restore singleton
		instance = this;
	}

	private void InitStates()
	{
		if(inited)
		{
			return;
		}
		
		inited = true;
		
		FindRoot();
		
		BaseState[] statesArray = root.GetComponentsInChildren<BaseState>(true);
		
		// init dictionary
		for(int i = 0; i < statesArray.Length; i++)
		{
			BaseState s = statesArray[i];
			System.Type t = s.GetType();
			
			if(states.ContainsKey(t))
			{
				throw new System.Exception("Dublicates states with Type: " + t + ". Use State with parameters or Popup state.");
			}
			
			states.Add(t, s);
			SetStateActive(s, false, false); // disable all
		}
		
		// find pop states
		
		popupRoot = transform.Find("PopupStates");
		if(popupRoot == null)
		{
			throw new System.Exception("PopupStates child not found");
		}
		popupStates = popupRoot.GetComponentsInChildren<BaseState>(true);
		popupRoot.gameObject.SetActive(false);
		for(int i = 0; i < popupStates.Length; i++)
		{
			BaseState s = popupStates[i];
			s.isPopupState = true;
			SetStateActive(s, false, false);
		}

		SetCurrent(root);
		current.OnEnter();
		
		SetStartState();
	}

	void SetStartState()
	{
		if(string.IsNullOrEmpty(autoStartState))
		{
			// find States Starter
			StatesStarter ss = FindObjectOfType(typeof(StatesStarter)) as  StatesStarter;
			if(ss == null)			
			{
				throw new System.Exception("Use StatesStarter for set Start state");
			}
			autoStartState = ss.startStateType;
		}
		
		if(!string.IsNullOrEmpty(autoStartState))
		{
			AutoStartState();
			autoStartState = null;
		}
	}
	
	private void AutoStartState()
	{
		if(!(current is RootState))
		{
			return;
		}
		
		if(!string.IsNullOrEmpty(autoStartState))
		{
			if(States.IsLogEnabled())
			{
				Debug.Log(string.Format("[States] Start state: {0}", autoStartState));
			}

			ChangeState(autoStartState);
		}
	}

	void SetCurrent(BaseState state)
	{
		current = state;
		SetStateActive(current, true, true);
		SetCurrentTagName(current, true);
	}

	void FindRoot()
	{
		root = GetComponentInChildren<RootState>();
		if(root == null)
		{
			throw new System.Exception("Root not found in " + name);
		}
	}

	protected void ChangeState(System.Type stateType, bool forceReload) {
		ChangeState(FindState(stateType), forceReload);
	}

	private void ChangeState(BaseState state, bool forceReload) {

		// if need laoding state scene
		if(LoadStateScene(state, forceReload))
			return;

		VerifyNestedCall();
		
		try
		{
			if(state == current)
			{
				return;
			}
		
			if(state.isPopupState)
			{
				throw new System.Exception(state + " is popup");
			}
		
			/*
			if(current == null || current.IsConnectedState(state))
			{
				Push(state);
				state_changing = false;
				return;
			}
			*/

			if(ChangeStackToState(state))
			{
				return;
			}
		}
		finally
		{
			state_changing = false;
			current.OnEntered();
		}
		
		throw new System.Exception("Cannot Change to " + state);
	}

	protected void PushState(System.Type stateType)
	{
		VerifyNestedCall();
		
		try
		{
			BaseState state = FindPopupState(stateType);
			if(state.isPopupState == false)
			{
				throw new System.Exception(state + " is not popup");
			}
			if(state.transform.parent != popupRoot)
			{
			}
		
			Push(state, true);
		}
		finally
		{
			state_changing = false;
			current.OnEntered();
		}
	}

	protected BaseState FindPopupState(System.Type stateType)
	{
		for(int i = 0; i < popupStates.Length; i++)
		{
			BaseState s = popupStates[i];
			if(s.GetType() == stateType)
			{
				return s;
			}
		}
		throw new System.Exception("Popup State < " + stateType + " > not found");
	}

	private void Push(BaseState state, bool pause_current)
	{
		BaseState old = current;
		if(current != null && pause_current)
		{
			current.OnPaused();
			SetCurrentTagName(current, false);
			SetStateActive(current, true, false); // pause state
		}
		if(state.isPopupState)
		{
			//TOTO: не забыть возвращять стейт в popupRoot
			state.transform.parent = old.transform; // attach to prev state
		}
		SetCurrent(state);
		current.OnEnter();
	}

	protected void PopState()
	{
		VerifyNestedCall();
		
		try
		{
			FlushTopState();
		
			// enable current state
			SetCurrent(current);
			current.OnResume();
		}
		finally
		{
			state_changing = false;
		}
	}

	private void FlushTopState()
	{
		current.OnExit();
		SetCurrentTagName(current, false);
		SetStateActive(current, false, false); // pause state
		
		BaseState parent = current.Parent;
		
		if(current.isPopupState)
		{
			// return to popup root
			current.transform.parent = popupRoot;
		}
		
		current = parent;
	}

	bool ChangeStackToState(BaseState state)
	{
		BaseState to_state = current.FindParentStateInStack(state);
		bool connected_to_current = to_state == current;
		if(to_state != null)
		{
			while(current != to_state)
			{
				FlushTopState();
			}
			
			// if connected state and not in stack
			if(current != state)
			{
				Push(state, connected_to_current);
			}
			else
			{
				// just return to paused state
				SetCurrent(current);
				current.OnResume();
			}
			
			return true;
		}
		return false;
	}
	
	private BaseState GetStateForType(System.Type stateType)
	{
		BaseState s = null;
		states.TryGetValue(stateType, out s);
		return s;
	}

	private BaseState FindState(System.Type stateType)
	{
		BaseState s = GetStateForType(stateType);
		if(s == null)
		{
			state_changing = false;
			throw new System.Exception("State < " + stateType + " > not found");
		}
		return s;
	}

	public const string CURRENT_TAGNAME = " <Current>";

	private static void SetCurrentTagName(BaseState state, bool flag)
	{
		//set CURRENT_TAGNAME
		int p = state.name.IndexOf(CURRENT_TAGNAME);
		if(flag)
		{
			// add tag
			if(p == -1)
			{
				state.name += CURRENT_TAGNAME;
			}
		}
		else
		{
			// remove tag
			if(p > 0)
			{
				state.name = state.name.Substring(0, p);
			}
		}
	}
	
	private static void SetStateActive(BaseState state, bool activeFlag, bool enableState)
	{
		state.gameObject.SetActive(activeFlag);
		state.enabled = enableState;
	}

	private bool state_changing = false;

	private void VerifyNestedCall()
	{
		// check
		if(state_changing)
		{
			throw new System.Exception("Nested call in state changing!");
		}
		state_changing = true;
	}
	
	void Update()
	{
		if(callOnUpdate && current)
		{
			current.OnUpdate();
		}
	}
}

