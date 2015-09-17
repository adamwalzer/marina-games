using UnityEngine;
using System.Collections.Generic;

public class Obstacle : IClearable
{
//	public GameObject[] flowersPatterns;
//	public GameObject[] foliagePrefabs;
	public GameObject lightningPrefab;
	public BonusePlace[] flowerPlaces;
	public BonusePlace[] foliagePlaces;
	public GameObject[] clouds;
	public GameObject[] bricks;
	public GameObject[] boxes;
	public HeartGate gate;
	public Barrel[] barrels;
	public Barrel[] wagons;
	public BonusePlace[] specialPlaces;
	public float width;
	public GameObject sparrowPrefab;
	public GameObject owlPrefab;
	LevelSettings _settings;
	Transform _camera;
	float _invisDistance = 20f;
	bool isViewed = false;
	GameObject bird;
//	List<BonusPattern> patterns;
//	List<GameObject> flowers;
//	List<GameObject> foliages;
	
	void Update ()
	{
		if (!isViewed)
			return;
		if (transform.position.x < _camera.position.x - _invisDistance) {
			deinit ();
			
		}
	}
	
	public void init (LevelSettings settings_, Bonus.BonusType type)
	{
		_settings = settings_;
		_camera = Camera.main.transform;
		gameObject.SetActive (true);
		isViewed = true;
		if(_settings.isDay){
			addPlants ();
		}
		if (transform.position.x > 5) {
			addFlowers (_settings.flowers.list, flowerPlaces, _settings.flowerChance, false);
			if(_settings.foliageChance > 0){
				addFlowers(_settings.foliages.list, foliagePlaces, _settings.foliageChance, true);
			}
			if (_settings.brickChance > 0 && bricks.Length > 0) 
			{
				addObst (bricks, _settings.brickChance);
			}
			if(_settings.boxesCnance > 0 && boxes.Length > 0){
				addObst(boxes, _settings.boxesCnance);
			}
			if(transform.position.x > 20){
				if(_settings.barrelChance > 0 && barrels.Length > 0){
					addBarrels();
				}
				if(_settings.wagonChance > 0 && wagons.Length > 0){
					addWagon();
				}
				if(gate != null && Random.value <= _settings.heartChance && States.State<StateGame>().damaged){
					gate.init();
				}else{
					gate.deinit();
				}
			}
			addBird();
		}	
		if(type == Bonus.BonusType.Lightning){
			addBonus();
		}
	}
	
	public override void deinit ()
	{
		isViewed = false;
		
		gameObject.SetActive (false);
		foreach (GameObject cl in clouds) {
			cl.SetActive (false);
		}
		foreach (GameObject br in bricks) {
			br.SetActive (false);
		}
		foreach (GameObject bo in boxes) {
			bo.SetActive (false);
		}
		foreach(Barrel ba in barrels){
			ba.deinit();
		}
		foreach(Barrel w in wagons){
			w.deinit();
		}
//		if(patterns != null){
//			foreach(BonusPattern bP in patterns){
//				bP.deinit();
//			}
//			patterns.Clear();
//		}
//		if(flowers != null){
//			foreach(GameObject go in flowers){
//				go.GetComponent<Flower>().deinit();
//			}
//			flowers.Clear();
//		}
//		if(foliages != null){
//			foreach(GameObject go in foliages){
//				go.GetComponent<FlowerFoliage>().deinit();
//			}
//			foliages.Clear();
//		}

		if(gate != null){
			gate.deinit();
		}
		if(bird != null){
			bird.GetComponent<Goose>().deinit();
		}
		bird = null;
	}

	void addFlowers (GameObject[] list, BonusePlace[] places, float chance, bool isFoliage)
	{
		List<BonusePlace> arr = new List<BonusePlace> (places);
//		tempList = new List<GameObject>();
//		patterns = new List<BonusPattern>();
		for (int i = 0; i < places.Length; i++) {
			if (Random.value <= chance) {
				int r = Random.Range (0, arr.Count);
				Vector2 v = arr [r].getPoint ();
				arr.RemoveAt (r);
				GameObject go = Global.instance.pool.get (list [Random.Range (0, list.Length)], transform.position);
				go.transform.position = v;
				go.transform.parent = transform.parent;
				BonusPattern s = go.GetComponent<BonusPattern> ();
				if(isFoliage){
					s.initFoliage(_settings);
				}else{
					s.initFlower (_settings);
				}
//				chance *= 0.5f;
//				patterns.Add(s);
			}
		}
	}

	void addObst (GameObject[] list, float chance)
	{
		foreach(GameObject g in list){
			if (Random.value <= chance) {
				g.SetActive (true);
			}
		}

	}
	
	void addPlants ()
	{
		foreach (GameObject p in clouds) {
			if (UnityEngine.Random.value <= _settings.cloudsChance) {
				p.SetActive (true);
			}
		}
	}

	void addBonus(){
		GameObject go = Global.instance.pool.get(lightningPrefab, transform.position);
		Bonus b = go.GetComponent<Bonus>();
		b.transform.parent = transform.parent;
		b.transform.position = specialPlaces[Random.Range(0, specialPlaces.Length)].getPoint();
		b.init();
		
	}

	void addBarrels(){
		foreach(Barrel ba in barrels){
			if(Random.value <= _settings.barrelChance){
				ba.init();
			}
		}
	}

	void addWagon(){
		if(Random.value <= _settings.wagonChance){
			wagons[Random.Range(0, wagons.Length)].init();
		}
	}

//	void addGate(){
//		if(Random.value <= _settings.heartChance){
//			gate.init();
//		}else{
//			gate.deinit();
//		}
//	}

	void addBird(){
		if(Random.value <= _settings.birdChance){
			Vector2 pos  = transform.position;
			pos.x -= width;
			pos.y = Random.Range(-2f, 2f);
			if(_settings.isDay){
				bird = Global.instance.pool.get(sparrowPrefab, pos);
			}else{
				bird = Global.instance.pool.get(owlPrefab, pos);
			}
			bird.transform.parent = transform.parent;
			bird.GetComponent<Goose>().init();
		}
	}
}
