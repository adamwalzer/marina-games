using UnityEngine;
using System.Collections;

public class VariegatedDrop : MonoBehaviour
{

	public Vector2 sizeRange = new Vector2 (0.1f, 1.0f);

	void Start ()
	{
		float r = Random.Range (sizeRange.x, sizeRange.y);		
		transform.localScale *= r;
	}
}
