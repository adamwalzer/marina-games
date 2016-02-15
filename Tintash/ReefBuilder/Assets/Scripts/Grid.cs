using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	private GameObject[,] _grids;	
	private int _rows;
	private int _cols;
	
	private void Awake()
	{
		_rows = DataFile.rows ;
		_cols = DataFile.cols;
		
		_grids = new GameObject[_rows, _cols];
		Intialize();
	}
	
	/*******************************************************
					
	********************************************************/
	
	public GameObject Get(int row, int col)
	{
		return _grids[row, col];
	}
	
	/*******************************************************
					
	********************************************************/
	
	public void Set(int row,int col, GameObject obj)
	{
		_grids[row, col] = obj;
	}
	
	/*******************************************************
					
	********************************************************/
	
	
	public void Remove(int row,int col)
	{

		Destroy(_grids[row, col]);
		_grids[row, col] = null;
	}
	
	/*******************************************************
					
	********************************************************/
	
	public void Reset()
	{
		for (int i = 0; i < DataFile.rows; i++)
		{
			for (int j = 0; j < DataFile.cols; j++)
			{
				GameObject one = _grids[i, j];
				if (one != null)
				{
					if(GettingObjId(i,j)!=-2)				
					Destroy(one);
					_grids[i, j] = null;
				}
			}
		}
	}
	
	/*******************************************************
					
	********************************************************/
	
	public int DestroyingRow(int r,int c,GameObject [,]coll)
	{	
		int numberOfOyesters=0;
		for (int _c=0;_c<DataFile.cols;_c++)
		{
			if(_grids[r,_c]!=null){
				if(GettingObjId(r,_c)==0)
					numberOfOyesters+=1;
				if(GettingObjId(r,_c)!=-2)
				   VerticalReposition2(r,_c,coll,1);
			}
			
		}
		return numberOfOyesters;
	}
	
	/*******************************************************
					
	********************************************************/
	
	public int DestroyingColoumn(int r,int c)
	{
		SoundManager.Instance.PlayColoumnCleared();
	Remove(r,c);
		int numberOfOyesters=0;
		for (int _r=0;_r<r;_r++)
		{
			if(_grids[_r,c]!=null){
				if(GettingObjId(_r,c)==0)
					numberOfOyesters+=1;
					
					if(GettingObjId(_r,c)!=-2)
				Remove(_r,c);
				}
				
		}
		return numberOfOyesters;
	}
	
	/*******************************************************
					
	********************************************************/
	
	public void VerticalReposition(int row,int col,GameObject [,]coll)
	{
		Remove(row,col);
		for (int _r=row;_r<_rows;_r++)
		{
			if(_grids[_r+1,col]!=null)
			{
				_grids[_r,col]=_grids[_r+1,col];
				_grids[_r,col].gameObject.GetComponent<ObjectsMovement>().SetTarget(coll[_r,col].GetComponent<BoxCollider2D>().bounds.center);
				_grids[_r,col].gameObject.GetComponent<ObjectsMovement>().run=true;
				_grids[_r,col].gameObject.GetComponent<ObjectsMovement>().SetObjRowAndCol(_r,col);
				_grids[_r+1,col]=null;
			}
			else
			break;
		}
	}
	
	/*******************************************************
					
	********************************************************/
	
	void Intialize()
	{	
		for(int i=0;i<_rows;i++)
			for(int j=0;j<_cols;j++)
				_grids[i,j]=null;
	
		
	}
	
	/*******************************************************
					
	********************************************************/
	
	public void VerticalReposition2(int row,int col,GameObject [,]coll,int del)
	{
	
		for(int i=0;i<del;i++)
		Remove(row+i,col);
		row=row+(del-1);//  row+1;
		for (int _r=row;_r<_rows;_r++)
		{
			
			if((_r+1)<DataFile.rows){
			if(_grids[_r+1,col]!=null)
			{
				int rr=_r;
				
				for(int i=_r;i>=0;i--)
				{
					
					if(_grids[i,col]==null)
						rr=i;
						else
						break;
				}
				_grids[rr,col]=_grids[_r+1,col];
				_grids[rr,col].gameObject.GetComponent<ObjectsMovement>().SetTarget(coll[rr,col].GetComponent<BoxCollider2D>().bounds.center);
				_grids[rr,col].gameObject.GetComponent<ObjectsMovement>().run=true;
				_grids[rr,col].gameObject.GetComponent<ObjectsMovement>().SetObjRowAndCol(rr,col);
				_grids[_r+1,col]=null;
			}
			else
				break;
				}
				else
				break;
		}
	}
	
	/*******************************************************
					
	********************************************************/
	
	public bool CombinationFive(int r,int c,ref int state)
	{
	
		// case 1
		bool yes=false;
		if(r!=0)
		if(_grids[r,c]!=null)
		{
			
			if(c<=(DataFile.cols-2))
			{
				
				if(checkingNull(r,c,1)){ // left
					
						if(IsLeft(r,c)){
						yes=true;
						state=1;
						}
					}
					if(!yes)
					if(c>=(2)){
					if(checkingNull(r,c,2) ){
						
						if(IsRight(r,c)){
						yes =true;
							state=2;
						}
					}
					if(!yes)
					if(checkingNull(r,c,0)){
						
						if(IsCenter(r,c)){
							yes=true;
							state=0;
						}
					}
				}
					if(!yes)
					if(c>=(1))
					if(checkingNull(r,c,0)){
							
							if(IsCenter(r,c)){
							yes=true;
							state=0;
						}
						}
						
					
			}
			else
			if(c==(DataFile.cols-1)){
				if(checkingNull(r,c,2)){
					
					if(IsRight(r,c)){
						yes =true;
						state=2;
					}
				}
				if(!yes)
					if(checkingNull(r,c,0)){
						
					if(IsCenter(r,c)){
						yes=true;
						state=0;
					}
					}
			}
				
			
		}
		return yes;
	}
	
	/*******************************************************
					
	********************************************************/
	
	public bool checkingNull(int r,int c, int opt)
	{
	
		
		
		bool _isnull=false;
	  if(opt==0){   // mid
	 		 if(checkingNullValue(r,c-1))
				if(checkingNullValue(r,c+1))
					if(checkingNullValue(r-1,c))
						if(checkingNullValue(r-1,c-1))
							if(checkingNullValue(r-1,c+1))
							_isnull=true;
							
							}

		else
		if(opt==1){ // left 
			if(checkingNullValue(r,c+1))
				if(checkingNullValue(r,c+2))
					if(checkingNullValue(r-1,c))
						if(checkingNullValue(r-1,c+1))
							if(checkingNullValue(r-1,c+2))
								_isnull=true;
		
	
		}
		else
		if(opt==2){ // right 
			if(checkingNullValue(r,c-1))
				if(checkingNullValue(r,c-2))
					if(checkingNullValue(r-1,c))
						if(checkingNullValue(r-1,c-1))
							if(checkingNullValue(r-1,c-2))
								_isnull=true;
								}
					
					
							return _isnull;		
		}
		 
	
	/*******************************************************
					
	********************************************************/
	
	public bool IsLeft(int r,int c){
		 
		 bool returnvalue=false;
		if(GettingObjId(r,c+1)==1 || GettingObjId(r,c+1)==2 ||GettingObjId(r,c+1)==3 || GettingObjId(r,c+1)==4 ||GettingObjId(r,c+1)==5||GettingObjId(r,c+1)==6)
			if(GettingObjId(r,c+2)==0)
				if(GettingObjId(r-1,c)==0)
					if(GettingObjId(r-1,c+1)==0)
						if(GettingObjId(r-1,c+2)==0)
							if(GettingObjId(r,c)==0)
							
							returnvalue=true;
							
		return returnvalue;
						
		 }
	
	/*******************************************************
					
	********************************************************/
	
	public bool IsRight(int r,int c){
		
		bool returnvalue=false;
		if(GettingObjId(r,c-1)==1 || GettingObjId(r,c-1)==2 ||GettingObjId(r,c-1)==3||GettingObjId(r,c-1)==4||GettingObjId(r,c-1)==5||GettingObjId(r,c-1)==6)
			if(GettingObjId(r,c-2)==0)
			    if(GettingObjId(r-1,c)==0)
			      if(GettingObjId(r-1,c-1)==0)
			          if(GettingObjId(r-1,c-2)==0)
							if(GettingObjId(r,c)==0)
							returnvalue=true;
		
		return returnvalue;
		
	}
		
	
	/*******************************************************
					
	********************************************************/
	
	public bool IsCenter(int r,int c){
		
		bool returnvalue=false;
		if(GettingObjId(r,c+1)==0)
			if(GettingObjId(r,c-1)==0)
				if(GettingObjId(r-1,c)==0)
					if(GettingObjId(r-1,c-1)==0)
						if(GettingObjId(r-1,c+1)==0)
							if(GettingObjId(r,c)==1||GettingObjId(r,c)==2||GettingObjId(r,c)==3||GettingObjId(r,c)==4||GettingObjId(r,c)==5||GettingObjId(r,c)==6)
							returnvalue=true;
		
		return returnvalue;
		
	}
	
	/*******************************************************
					
	********************************************************/
	
	public bool checkingNullValue(int r,int c)
	{
	
			
			if(r<0||c<0||r>=DataFile.rows ||c>=DataFile.cols)
			return false;
			else
			{
					if(_grids[r,c]!=null)
					{
					
					return true;
					}
					else{
					
					return false;
					}
			}
	}
	
	/*******************************************************
					
	********************************************************/
	
	public int GettingObjId(int r,int c)
	{
		int val=-1;
		if(_grids[r,c]!=null){
		 val= _grids[r,c].gameObject.GetComponent<ObjectsMovement>().ObjId;
		
		}
		return val;
	}
	
	/*******************************************************
					
	********************************************************/
	
	public void ShipPlacement(int r,int c,GameObject s)
	{
		if(r==1 && c>=1 && c<=(DataFile.cols-2))
		{
			
			_grids[r,c]=s;
			_grids[r,c-1]=s;
			_grids[r,c+1]=s;
			_grids[r+1,c]=s;
			_grids[r+1,c+1]=s;
			_grids[r-1,c]=s;
			_grids[r-1,c]=s;
			_grids[r-1,c-1]=s;
			
		}
	
	}
	
	
	
	
	
	
	
	
	
	
}
