using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class Recipe 
{
	public Sprite image;
	public RecipeItem[] items;
}


[Serializable]
public struct RecipeItem{
	public Sprite icon;
	public float colorParam;
	public GameObject[] items;
}