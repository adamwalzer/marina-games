using UnityEngine;
using System.Collections;

public class Submarine : MonoBehaviour {

public float speed;
Vector3 dir ;
float scalex;
public int opt;
bool right;
bool left;
	// Use this for initialization
	/*******************************************************
					OnEnable() Function 
	********************************************************/
	void OnEnable () {
				Vector3 s =transform.localScale;
			if(opt==0)
			{
			Debug.Log(" right ");
				
				dir=Vector3.right;
				transform.localScale =  new Vector3(s.x*-1,s.y,s.z);
					transform.rotation = Quaternion.Euler(0, 0,-13);
					transform.position = new Vector3(-9.2f,3.98f,0.0f);	
					left=true;
			}
			else{
					Debug.Log(" right ");
			transform.position = new Vector3(9f,1.71f/*2.2f*/,0.0f);	
					dir=Vector3.left;
					//transform.localScale =  new Vector3(s.x,s.y,s.z);
					transform.rotation = Quaternion.Euler(0, 0,13);	
					left=false;
				}
	}
	
	/*******************************************************
					onDsiable() Function 
	********************************************************/
	
	void OnDisable () {
		transform.localScale =  new Vector3(1f,1f,1f);
		transform.rotation = Quaternion.Euler(0, 0,0);
	
	}
	
	/*******************************************************
					
	********************************************************/
	
	// Update is called once per frame
	void Update () {
	
	
		
		transform.position+=dir *speed*Time.deltaTime;
		if(transform.position.x>9f)
		RightWallFunction();
		else
					if(transform.position.x<-9.2f)
					LeftWallFunction();
					
		
	}
	
	/*******************************************************
					
	********************************************************/
	
	
	void RightWallFunction()
	{
	if(left){
	Debug.Log(" strike with right wall ");
		Vector3 s =transform.localScale;
		dir=Vector3.left;
		transform.localScale =  new Vector3(s.x*-1,s.y,s.z);
		transform.rotation = Quaternion.Euler(0, 0,13);	
		left=false;
		}
	
	}
	
	/*******************************************************
					
	********************************************************/
	
	void LeftWallFunction()
	{
		if(!left){
		Debug.Log(" strike with left wall ");
		Vector3 s =transform.localScale;
		dir=Vector3.right;
		transform.localScale =  new Vector3(s.x*-1,s.y,s.z);
		transform.rotation = Quaternion.Euler(0, 0,-13);	
		left=true;
		}
	
	}
}
