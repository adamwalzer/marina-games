using UnityEngine;
using System.Collections;

public class VirtualBoxInfo : MonoBehaviour {

	
	public int row;
	public int col;
	public bool opt;
	public void Set(int r, int c)
	{
		row = r;
		col = c;
	}
	
	void OnEnable()
	{
	if(opt)
		Debug.Log (" center of point "+this.transform.GetComponent<BoxCollider2D>().bounds.center);
	}
}
