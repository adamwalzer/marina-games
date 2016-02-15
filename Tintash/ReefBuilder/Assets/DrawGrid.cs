using UnityEngine;
using System.Collections;

public class DrawGrid : MonoBehaviour {


public GameObject prefeb;

private  int row;
private int col;
public Transform Parent;
	// Use this for initialization
	
	/*******************************************************
					
	********************************************************/
	
	void Start () {
		row = DataFile.rows;
		col = DataFile.cols;
		
		
		
		
	
	for(int i=0;i<row;i++)
		for(int j=0;j<col;j++)
		{
			Vector3 Pos =new Vector3(DataFile.statingPos.x+(DataFile.width*j),DataFile.statingPos.y+(DataFile.height*i),0f);
			GameObject obj =Instantiate(prefeb,Pos,Quaternion.identity) as GameObject;
		
			obj.transform.SetParent(Parent);
			
			
			obj.name=i.ToString()+j.ToString();
		}
	}
	}
	