using UnityEngine;
using System.Collections.Generic;

public class LevelSettings : MonoBehaviour 
{
	public int level = 1;
	public float duration = 90f;
	public float speedModif = 1;
	public Vector2 spawnInterval = new Vector2(0.5f, 1.0f);
	public bool duck = false;
	public float duckSpeed = 1.0f;
	public float duckForce = 0.0f;
	public Vector2 duckSpawn = new Vector2(5.0f, 10.0f);
	public bool bouckuet = false;
	public int smogDome = 0;
}