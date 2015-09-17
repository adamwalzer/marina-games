using UnityEngine;
using System.Collections.Generic;

public class LevelSettings : MonoBehaviour 
{
	public float duration = 90f;
	public float speedModif = 1;
	public Vector2 spawnInterval = new Vector2(0.5f, 1.0f);
	public List<FallingPrefs> dropItems;
	public bool lightnings = false;
	public bool grasshoppers = false;
	public CloudSettings rainSettings;
	public bool rainIsContinue;
	public CloudSettings snowSettings;
	public bool tornado;
}