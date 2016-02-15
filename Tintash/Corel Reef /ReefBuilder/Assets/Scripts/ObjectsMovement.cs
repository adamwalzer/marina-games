using UnityEngine;
using System.Collections;

public class ObjectsMovement : MonoBehaviour {

	public 		float 		_speed;
	private 	Vector3 	_forward;
	public 		Vector3 	_direction =Vector3.up;
	public 		bool hitCol =false;
	// Use this for initialization
	public 		bool run =false;
	private 	Vector3 targetPos;
	private 	float speed=8f;
	public  	int ObjRow;
	public  	int ObjCol;
	public 		int ObjId;
	public 	 	int weight;
	public 		int countCollision; 
	// Keep track of the direction in which the oyester is moving
	private Vector2 velocity;
	
	// used for velocity calculation
	private Vector2 lastPos;
	public bool postionSet=false;
	void Start () {
	//	FireObject();
	Controller.GetInstance().turn=false;
		countCollision=0;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(run){
			transform.position = Vector3.MoveTowards(transform.position, targetPos, speed  * Time.deltaTime);
		if(transform.position== targetPos){
				run=false;
				//gameObject.GetComponent<Rigidbody2D>().isKinematic=false;
				 
				//gameObject.GetComponentiRigidbody2D>().constraints =RigidbodyConstraints2D.FreezeAll;
				//gameObject.GetComponentInChildren<Collider2D>().isTrigger=true;
				postionSet=true;
				
				if(ObjId!=8&& ObjId!=6 && ObjId!=9 && ObjId!=10){
						Debug.Log(" i am in aaa");
						Controller.GetInstance().turn=true;
						Controller.GetInstance().CheckingCombination(ObjRow,ObjCol);
						}
						else
						{
					Debug.Log(" i am in aaa");
							if(ObjId==6)
						Controller.GetInstance().DestroyingColoumn(ObjRow,ObjCol);
						else 
						if(ObjId==8)
						Controller.GetInstance().DestroyEntireRow(ObjRow,ObjCol);
						else
						
						if(ObjId==9){
						PlayerPrefs.SetInt("heartObj",1);
						Controller.GetInstance().HeartDestorying(ObjRow,ObjCol);
						
						}
						else
						if(ObjId==10){
						PlayerPrefs.SetInt("heartObj",0);
						Controller.GetInstance().HeartDestorying(ObjRow,ObjCol);
							}
						}
						
						
					}
			Controller.GetInstance().IsKinematic(false);
				}
	
	}
	void FixedUpdate ()
	{
		// Get pos 2d of the ball.
		Vector3 pos3D = transform.position;
		Vector2 pos2D = new Vector2(pos3D.x, pos3D.y);
		
		// Velocity calculation. Will be used for the bounce
		velocity = pos2D - lastPos;
		lastPos = pos2D;
	}
	public void SetObjRowAndCol(int r,int c)
	{
			ObjCol=c;
			ObjRow=r;
	}
	public Vector2 GetRowAndCol()
	 {
	  return new Vector2(ObjRow, ObjCol);
	}
	public void FireObject()
	{
		_forward = transform.TransformDirection( _direction );
		
		Vector2 dir = new Vector2(_forward.x,_forward.y);
		gameObject.GetComponent<Rigidbody2D>().AddForce(dir*_speed);
			
			
			
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log("collide with "+ name +"coll "+coll.gameObject.name +"  "+hitCol);
		if(hitCol)
		if(coll.gameObject.name=="BottomWall" || coll.gameObject.CompareTag("obj")||coll.gameObject.CompareTag("fence")|| coll.gameObject.CompareTag("shrub")){
			hitCol=false;
					gameObject.GetComponent<Rigidbody2D>().isKinematic=true;
					Vector3 pos =new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,-10f);
					int  layerMask  = 1 << LayerMask.NameToLayer("layer2");
					
					RaycastHit2D hit = Physics2D.Raycast(pos,Vector3.forward,Mathf.Infinity,layerMask);
					if(hit.collider!=null)
					{
					if(hit.collider.tag!="end")
					{
					
						
						ObjRow = hit.collider.gameObject.GetComponent<VirtualBoxInfo>().row;
						ObjCol = hit.collider.gameObject.GetComponent<VirtualBoxInfo>().col;
					CheckingPoperPostion(ref ObjRow,ref ObjCol,coll.contacts[0],pos);
						gameObject.transform.localPosition =Controller.GetInstance().GetVirtualBoxCenter(ObjRow,ObjCol);
				ExactLoacation(ObjRow,ObjCol);
				
				}
				else{
					Debug.Log(" end function");
					Controller.GetInstance().hit=true;
					Controller.GetInstance().levelcompleted();
					Destroy(this.gameObject);
				}
						
					}
					
			}
			else
			if(coll.gameObject.name=="Leftwall" || coll.gameObject.name=="Rightwall")
		{
			ContactPoint2D contact = coll.contacts[0];
			
			// Normal
			Vector3 N = coll.contacts[0].normal;
			
			//Direction
			Vector3 V = velocity.normalized;
			
			// Reflection
			Vector3 R = Vector3.Reflect(V, N).normalized;
			Debug.Log("direction angle "+velocity);
			// Assign normalized reflection with the constant speed
			Debug.Log(" reflection angle  when  hit the wall vack"+R ); 
			
			float a=velocity.magnitude;
			float b=R.magnitude;
			
			float ab =Vector2.Dot(velocity,R);
			float angle =Mathf.Acos(ab/(a*b))*Mathf.Rad2Deg;
			
			Debug.Log("Angle = "+angle);
			if(angle>175 && angle<=180)
			{
					
				R =new Vector3(0.9f, -0.4f, 0.0f);
				gameObject.GetComponent<Rigidbody2D>().isKinematic=true;
				gameObject.GetComponent<Rigidbody2D>().isKinematic=false;
				gameObject.GetComponent<Rigidbody2D>().AddForce(R*_speed);
			}
			
			
			Debug.Log(" angle that will hit the wall vack "+Reflect(new Vector2(_forward.x,_forward.y),contact.normal)+" velocity "+gameObject.GetComponent<Rigidbody2D>().velocity);
			
		}
		else{
			if(countCollision!=0){
				Debug.Log("if(countCollision!=0){");
			
			ContactPoint2D contact = coll.contacts[0];
			
			// Normal
			Vector3 N = coll.contacts[0].normal;
			
			//Direction
			Vector3 V = velocity.normalized;
			
			// Reflection
			Vector3 R = Vector3.Reflect(V, N).normalized;
			gameObject.GetComponent<Rigidbody2D>().isKinematic=true;
			gameObject.GetComponent<Rigidbody2D>().isKinematic=false;
			gameObject.GetComponent<Rigidbody2D>().AddForce(R*_speed);
			}
		}
		if(coll.gameObject.name=="Leftwall" || coll.gameObject.name=="Rightwall" ||coll.gameObject.name=="TopWall" ||coll.gameObject.name=="windmill1" 
		   ||coll.gameObject.name=="windmill2" ||coll.gameObject.name=="HotBalloon2" ||coll.gameObject.name=="HotBalloon1"
		   ||coll.gameObject.name=="ShipWheel3" ||coll.gameObject.name=="ShipWheel2" ||coll.gameObject.name=="ShipWheel1")
		   {
			if(!postionSet){
				if(coll.gameObject.name=="HotBalloon2" ||coll.gameObject.name=="HotBalloon1")
				SoundManager.Instance.PlayStrikeWithballonSound();
				   	Debug.Log(" i am in count collision");
					if(coll.gameObject.name=="TopWall")
						if(countCollision==0){
						return;
						}
						
						countCollision+=1;
						
						if(countCollision==5){
						Debug.Log(" collision count is equal to 6 ");
						Controller.GetInstance().IsKinematic(true);
						
					gameObject.GetComponent<Rigidbody2D>().isKinematic=true;
					gameObject.GetComponent<Rigidbody2D>().isKinematic=false;
					gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.down*_speed);
					}
				   }
		   }
			
			
	}
	

	void ExactLoacation(int r,int c)
	{

		Vector3 pos =Controller.GetInstance().GetVirtualBoxCenter(r,c);
		int _row=r;
		int _col=c;


		int x=r;
		if(x>0)
		do
		{
		
			
			if(Controller.GetInstance().mainGrid.Get(x-1,c)){	
											Debug.Log("Break Condition");
							
							break;
						
				}
				else{
				Debug.Log("Gid["+(x-1)+"]["+c+"] == null");
						pos= Controller.GetInstance().GetVirtualBoxCenter(x-1,c);
						_row=x-1;
						x--;
						}
		
		}while(x>0);

		targetPos=new Vector3(pos.x,pos.y,0f);
		Debug.Log("Target postion of "+name +targetPos);
		Controller.GetInstance().AddInMainGrid(_row,c,this.gameObject);
		ObjRow=_row;
		ObjCol=_col;
		//gameObject.GetComponent<Rigidbody2D>().constraints =RigidbodyConstraints2D.FreezeAll;
		if(ObjRow==DataFile.rows-1)
		{
			Debug.Log(" end function");
			Controller.GetInstance().hit=true;
			Controller.GetInstance().levelcompleted();
			Destroy(this.gameObject);
		}else
		
		run=true;
	
		
		
	}
	
	public void SetTarget(Vector3 tar)
	{
	targetPos=tar;
	}
			public  Vector2 Reflect(Vector2 vector, Vector2 normal)
			{
				return vector - 2 * Vector2.Dot(vector, normal) * normal;
			}
			
			
	private  void CheckingPoperPostion(ref int r,ref int c,ContactPoint2D point,Vector3 pos)
	{	
		float right =1000f;
		float left = 1000f;
		Vector2 pos2d = new Vector2(pos.x,pos.y);
		if(Controller.GetInstance().mainGrid.checkingNullValue(r,c))
		{
			if(c==0)
			{
				if(!Controller.GetInstance().mainGrid.checkingNullValue(r,c+1))
				{
				
					c=c+1;
				}
				else
				{	
					r=r+1;
				}
			}
			else
				if(c==DataFile.cols-1)
				{
					if(!Controller.GetInstance().mainGrid.checkingNullValue(r,c-1))
					{
						
						c=c-1;
					}
					else
					{	
						r=r+1;
					}	
				}
				
				else
				if(c>=1 && c<=DataFile.cols-2)
				{
					left = DistCal(r,c-1,pos2d);
					right= DistCal(r,c+1,pos2d);
					
					
					if(left>right)
					{
						if(Controller.GetInstance().mainGrid.checkingNullValue(r,c+1))
						r=r+1;
						else
						c=c+1;
					}
					else{
					if(Controller.GetInstance().mainGrid.checkingNullValue(r,c-1))
						r=r+1;
						else
						c=c-1;
						}
					
				}	
				
				
				
		}
	
		
		
		
	}	
	
		private float Distance(Vector2 a, Vector2 b)
		{
				return Vector2.Distance(a,b);
		}	
		
		private float DistCal(int r,int c,Vector2 pos )
		{
					Vector3 cen =Controller.GetInstance().GetVirtualBoxCenter(r,c);
					Vector2 cen2d = new Vector2(cen.x,cen.y);
					return Vector2.Distance(cen2d,pos);
		}
	
	
	
	
}
