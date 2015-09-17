using UnityEngine;
using System.Collections;

public class SharkMotor : MonoBehaviour 
{
	public float speed = 1f;
	void Update () {
		Vector2 pos = transform.position;
		pos.x -= speed * Time.deltaTime;
		transform.position = pos;
	}
}
