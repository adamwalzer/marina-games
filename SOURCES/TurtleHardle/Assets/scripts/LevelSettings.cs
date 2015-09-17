using UnityEngine;
using System.Collections.Generic;

public class LevelSettings : MonoBehaviour 
{
	public int number;
	public TerrainsList terrains;
	public Vector2 emptySpace;
	public float plantsChance;
	public Vector2 plantsDencity;
	public float bonusMode = 1;

	public float rewardChance = 0.2f;

//	public int levelLenght = 50;
	public float heartChance = 0.1f;
	public bool hasShark = false;
	public float sharkChance = 0.2f;

	public FallingPrefs[] fallingItems;
	public GameObject lifeBonus;
	public GameObject speedBonus;

	public int maxSpeedCount = 2;
}
