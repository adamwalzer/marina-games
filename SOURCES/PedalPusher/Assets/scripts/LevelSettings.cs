using UnityEngine;
using System.Collections.Generic;

public class LevelSettings : MonoBehaviour 
{
	public int number;
	public float speedMode;
	public TerrainsList terrains;
	public BonusesList bonuses;
//	public Vector2 emptySpace;
	public float plantsChance;
	public Vector2 plantsDencity;
//	public float bonusMode = 1;

	public float bonuseChance = 0.2f;

//	public int levelLenght = 50;
	public float heartChance = 0.1f;
	public float starChance = 0.8f;
	public float rockChance = 0.2f;
	public float fireCance = 0.1f;

	public int orangeMax = 2;
	public int violetMax = 2;
	public int orbMax = 2;
}
