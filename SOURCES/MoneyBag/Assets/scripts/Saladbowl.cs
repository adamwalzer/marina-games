using UnityEngine;
using System.Collections;

public class Saladbowl : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D col){
		if(col.tag == "R"){
			States.State<StateGame>().catchReward(col.GetComponent<Reward>());
		}else if(col.tag == "O"){
			States.State<StateGame>().catchObstacle(col.GetComponent<Obstacle>());
		}
	}

	public void move(float xPos){
		Vector2 pos = transform.position;
		pos.x = xPos;
		transform.transform.position = pos;
	}
}