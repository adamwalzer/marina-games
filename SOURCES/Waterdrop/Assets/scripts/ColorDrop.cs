using UnityEngine;
using System.Collections.Generic;

public class ColorDrop : MonoBehaviour
{
	public List<Color> colors;
	public SpriteRenderer view;

	void Start(){
		int r = Random.Range(0, colors.Count);
		view.color = colors[r];
	}
}
