﻿using UnityEngine;
using System.Collections;

public class Global 
{
	private static Global _instance;

	private int _levelsCount = 5;

	private SoundManager _soundManager;

	int _globalScores;

	public static Global instance{
		get{
			if(_instance == null){
				_instance = new Global();
			}
			return _instance;
		}
	}

	public int levelsCount{
		get{
			return _levelsCount;
		}set{
			_levelsCount = value;
		}
	}

	public int totalScores{
		get{
			return _globalScores;
		}set{
			_globalScores = value;
		}
	}

	public SoundManager soundManager{
		get{
			return _soundManager;
		}set{
			_soundManager = value;
		}
	}
}