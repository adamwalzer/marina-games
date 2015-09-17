using UnityEngine;
using System.Collections.Generic;

public class PoolItem 
{
	public List<IPoolable> items;
	public float usageTime;
	GameObject _prefab;

	public PoolItem(GameObject prefab_){
		usageTime = Time.time;
		_prefab = prefab_;
		items = new List<IPoolable>();
	}

	public GameObject take(Vector3 position){
		usageTime = Time.time;
		GameObject result;
		IPoolable poolable;
		poolable = items.Find(item => !item.isBusyPoolable);
		if(poolable == null){
			result = GameObject.Instantiate(_prefab, position, Quaternion.identity) as GameObject;
			poolable = result.GetComponent<IPoolable>();
			items.Add(poolable);
		}else{
			result = poolable.gameObject;
			result.transform.position = position;
		}
		poolable.startPoolable();
		return result;
	}
}
