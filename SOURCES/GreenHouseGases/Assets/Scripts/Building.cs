using UnityEngine;
using System.Collections.Generic;

public class Building : MonoBehaviour 
{
	public List<Sprite> images;

	void Start(){
		SpriteRenderer sR = GetComponent<SpriteRenderer>();
		sR.sprite = images[Random.Range(0, images.Count)];
	}
}
