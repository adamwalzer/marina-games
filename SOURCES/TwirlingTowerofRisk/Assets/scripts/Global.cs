using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Global : MonoBehaviour
{
	public SoundManager soundManager;
	public Pool pool;
	private static Global _instance = null;
	private static bool applicationIsQuitting = false;
//	private int _levelsCount = 5;
	private int _totalScore;

	public static Global instance {
		get {
			if (applicationIsQuitting) {
				Debug.LogWarning ("[Singleton] Instance " + typeof(Global) +
					" already destroyed on application quit." +
					"Won't create again - returning null.");
				return null;
			}
			if (_instance == null) {
				GameObject prefab = (GameObject)Resources.Load ("GlobalObject", typeof(GameObject));
				GameObject globalInstance = (GameObject)GameObject.Instantiate (prefab);
				_instance = globalInstance.GetComponent<Global> ();
				_instance.init ();
			}
			
			return _instance;
		}
	}
	
	public void init ()
	{
		GameObject.DontDestroyOnLoad (this.gameObject);
		Application.runInBackground = true;
		Application.targetFrameRate = 50;

		
		_instance = this;
		
		GameObject sm = new GameObject ();
		DontDestroyOnLoad (sm);
		sm.transform.parent = transform;
		sm.name = "SoundManager";
		soundManager = sm.AddComponent<SoundManager> ();
		soundManager.initMusic(GetComponent<AudioSource>());

		GameObject p = new GameObject ();
		DontDestroyOnLoad (p);
		p.transform.parent = transform;
		p.transform.localPosition = Vector3.zero;
		p.name = "Pool";
		pool = sm.AddComponent<Pool> ();
	}
	
	void OnDestroy ()
	{
		if (this != _instance) {
			Debug.LogError ("Something went wrong |/");
		} else {
			applicationIsQuitting = true;
			if (_instance == this) {
				_instance = null;
			}
		}
	}
	
	void OnDisable ()
	{
		// Remove callback when object goes out of scope
		Application.RegisterLogCallback (null);
	}


//	public int levelsCount {
//		get {
//			return _levelsCount;
//		}
//	}

	public int totalScores{
		get{
			return _totalScore;
		}
		set{
			_totalScore = value;
		}
	}
	
	public static GameObject getRandomPrefab (FallingPrefs[] list, int summa)
	{
		int r = UnityEngine.Random.Range (0, summa);
		int sum = 0;
		for (int i = 0; i < list.Length; i++) {
			sum += list [i].weight;
			if (r < sum) {
				return list [i].prefab;
			}
		}
		return  list [0].prefab;
	}

	public static GameObject getRandomPrefab (List<FallingPrefs> list, int summa)
	{
		int r = UnityEngine.Random.Range (0, summa);
		int sum = 0;
		for (int i = 0; i < list.Count; i++) {
			sum += list [i].weight;
			if (r < sum) {
				return list [i].prefab;
			}
		}
		return  list [0].prefab;
	}
}

