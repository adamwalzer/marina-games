using UnityEngine;
using System.Collections.Generic;

public class LevelSettings : MonoBehaviour 
{
	public FallingPrefs[] fallingItems;
	public Vector2 spawnPeriod;
	public int levelTime;
	public RewardTypes.RewardType type;
	public float quest;
	public float mistakeValue;
	public bool lightningBonus = false;
	public bool moneybagBonus = false;
}
