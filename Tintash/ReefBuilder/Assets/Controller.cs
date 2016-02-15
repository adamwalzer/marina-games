using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Controller : MonoBehaviour {

	
	private 		Vector2 			movement;
	public 			Transform 			shooter;
	private 		Vector3 			screenPos;
	private 		float   			angleOffset;
	
	public 			GameObject[,] 		virtualGrid;
	
	public 			GameObject 			prefab;
	
	public 			GameObject 			virtualBox;
	public 			Transform 			virtualBoxParent;
	public 			Transform			ObjParents;
	private  static Controller 			instance;

	public 			bool 				test = false;
	
	public 			Grid 				mainGrid;
	public			Levels              UIhandler;
	int count;
	public  		bool 				turn;
	public 			int 				del = 1; 
	
	public 			AnimationClip 		ani;
	public 			Animator 			shooteranim;
	public 			GameObject [] 		Oysters;

	
	public 			int 				objectlistcount;
	public 			int					currentlevel;
	public 			int 				Previouslevel;
	public 			int					_score=0; 
	public 			int 				i = 0;
	public  		Timer				runTimer;
	public 			int					[]option=new int[3];
	public 			int					[]levelScores = new int[5]; 
	
	public 			Sprite				[]images; 
	public 			Image				[]_imageBar; 
	public			Text				scoreText;
	public 			GameObject			boat_hull;
	public 			GameObject 			cannon;	
	public 			GameObject			anchor1;
	public 			GameObject			anchor2;
	public 			GameObject			[]wheels;
	public 			GameObject			blast;
	public 			GameObject			rowClearObject; 
	public	 		GameObject			ship;
	public 			GameObject			submarine1;
	public 			GameObject			submarine2;
	public 			GameObject			bgSound;
	public 			GameObject			colClearedObj;
	public 			bool				hit;
	public 			AnimationClip       bclip;
	public 			AnimationClip		rclaerclip;
	public 			AnimationClip		cclearclip;
	private 		Vector2            	destroyPos;
	public 			float				saveangle;
	public 			float				_speed; 
	public 			Vector2				[]colClearedarray;
	
	

	
	// Use this for initialization
	
	/*******************************************************
					Awake() Function 
	********************************************************/
	
	void Awake ()
	{
		if (instance != null) {
			Destroy (this.gameObject);
		} else {
			instance = this;
			
		}
		
	}
	
	
	/*******************************************************
					GetInstance Function 
	********************************************************/
	
	public static Controller GetInstance ()
	{
		return instance;
		
		
	}
	
	/*******************************************************
					Start() Function 
	********************************************************/
	
	void Start () {
			mainGrid =this.GetComponent<Grid>();
			virtualGrid = new GameObject[DataFile.rows,DataFile.cols];
			_speed =DataFile.CorealSpeed;
			MakingVirtualGrid();
			currentlevel=1;
			Previouslevel=1;
			count=0;
			objectlistcount=0;
			CalObjectListcount();
			hit = false;
			_score=0;
			scoreText.text="0";
			ResetOptionArray();
		IntializeLevelScoreArray();
		colClearedObj.SetActive(false);
	}
	
	// Update is called once per frame
	/*******************************************************
					Upadte() Function 
	********************************************************/
	
	void Update () {	
			if(turn){
				if (Input.GetMouseButtonDown (0)) {
					if(!test)
					CalculateRotation();
					
				} else
				if (Input.GetMouseButtonUp (0)) {
					if(!test){
				Debug.Log("**************************************");		
					IntializeObstacle();
						UpdateOptionArray();
					}
					else
						Testing( Camera.main.ScreenToWorldPoint(Input.mousePosition));
				
				}
				else
				if (Input.GetMouseButton(0)) {
					if(!test)
				CalculateRotation();
				}
			}
	
	}

	/*******************************************************
					CalculateRotation() Function 
	********************************************************/
	
	void CalculateRotation()
	{	
			Vector3 moveTowards = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			
			movement = new Vector2(moveTowards.x -shooter.position.x,moveTowards.y -shooter.position.y) ;
			float targetAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
			targetAngle=-targetAngle;
			
			if(targetAngle>=8 && targetAngle<=175)
			{
				saveangle=targetAngle;
			}
			else
				targetAngle=saveangle;
			targetAngle=-targetAngle;
			targetAngle=(targetAngle-90)-180;
			
			shooter.rotation = Quaternion.Euler(0, 0,targetAngle);
		
	}
	
	/*******************************************************
					IntializeObstacle() Function 
	********************************************************/
	
	
	void IntializeObstacle()
	{

		GameObject obj =Instantiate(Oysters[option[2]],shooter.localPosition,shooter.GetChild(0).rotation) as GameObject;
		obj.transform.GetChild(0).eulerAngles=new Vector3(0f,0f,obj.transform.rotation.z);
		obj.GetComponent<ObjectsMovement>()._speed=_speed;// DataFile.CorealSpeed;
		obj.GetComponent<ObjectsMovement>().hitCol=true;
		
		obj.GetComponent<ObjectsMovement>().FireObject();
		shooter.GetChild(0).GetComponent<Canon>().startAnimation();
		obj.transform.SetParent(ObjParents);
		obj.name =count.ToString();
		count++;
		i=(i=i+1)%6;
		
		
		
	
	}
	
	/*******************************************************
					CalObjectListcount() Function 
	********************************************************/
	
	public void CalObjectListcount()
	{
			int end=8;
			int weight=10;
			switch(currentlevel)
			{
				case 2:
						end=10;
						weight=5;
						break;
				
				case 3:
						end=13;
						weight=3;
						break;
			
				case 4:
						end=14;
						weight=3;
						break;
				
				case 5:
						end	= 14;
						weight=3;
						break;	
				
				
			}
		
			
			for(int i=6;i<end;i++)
			{
				Oysters[i].GetComponent<ObjectsMovement>().weight=weight;
				objectlistcount=i+1;
			}
		
		
		
	}

	/*******************************************************
					MakingVirtualGrid() Function 
	********************************************************/
	
	void MakingVirtualGrid()
	{

			for(int i=0;i<DataFile.rows;i++)
				for(int j=0;j<DataFile.cols;j++)
			{
				Vector3 Pos =new Vector3(DataFile.statingPos.x+(DataFile.width*j),DataFile.statingPos.y+(DataFile.height*i),0f);
				GameObject obj =Instantiate(virtualBox,Pos,Quaternion.identity) as GameObject;
				
				obj.transform.SetParent(virtualBoxParent);
				obj.GetComponent<VirtualBoxInfo>().Set(i,j);
				virtualGrid[i,j] = obj;
				obj.name=i.ToString()+j.ToString();
			}
		
	}
	
	/*******************************************************
					AddMainGrid() Function 
	********************************************************/
	
	public void AddInMainGrid(int r, int c,GameObject obj)
	{
		mainGrid.Set(r,c,obj);
	
	}
	
	
	/*******************************************************
					Iskinematic() Function 
	********************************************************/
	public void IsKinematic(bool opt)
	{
		switch(currentlevel){
		case 2:
			anchor1.GetComponent<PolygonCollider2D>().isTrigger = opt;
			anchor2.GetComponent<PolygonCollider2D>().isTrigger = opt;
			break;
		case 3:
			for(int i=0;i<wheels.Length;i++)
				wheels[i].GetComponent<PolygonCollider2D>().isTrigger = opt;;
			
			break;
			
		case 5:
			submarine1.GetComponent<PolygonCollider2D>().isTrigger = opt;;
			submarine2.GetComponent<PolygonCollider2D>().isTrigger = opt;;
			break;			
		}
	}
	
	public void RemoveFromArrayOfObject(int r,int c)
	{
	
	}
	
	/*******************************************************
					GetVirtualBoxcenter() Function 
	********************************************************/
	
	public Vector3 GetVirtualBoxCenter(int r,int c)
	{
		return virtualGrid[r,c].GetComponent<BoxCollider2D>().bounds.center;
	}
	
	void Testing(Vector3 pos)
	{
		
		int  layerMask  = 1 << LayerMask.NameToLayer("layer1");
		RaycastHit2D hit = Physics2D.Raycast(pos,Vector3.forward,Mathf.Infinity,layerMask);
		if(hit.collider!=null)
		{
			Vector2 rc = hit.collider.transform.GetComponentInParent<ObjectsMovement>().GetRowAndCol();
			
			mainGrid.VerticalReposition2((int)rc.x,(int)rc.y,virtualGrid,del);
			//mainGrid.VerticalReposition2((int)rc.x,(int)(rc.y-1),virtualGrid,del);
			
			
			
			
		}
	}
	
	/*******************************************************
					ResetOptionArray() Function 
	********************************************************/
	
	public void ResetOptionArray()
	{
		option[0]=0;
		option[1]=1;
		option[2]=2;
		UpdateImageArray();
	}
	
	/*******************************************************
					StartGame() Function 
	********************************************************/
	
	public void StartGame()
	{
	
	
	Invoke("Run",2.0f);
	
	}
	
	/*******************************************************
					Run() Function 
	********************************************************/
	
	public void Run()
	{
		runTimer.timeStarted=true;
		turn=true;
	}
	
	/*******************************************************
					GetRandomNumber() Function 
	********************************************************/
	
	public int GetRandomNumber()
	{
		int totalWeight = 0;
		
		for(int i=0;i< objectlistcount; i++) {
			totalWeight += Oysters[i].GetComponent<ObjectsMovement>().weight;
		}
		int randomNumber =Random.Range(0,totalWeight);
		
		int m_random = 0;
		for(int j=0;j< objectlistcount; j++) {
			if (randomNumber < Oysters[j].GetComponent<ObjectsMovement>().weight) {
				break;
			} else {
				randomNumber -= Oysters[j].GetComponent<ObjectsMovement>().weight;
			}
			m_random++;
		}
		return m_random;
	}
	
	/*******************************************************
					UpdateOptionArray() Function 
	********************************************************/
	
	private void UpdateOptionArray()
	{
	
			for(int i=2;i>0;i--)
			{
				option[i]=option[i-1];
			}
			
			option[0]=GetRandomNumber();
		UpdateImageArray();
		
	}
	
	/*******************************************************
					UpdateImageArray() Function 
	********************************************************/
	
	public void UpdateImageArray()
	{
	int j=2;
			for(int i=0;i<3;i++)	{
			
			
			_imageBar[i].sprite=images[option[j]];
			j--;
			
			}
	
	}
	
	/*******************************************************
					DestroyingColoumn() Function 
	********************************************************/
	
	public void DestroyingColoumn(int r,int c)
	{
		if(r==0)
		{
			mainGrid.Remove(r,c);
			turn=true;
		}
		else
		{
			colClearedObj.transform.localScale =new Vector3(colClearedObj.transform.localScale.x,colClearedarray[r].y,colClearedObj.transform.localScale.z);
			float x  = virtualGrid[r,c].GetComponent<BoxCollider2D>().bounds.center.x;
			colClearedObj.transform.localPosition= new Vector3(x,colClearedarray[r].x,0f);
		colClearedObj.SetActive(true);
			destroyPos= new Vector2((float)r,(float)c);
			Invoke("DestroyEntireColumn",cclearclip.length);
		}
		
	}
	
	public void DestroyEntireColumn()
	{
				int r=(int)destroyPos.x;
				int c=(int)destroyPos.y;
		
		int num=mainGrid.DestroyingColoumn(r,c);
		colClearedObj.SetActive(false);
		_score +=(DataFile.starFishScore +(num *DataFile.oystersScore));
		scoreText.text =_score.ToString();
		turn = true;
	}
	/*******************************************************
					DestroyingRow() Function 
	********************************************************/
	
	public void DestroyingRow()
	{
		rowClearObject.SetActive(false);
		int r=(int)destroyPos.x;
		int c=(int)destroyPos.y;
		
		int num=mainGrid.DestroyingRow(r,c,virtualGrid);
			_score +=(DataFile.eelScore +(num *DataFile.oystersScore));
			scoreText.text =_score.ToString();
		
		turn=true;
	}
	
	/*******************************************************
					CheckingComination() Function 
	********************************************************/
	
	public void CheckingCombination(int r,int c)
	{
		int state=-1;
		Debug.Log("value of combination =:"+mainGrid.CombinationFive(r,c,ref state));
		Vector3 pos =Vector3.zero;
		switch(state){
		case 0:
			
			CalculateScore(mainGrid.GettingObjId(r,c));
			pos= AnimationPos(r,c);
			DestroyFiveCombination(r-1,c-1,pos);
				
				break;
		case 1:
			CalculateScore(mainGrid.GettingObjId(r,c+1));
			pos= AnimationPos(r,c+1);
			DestroyFiveCombination(r-1,c,pos);
			
			break;
		
		case 2:
			CalculateScore(mainGrid.GettingObjId(r,c-1));
			pos= AnimationPos(r,c-1);
			DestroyFiveCombination(r-1,c-2,pos);
			
			break;
				}
		
	}
	
	/*******************************************************
			Destroying Five Combination () Function 
	********************************************************/
	
	public void DestroyFiveCombination(int r, int c,Vector3 pos)
	{
		SoundManager.Instance.PlayFiveGroupCleared();
		blast.transform.localPosition =pos;
		blast.SetActive(true);
		destroyPos= new Vector2((float)r,(float)c);
		Invoke("closeblast",bclip.length);

	}
	
	/*******************************************************
					DestroyEntireRow() Function 
	********************************************************/
	
	public void DestroyEntireRow(int r,int c)
	{
		SoundManager.Instance.PlayRowCleared();
		float yPos = virtualGrid[r,c].GetComponent<BoxCollider2D>().bounds.center.y;
		Vector3 pos = new Vector3(rowClearObject.transform.position.x,yPos,rowClearObject.transform.position.z);
		rowClearObject.transform.position = pos;
		rowClearObject.SetActive(true);
		destroyPos= new Vector2((float)r,(float)c);
		
		Invoke("DestroyingRow",rclaerclip.length);
	}
	
	/*******************************************************
					CalculateScore() Function 
	********************************************************/
	
	
	public void CalculateScore(int animalId)
	{
		
		switch(animalId){
		case 1:
			_score += DataFile.snailscore;
			break;	
		case 2:
			_score += DataFile.crabscore;
			break;
		case 3:
			_score += DataFile.shrimpscore;
			break;	
		case 4:
			_score += DataFile.octupusScore;
			break;
		case 5:
			_score += DataFile.fishScore;
			break;
		case 6:
			_score += DataFile.clamScore;
			break;		
				
		
		}
		
		scoreText.text =_score.ToString();
	}
	
	/*******************************************************
					NextButtonPressed() Function 
	********************************************************/
	
	public void NextButtonPressed()
	{
		SoundManager.Instance.ClickSound();
		CalObjectListcount();
		if(currentlevel==Previouslevel){
			bgSound.SetActive(true);
		if(currentlevel>1)
		_speed =_speed+(_speed*0.2f);
		else
				_speed =DataFile.CorealSpeed;
			LevelObject(currentlevel);
			
		
		cannon.SetActive(true);
		boat_hull.SetActive(true);
			StartGame();
		}
		UIhandler.nextButtonPressed(currentlevel,Previouslevel);
		
		
	}
	
	/*******************************************************
					LevelCompleted() Function 
	********************************************************/
	
	public void levelcompleted()
	{
		bgSound.SetActive(false);
		shooter.rotation = Quaternion.identity;
		mainGrid.Reset();
		foreach(Transform child in ObjParents) {
			Destroy(child.gameObject);
		}
		runTimer.timeStarted=false;
		cannon.SetActive(false);
		boat_hull.SetActive(false);
		if(currentlevel==2){
		anchor1.SetActive(false);
		anchor2.SetActive(false);
		}
		else
		if(currentlevel==3)
		{
			for(int i=0;i<wheels.Length;i++)
				wheels[i].SetActive(false);
			
		}
		else if(currentlevel == 4)
		ship.SetActive(false);
		else if(currentlevel==5)
		{
			submarine1.SetActive(false);
			submarine2.SetActive(false);
		}
		
		turn=false;
		
		levelScores[currentlevel-1]=_score;
		UIhandler.LevelcompleFuntion(_score,runTimer.timerInSeconds2,hit);
		_score=0;
		
		
	}
	
	/*******************************************************
				ReplayButtonPressed() Function 
	********************************************************/
	
	public void ReplayButtonPressed()
	{
		SoundManager.Instance.ClickSound();
		cannon.SetActive(true);
		boat_hull.SetActive(true);
		UIhandler.nextButtonPressed(currentlevel,Previouslevel);
		LevelObject(currentlevel);
		bgSound.SetActive(true);
		StartGame();
	}
	
	/*******************************************************
					AnimalPos() Function 
	********************************************************/
	
	public Vector3 AnimationPos(int r,int c)
	{
		Vector3 b=virtualGrid[r-1,c].GetComponent<BoxCollider2D>().bounds.center;
		return b;
	}
	
	/*******************************************************
					closeblast() Function 
	********************************************************/
	
	public void closeblast()
	{
		blast.SetActive(false);
		int r= (int)destroyPos.x;
		int c= (int)destroyPos.y;
		mainGrid.VerticalReposition2(r,c,virtualGrid,2);
		mainGrid.VerticalReposition2(r,c+1,virtualGrid,2);
		mainGrid.VerticalReposition2(r,c+2,virtualGrid,2);
	}
	
	/*******************************************************
					ShipPlacement() Function 
	********************************************************/
	
	public void ShipPlacement()
	{
		int col =Random.Range(1,DataFile.cols-2);
		ship.SetActive(true);
		ship.transform.position = virtualGrid[1,col].GetComponent<BoxCollider2D>().bounds.center;
		mainGrid.ShipPlacement(1,col,ship);
	
	}
	
	/*******************************************************
					LevelObject() Function 
	********************************************************/
	
	void LevelObject(int currLevel)
	{
		switch(currLevel){
		case 2:
			anchor1.SetActive(true);
			anchor2.SetActive(true);
			break;
		case 3:
			for(int i=0;i<wheels.Length;i++)
				wheels[i].SetActive(true);
			anchor1.SetActive(false);
			anchor2.SetActive(false);
			break;
		case 4:
			
			for(int i=0;i<wheels.Length;i++)
				wheels[i].SetActive(false);
			ShipPlacement();
			break;
			
		case 5:
			submarine1.SetActive(true);
			submarine2.SetActive(true);
			break;			
		}
		
		ResetOptionArray();
	}
	
	/*******************************************************
					IntializeLevelScoreArray() Function 
	********************************************************/
	public void IntializeLevelScoreArray()
	{
		for(int i=0;i<5;i++)
		levelScores[i]=0;
	}
	
	
	
	
	
	
}
