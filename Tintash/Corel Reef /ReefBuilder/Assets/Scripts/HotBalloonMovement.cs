using UnityEngine;
using System.Collections;

public class HotBalloonMovement : MonoBehaviour {

	public float speed;
	Vector3 dir =Vector3.up;
	float scalex;
	public bool Up;
	public int ballonNumber;
	bool run =false;
	
	
	void OnEnable()
	{
		if(ballonNumber==1)
			transform.localPosition = new Vector3(-5.11f,6.13f,0f);
			else
			transform.localPosition = new Vector3(6.5f,-4.4f,0f);
			
			
		
			run=true;
			
	}
	
	void OnDisable()
	{
	
		
	//	transform.GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.None;
		run=false;
	}
	
	void Update () {
		
		
		if(run){
		transform.position+=dir *speed*Time.deltaTime;
		if(transform.position.y>6f){
			dir =Vector3.down;
			Up=false;
			}
		else
			if(transform.position.y<-6f){
				dir =Vector3.up;
			Up=true;
				}
		}
		
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("collidrjjcejkwcke "+other.name);
		if(other.gameObject.CompareTag("obj")){
			
			if(other.gameObject.GetComponentInParent<ObjectsMovement>().postionSet)
				if(Up)
			{
				dir=Vector3.down;
				Up=false;
			}
			else
			{
				dir = Vector3.up;
				Up=true;
			}
		}
	}
/*	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log(coll.gameObject.name);
		
		if(coll.gameObject.CompareTag("obj")){
			
			if(coll.gameObject.GetComponent<ObjectsMovement>().postionSet)
			if(Up)
			{
				dir=Vector3.down;
				Up=false;
			}
			else
			{
				dir = Vector3.up;
				Up=true;
			}
			}
		}*/
}
