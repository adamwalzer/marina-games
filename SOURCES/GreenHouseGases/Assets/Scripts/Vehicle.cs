using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Vehicle : MonoBehaviour 
{
	public List<Sprite> images;
	public float step;

	Vector3 _destination;
	bool _moveToLeft;
	bool isMoving = false;
	float _speed;

	void Start(){
		SpriteRenderer sR = GetComponent<SpriteRenderer>();
		sR.sprite = images[Random.Range(0, images.Count)];
		_speed = step * (1.0f + Random.value);
	}

	void Update(){
		if(!isMoving) return;

		float _step = _speed * Time.deltaTime;
		if((_moveToLeft && transform.localPosition.x <= _destination.x + _step) || (!_moveToLeft && transform.localPosition.x >= _destination.x - _step)){
			//arrived
			isMoving = false;
			Destroy(gameObject);
		}else{
			//move
			Vector3 newPos = Vector3.MoveTowards(transform.localPosition, _destination, _step);
			transform.localPosition = newPos;
		}
	}

	public void init(){
		_destination = transform.localPosition;
		_destination.x *= -1;
		_moveToLeft = _destination.x < 0;
		if(_moveToLeft){
			transform.localScale = new Vector3(-1, 1, 1);
		}
		isMoving = true;
	}
}
