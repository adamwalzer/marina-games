using UnityEngine;
using System.Collections;

public class Saladbowl : MonoBehaviour 
{
	public Sprite emptySprite;
	public Sprite fullSprite;

	public SpriteRenderer view;

	bool isEmpty = true;
	int counter = 0;

	public void reset(){
		view.sprite = emptySprite;
		isEmpty = true;
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.tag == "R"){
			States.State<StateGame>().catchReward(col.GetComponent<Reward>());
			counter++;
			if(isEmpty){
				isEmpty = false;
				view.sprite = fullSprite;
			}
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