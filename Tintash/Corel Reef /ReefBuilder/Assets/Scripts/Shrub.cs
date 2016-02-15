using UnityEngine;
using System.Collections;

public class Shrub : MonoBehaviour {

	public float []yPos; 
	public float startingx;
	public float xfac;
	public int []options;
	public int no;
	// Use this for initialization
	void OnEnable () {
	
		int   col  = Random.Range(options[0],options[1]);
		int   row  = Random.Range(0,5); 
		float xval=0.0f;
		if(no==0)
				xval=startingx-(xfac*col);
				
		else
		{
			int a=col-6;
			xval=startingx-(xfac*a);
			}
		
		transform.localPosition =  new Vector3(xval,yPos[row],0.0f);
		Controller.GetInstance().mainGrid.Set(row,col,gameObject);
		Controller.GetInstance().mainGrid.Set(row,col+1,gameObject);
			
			
	}
	
	// Update is called once per frame
}
