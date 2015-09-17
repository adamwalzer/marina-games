using UnityEngine;
using System.Collections.Generic;

public class LevelSettings : MonoBehaviour 
{
	public int number;
	public float speedMode;
	public TerrainsList terrains;
	public Vector2 emptySpace;
	public float plantsChance;
	public Vector2 plantsDencity;
	public float bonusMode = 1;

	public float bonuseChance = 0.2f;

	public int levelLenght = 50;
	public float heartChance = 0.1f;
//	public float lightningChance = 0.1f;
	public bool hasBalloons = false;
	public float balloonChance = 0.1f;
	public bool hasGoose = false;
	public float gooseChance = 0.2f;

	public float fruitChance = 0.1f;
	public GameObject[] fruits;

	public int maxLightningsCount = 2;
}
