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
//	public 			GameObject			anchor1;
//	public 			GameObject			anchor2;
	public 			GameObject			[]windMill;
	public 			GameObject			[]_shrubs;
	public 			GameObject			blast;
	public 			GameObject			GreenGem;
	public 			GameObject			heartObj;
	public 			GameObject			goldObj;
	public 			GameObject			rowClearObject; 
	public	 		GameObject			fence;
	public 			GameObject			hotballon1;
	public 			GameObject			hotballon2;
	public 			GameObject			bgSound;
	public 			GameObject			colClearedObj;
	public 			bool				hit;
	public 			AnimationClip       bclip;
	public 			AnimationClip		rclaerclip;
	public 			AnimationClip		cclearclip;
	public 			AnimationClip		greenGemclip;
	public 			AnimationClip		heartclip;
	public 			AnimationClip		goldClip;
	private 		Vector2            	destroyPos;
	public 			float				saveangle;
	public 			float				_speed; 
	public 			Vector2				[]colClearedarray;
	public 			Vector2				blastPos;
	public 			int 				ObjectIdforAnim;
	public 			float[]				fenceXpos;
	public 			int testcol;
	public bool yescol;
	
	public GameObject tutorial;
	
	

	
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
	//	FencePlacement();
	
		
		
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
			
			if(yescol){
			FencePlacement();
			yescol=!yescol;
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
			int end=6;
			int weight=10;
			switch(currentlevel)
			{
				case 2:
						end=10;
						weight=4;
						break;
				
				case 3:
						end=13;
						weight=3;
						break;
			
				case 4:
						end=14;
						weight=2;
						break;
				
				case 5:
						end	= 14;
						weight=2;
						break;	
				
				
			}
		
			
			for(int i=5;i<=end;i++)
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
			virtualBoxParent.transform.localPosition =new Vector3(0.77f,0f,0f);
		
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
			_shrubs[0].GetComponent<BoxCollider2D>().isTrigger = opt;
			_shrubs[1].GetComponent<BoxCollider2D>().isTrigger = opt;
			break;
		case 3:
			for(int i=0;i<windMill.Length;i++){
				windMill[i].GetComponent<PolygonCollider2D>().isTrigger = opt;
				Transform _windMillTrans=windMill[i].transform.FindChild("wings");
				_windMillTrans.FindChild("1").GetComponent<BoxCollider2D>().isTrigger=opt;
				_windMillTrans.FindChild("2").GetComponent<BoxCollider2D>().isTrigger=opt;
				_windMillTrans.FindChild("3").GetComponent<BoxCollider2D>().isTrigger=opt;
				_windMillTrans.FindChild("4").GetComponent<BoxCollider2D>().isTrigger=opt;
				
				}
				
			
			break;
			
		case 5:
			hotballon1.GetComponent<BoxCollider2D>().isTrigger = opt;
			hotballon2.GetComponent<BoxCollider2D>().isTrigger = opt;
			hotballon1.GetComponent<CircleCollider2D>().isTrigger = opt;
			hotballon2.GetComponent<CircleCollider2D>().isTrigger = opt;
			
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
		Debug.Log(" i am in DestroyingColoumn");
		if(r==0)
		{
			Debug.Log(" if(r==0)");
			mainGrid.Remove(r,c);
			turn=true;
		}
		else
		{
			Debug.Log(" if(r==0) else");
			colClearedObj.transform.localScale =new Vector3(colClearedObj.transform.localScale.x,colClearedarray[r-1].x,colClearedObj.transform.localScale.z);
			float x  = virtualGrid[r,c].GetComponent<BoxCollider2D>().bounds.center.x;
			float y	 = virtualGrid[r,c].GetComponent<BoxCollider2D>().bounds.center.y;
			colClearedObj.transform.localPosition= new Vector3(x,y,0f);
		colClearedObj.SetActive(true);
			destroyPos= new Vector2((float)r,(float)c);
			SoundManager.Instance.PlayColoumnCleared();
			Invoke("DestroyEntireColumn",cclearclip.length);
		}
		
	}
	
	public void DestroyEntireColumn()
	{
		
				int r=(int)destroyPos.x;
				int c=(int)destroyPos.y;
		
		int num=mainGrid.DestroyingColoumn(r,c);
		colClearedObj.SetActive(false);
		_score +=(DataFile.barrel +(num *DataFile.stoneScore));
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
			_score +=(DataFile.crystal +(num *DataFile.stoneScore));
			scoreText.text =_score.ToString();
		
		turn=true;
	}
	/*******************************************************
					HeartDestorying() Function 
	********************************************************/
	public void HeartDestorying(int r,int c)
	{
		Debug.Log(" i ma in HeartDestorying function ");
		destroyPos.x= (float)r;
		destroyPos.y=(float)c;
		if(r>=0)
		{
			Vector3 pos =new Vector3(virtualGrid[r,c].GetComponent<BoxCollider2D>().bounds.center.x,virtualGrid[r,c].GetComponent<BoxCollider2D>().bounds.center.y,0f);
			
			if(PlayerPrefs.GetInt("heartObj")==1){
			pos = new Vector3(pos.x,pos.y-1.0f,pos.z);
			heartObj.transform.localPosition = pos;
			heartObj.SetActive(true);
			
			Invoke("CloseHeartAnimation",heartclip.length);
			}
			else
				if(PlayerPrefs.GetInt("heartObj")==0)
				{
				goldObj.transform.localPosition = pos;
				goldObj.SetActive(true);
				Invoke("CloseHeartAnimation",goldClip.length);
				}
			
		}
		else
		{
		
			Invoke("CloseHeartAnimation",0.2f);
			}
		//mainGrid.Remove(r,c);
	} 
	public void CloseHeartAnimation()
	{
		if(PlayerPrefs.GetInt("heartObj")==1){
		SoundManager.Instance.PlayheartSound();
		heartObj.SetActive(false);
		
		}
		else
		if(PlayerPrefs.GetInt("heartObj")==0){
		SoundManager.Instance.PlayGoldSound();
		goldObj.SetActive(false);
		}
		int r =(int)destroyPos.x;
		int c=(int)destroyPos.y;
		
		if(r>=0){
		int num=0;
			mainGrid.VerticalReposition2(r,c,virtualGrid,1);
			
			Debug.Log(" [r,c] =["+r+","+c+"]");
		if(mainGrid.checkingNullValue (r,c-1))
		{
			if(mainGrid.GettingObjId(r,c-1)==0)
			{
					Debug.Log(" [r,c-1] =["+r+","+(c-1)+"]");
				num=num+1;
				
			}
			
				if(mainGrid.GettingObjId(r,c-1)!=-2)
			mainGrid.VerticalReposition2(r,c-1,virtualGrid,1);
			
		}
		
		if(mainGrid.checkingNullValue (r-1,c-1))
		{
			if(mainGrid.GettingObjId(r-1,c-1)==0)
				{Debug.Log(" [r-1,c-1] =["+(r-1)+","+(c-1)+"]");
				num=num+1;
				
			}
				if(mainGrid.GettingObjId(r-1,c-1)!=-2)
			mainGrid.VerticalReposition2(r-1,c-1,virtualGrid,1);
			
		}
		if(mainGrid.checkingNullValue (r-1,c))
		{
			if(mainGrid.GettingObjId(r-1,c)==0)
			{
					Debug.Log(" [r-1,c] =["+(r-1)+","+(c)+"]");
				num=num+1;
				
			}
			
				if(mainGrid.GettingObjId(r-1,c)!=-2)
			mainGrid.VerticalReposition2(r-1,c,virtualGrid,1);
			
		}
		
		//	mainGrid.VerticalReposition2(r,c,virtualGrid,1);
		
		if(mainGrid.checkingNullValue (r,c+1))
		{
			if(mainGrid.GettingObjId(r,c+1)==0)
				{Debug.Log(" [r,c+1] =["+r+","+(c+1)+"]");
				num=num+1;
				
			}
				if(mainGrid.GettingObjId(r,c+1)!=-2)
			mainGrid.VerticalReposition2(r,c+1,virtualGrid,1);
			
		}
		
		if(mainGrid.checkingNullValue (r-1,c+1))
		{
			if(mainGrid.GettingObjId(r-1,c+1)==0)
				{Debug.Log(" [r-1,c+1] =["+(r-1)+","+(c+1)+"]");
				num=num+1;
				
			}
			
				if(mainGrid.GettingObjId(r-1,c+1)!=-2)
			mainGrid.VerticalReposition2(r-1,c+1,virtualGrid,1);
			
		}
		Debug.Log("value of num +"+num);
			Debug.Log("value of _score +"+_score);
			if(PlayerPrefs.GetInt("heartObj")==1)
			_score +=(DataFile.heartScore *(num *DataFile.stoneScore));
			else
				if(PlayerPrefs.GetInt("heartObj")==0)
					_score +=(DataFile.GoldScore *(num *DataFile.stoneScore));
			scoreText.text =_score.ToString();
		}
		else
		mainGrid.Remove(r,c);
		
		
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
		SoundManager.Instance.PlaySoundFile(ObjectIdforAnim);//PlayFiveGroupCleared();
		destroyPos= new Vector2((float)r,(float)c);
		if(ObjectIdforAnim!=7)
		{
			blast.transform.localPosition =new Vector3(pos.x-blastPos.x,pos.y-blastPos.y,pos.z);
			blast.SetActive(true);
			Invoke("closeblast",bclip.length);
		}
		else
		
		{
			GreenGem.transform.localPosition =new Vector3(pos.x-blastPos.x,pos.y-blastPos.y,pos.z);
			GreenGem.SetActive(true);
			Invoke("closeblast",greenGemclip.length);
		}
		
		

	}
	
	/*******************************************************
					DestroyEntireRow() Function 
	********************************************************/
	
	public void DestroyEntireRow(int r,int c)
	{
		SoundManager.Instance.PlayRowCleared();
		float xfac = -1.44897f;
		float yPos = virtualGrid[r,c].GetComponent<BoxCollider2D>().bounds.center.y;
		//Vector3 pos = new Vector3(rowClearObject.transform.position.x,yPos,rowClearObject.transform.position.z);
		Vector3 pos =virtualGrid[r,c].GetComponent<BoxCollider2D>().bounds.center;
		pos.x=pos.x+xfac;
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
		ObjectIdforAnim=animalId;
		switch(animalId){
		case 1:
			_score += DataFile.squirrel;
			break;	
		case 2:
			_score += DataFile.ant;
			break;
		case 3:
			_score += DataFile.rat;
			break;	
		case 4:
			_score += DataFile.snake;
			break;
		case 5:
			_score += DataFile.frog;
			break;
		case 7:
			_score += DataFile.gem;
			break;		
				
		
		}
		
		scoreText.text =_score.ToString();
	}
	
	/*******************************************************
					Tutorial() Function 
	********************************************************/
	
	public void Tutorial()
	{
		tutorial.SetActive(true);
	}
	
	public void TutorialFinished()
	{
	tutorial.SetActive(false);
			NextButtonPressed();
	
	}
	/*******************************************************
					NextButtonPressed() Function 
	********************************************************/
	
	public void NextButtonPressed()
	{
		SoundManager.Instance.ClickSound();
		if(PlayerPrefs.GetInt("playButtonPressed")==1)
		{
			PlayerPrefs.SetInt("playButtonPressed",0);
			Tutorial();
		}
		else
		{
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
		fence.SetActive(false);
	//	anchor1.SetActive(false);
	//	anchor2.SetActive(false);
		}
		else
		if(currentlevel==3)
		{
			for(int i=0;i<windMill.Length;i++)
				windMill[i].SetActive(false);
			
		}
		else if(currentlevel == 4){
			_shrubs[0].SetActive(false);
			_shrubs[1].SetActive(false);
		}
		else if(currentlevel==5)
		{
			hotballon1.SetActive(false);
			hotballon2.SetActive(false);
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
		Vector3 b=virtualGrid[r,c].GetComponent<BoxCollider2D>().bounds.center;
		return b;
	}
	
	/*******************************************************
					closeblast() Function 
	********************************************************/
	
	public void closeblast()
	{
		if(ObjectIdforAnim!=7)
		blast.SetActive(false);
		else
		GreenGem.SetActive(false);
		
		int r= (int)destroyPos.x;
		int c= (int)destroyPos.y;
		mainGrid.VerticalReposition2(r,c,virtualGrid,2);
		mainGrid.VerticalReposition2(r,c+1,virtualGrid,2);
		mainGrid.VerticalReposition2(r,c+2,virtualGrid,2);
	}
	
	/*******************************************************
					FencePlacement() Function 
	********************************************************/
	
	public void FencePlacement()
	{
		int col =Random.Range(0,DataFile.cols-1);
		//int col =testcol;
		Debug.Log("col of fence ="+col);
		float intialPosX =-8.28f;
		float fac=-1.98f;
		float finalX =fenceXpos[col];//intialPosX-(col*fac);
		fence.SetActive(true);
		fence.transform.position = new Vector3(finalX,fence.transform.position.y,fence.transform.position.z);
		mainGrid.FencePlacement(0,col,fence);
	
	}
	
	/*******************************************************
					LevelObject() Function 
	********************************************************/
	
	void LevelObject(int currLevel)
	{
		switch(currLevel){
		case 2:
		FencePlacement();
		//	anchor1.SetActive(true);
		//	anchor2.SetActive(true);
			break;
		case 3:
			for(int i=0;i<windMill.Length;i++)
				windMill[i].SetActive(true);
		//	anchor1.SetActive(false);
		//	anchor2.SetActive(false);
			break;
		case 4:
			
			for(int i=0;i<_shrubs.Length;i++)
				//windMill[i].SetActive(false);
				_shrubs[i].SetActive(true);
		//	FencePlacement();
			break;
			
		case 5:
			hotballon1.SetActive(true);
			hotballon2.SetActive(true);
			
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
