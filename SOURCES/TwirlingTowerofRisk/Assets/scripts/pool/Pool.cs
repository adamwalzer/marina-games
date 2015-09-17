using UnityEngine;
using System.Collections.Generic;

public class Pool : MonoBehaviour
{
	public Dictionary<GameObject, PoolItem> pool;

	public GameObject get(GameObject item, Vector3 position)
	{
		if(pool == null){
			pool = new Dictionary<GameObject, PoolItem>();
		}
		PoolItem result;
		if(pool.TryGetValue(item, out result)){
		}else{
			result = new PoolItem(item);
			pool.Add(item, result);
		}
		checkUnusedItems();
		return result.take(position);
	}

	void checkUnusedItems ()
	{
		List<GameObject> toDelete = new List<GameObject>();
		foreach(KeyValuePair<GameObject, PoolItem> pair in pool) {
			if (pair.Value.usageTime < Time.time - 1 * 60) {
				toDelete.Add(pair.Key);
			}
		}
		foreach(GameObject go in toDelete) {
			pool.Remove(go);
		}
	}
}
