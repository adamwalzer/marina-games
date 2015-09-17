using UnityEngine;
using System.Collections;

public class TornadoMovement : MonoBehaviour 
{
	public float maxVelocityX = 10;
	public Transform butterfly;
	public float dMax = 3f;

	public Vector2 veloc;

	float normVelocity;
	Rigidbody2D rig2D;
	Vector2 startPos;
	Transform trfm;
	bool isStarted = false;

	void Update(){
		if(!isStarted) return;
		if(butterfly.position.x - trfm.position.x > dMax){
			if(rig2D.velocity.x < maxVelocityX){
				Vector2 v = rig2D.velocity;
				v.x += 0.1f;
				rig2D.velocity = v;
			}
		}
		else{
			if(rig2D.velocity.x > normVelocity){
				Vector2 v = rig2D.velocity;
				v.x -= 0.5f;
				rig2D.velocity = v;
			}
		}
		veloc = rig2D.velocity;
	}

	void Start(){
		rig2D = GetComponent<Rigidbody2D>();
		rig2D.velocity = Vector2.zero;
		trfm = transform;
		startPos = trfm.position;
	}

	public void start(float speed){
		Vector2 velocity = new Vector2();
		velocity.x = speed;
		rig2D.velocity = velocity;
		normVelocity = velocity.x;
		isStarted = true;
	}
	
	public void stop(){
		isStarted = false;
		rig2D.velocity = Vector2.zero;
		transform.position = startPos;
	}
}
