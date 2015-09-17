using UnityEngine;
using System.Collections;

public class IPoolable : MonoBehaviour 
{
	public bool _isBusyPoolable = false;

	public bool isBusyPoolable{
		get{
			return _isBusyPoolable;
		}
	}

	void OnEnable(){
		_isBusyPoolable = true;
	}

	void OnDisable(){
		_isBusyPoolable = false;
	}

	public void startPoolable(){
		_isBusyPoolable = true;
	}

	public void stopPoolable(){
		_isBusyPoolable = false;
	}
}
