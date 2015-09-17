using UnityEngine;
using System.Collections.Generic;

public class LevelSettings : MonoBehaviour 
{
	public int number;
	public GameObject hero;
	public int heroIndex;
	public GameObject pollenHero;
	public GameObject speedyHero;
	public float speedMode;
	public TerrainsList terrains;
	public TerrainsList flowers;
	public TerrainsList foliages;
	public float cloudsChance;

	public int pollinateUnit = 1;
	public int pollenMeterMax = 50;

	public float flowerChance = 0.2f;
	public float foliageChance = 0f;
	
//	public int levelDuration = 30;

	public float heartChance = 0.1f;

	public float brickChance = 0.2f;
	public float boxesCnance = 0.2f;
	public float wagonChance = 0.1f;
	public float barrelChance = 0.1f;

	public float birdChance = 0.1f;

	public int ligtningMax = 1;

	public bool isDay;
}
