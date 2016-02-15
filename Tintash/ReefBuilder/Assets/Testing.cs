using UnityEngine;
using System.Collections;

public class Testing : MonoBehaviour {

	/*public Vector3 direction     = Vector3.up;
	public float   rotationSpeed = 45f;
	public bool run =false;
	public Vector3 forward=Vector3.zero;
	public bool apply =false;
	public Vector3 startingpostion;
	public bool yesRun=false;
	public bool RayCastYes=false;
	public bool hitwallyes=false;
	public Vector3 wallhitpoint;
	public Vector3 rightwallhitpoint;
	public bool righthitwallyes=false;
	
	public Vector3 rayDir;
	public Vector2 rayDir2d;
	Bounds ap;
	
	
	public float speed; 
	*/
	void Start()
	{
	Debug.Log(" Name of game object "+gameObject.name);
	Debug.Log("width"+gameObject.GetComponent<BoxCollider2D>().bounds.size.x );
		Debug.Log("height"+gameObject.GetComponent<BoxCollider2D>().bounds.size.y );
		Debug.Log("center "+gameObject.GetComponent<BoxCollider2D>().bounds.center);
	
//		startingpostion=transform.position;
	}
/*	
	void Update()
	{
	if(run){
			transform.Rotate( new Vector3( 0f, 0f,rotationSpeed) );
			run=!run;
		}
		 forward = transform.TransformDirection( direction );
		
		Vector3 position = transform.position;
		Debug.DrawLine( position, position + direction, Color.red );
		Debug.DrawLine( position, position + forward, Color.green );
		if(hitwallyes)
			Debug.DrawLine( startingpostion, wallhitpoint, Color.red );
			if(righthitwallyes)
			Debug.DrawLine( wallhitpoint, rightwallhitpoint, Color.red );
		if(yesRun)
			Debug.DrawLine( startingpostion, position + forward, Color.white );
		if(apply)
		{
			Vector3 pos= forward;
		Vector2 dir = new Vector2(pos.x,pos.y);
		//	gameObject.GetComponent<Rigidbody2D>().velocity = dir*speed;
			gameObject.GetComponent<Rigidbody2D>().AddForce(dir*speed);
		
		
		apply=!apply;
			yesRun=!yesRun;
		}
	}
	
	
	void FixedUpdate() {
	
	if(RayCastYes){
		Vector2 dir = new Vector2(forward.x,forward.y);
		RaycastHit2D hit = Physics2D.Raycast(transform.position, dir);
			RaycastHit2D []hit2 =Physics2D.RaycastAll(transform.position, dir);
		if (hit.collider != null) {
		//	float distance = Mathf.Abs(hit.point.y - transform.position.y);
		//	float heightError = floatHeight - distance;
		//	float force = liftForce * heightError - rb2D.velocity.y * damping;
		//	rb2D.AddForce(Vector3.up * force);
		
		Debug.Log("name of collider "+hit.collider.name);
		Vector3 point =new Vector3(hit.point.x,hit.point.y,0f);
				Debug.DrawLine( startingpostion, point, Color.gray );
		}
		
		if(hit2.Length>0)
		{
		Debug.Log("length of hit 2 is "+hit2.Length);
			for(int i=0;i<hit2.Length;i++){
			
			
					Debug.Log("name of collider of hit2["+i+"] ="+hit2[i].collider.name);
					if(hit2[i].collider.name=="Leftwall"){
					Debug.Log("i am in left wall");
						wallhitpoint =new Vector3(hit2[i].point.x,hit2[i].point.y,0f);
						Debug.Log("Hit2.normal " +hit2[i].normal);
						 rayDir  =  Vector3.Reflect( forward ,hit2[i].normal  ) ;
						rayDir2d =Reflect(dir,hit2[i].normal);
						//Debug.DrawLine( startingpostion, point, Color.red );
						RaycastHit2D []hit21 =Physics2D.RaycastAll(wallhitpoint, new Vector2(rayDir.x,rayDir.y));
						if(hit21.Length>0){
							Debug.Log("length of hit 21 is "+hit21.Length);
							for(int ii=0 ;ii<hit21.Length;ii++){
								Debug.Log("name of collider of hit21["+ii+"] ="+hit21[ii].collider.name);
								if(hit21[ii].collider.name=="Rightwall" ||hit21[ii].collider.name=="BottomWall"){
									Debug.Log("i am in "+hit21[ii].collider.name);
									rightwallhitpoint =new Vector3(hit21[ii].point.x,hit21[ii].point.y,0f);
									righthitwallyes=true;
								}
								}
								
						}
						hitwallyes=true;
					}
					
					}
		}
			RayCastYes=!RayCastYes;
		}
		}
		
	public  Vector2 Reflect(Vector2 vector, Vector2 normal)
	{
		return vector - 2 * Vector2.Dot(vector, normal) * normal;
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log(coll.gameObject.name);
		if(coll.gameObject.name=="BottomWall"){
////			gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
////		apply=false;
////		//gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.zero);
			gameObject.GetComponent<Rigidbody2D>().isKinematic=true;
		}
////		else
//{
//		//if(coll.gameObject.name=="Leftwall"){
//		ContactPoint2D[] p= coll.contacts;
//		Debug.Log("collider hit point "+p.Length);
//		Debug.Log("hit point ="+p[0].point);
//			Vector2 dir = new Vector2(forward.x,forward.y);
//			gameObject.GetComponent<Rigidbody2D>().velocity =Reflect(dir,new Vector2(1f,0f))*speed;
//		}
//		
	}
	*/
}
