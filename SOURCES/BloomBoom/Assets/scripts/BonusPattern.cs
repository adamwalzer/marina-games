using UnityEngine;
using System.Collections.Generic;

public class BonusPattern : IClearable
{
	public GameObject[] flowerPrefabs;

	public Transform[] places;
	protected bool isWorking = false;
	protected Transform came;
	protected float invisDistance = 10f;
	protected LevelSettings settings;

	List<GameObject> folliage;
	List<GameObject> flowers;


	void Update ()
	{
		if (!isWorking)
			return;
		if (came == null) {
			came = Camera.main.transform;
		}
		if (came.position.x > transform.position.x + invisDistance) {
			deinit ();
		}
	}

	public void initFlower (LevelSettings settings_)
	{
		settings = settings_;
		flowers = new List<GameObject>();
		foreach (Transform t in places) {
			GameObject bo = Instantiate(getBonus()) as GameObject;
			bo.transform.parent = transform;
			bo.transform.position = t.position;
			Flower f = bo.GetComponent<Flower>();
			f.init(settings);
			flowers.Add(bo);
		}
		gameObject.SetActive (true);
		isWorking = true;
	}

	public void initFoliage (LevelSettings settings_)
	{
		settings = settings_;
		folliage = new List<GameObject>();
		foreach (Transform t in places) {
//			GameObject bo = Global.instance.pool.get(getBonus(), t.position);
			GameObject bo = Instantiate(getBonus()) as GameObject;
			bo.transform.parent = transform;
			bo.transform.position = t.position;
			FlowerFoliage ff = bo.GetComponent<FlowerFoliage>();
			ff.init(settings);
			folliage.Add(bo);
		}
		gameObject.SetActive (true);
		isWorking = true;
	}

	public override void deinit ()
	{
		if(flowers != null && flowers.Count > 0){
			foreach(GameObject f in flowers){
				Destroy(f);
			}
			flowers.Clear();
		}
		if(folliage != null && folliage.Count > 0){
			foreach(GameObject f in folliage){
				Destroy(f);
			}
			folliage.Clear();
		}
		gameObject.SetActive (false);
		isWorking = false;
	}

	protected virtual GameObject getBonus ()
	{
		return flowerPrefabs[Random.Range(0, flowerPrefabs.Length)];
	}
}
