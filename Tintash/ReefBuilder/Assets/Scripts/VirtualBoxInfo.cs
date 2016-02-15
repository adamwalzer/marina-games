using UnityEngine;
using System.Collections;

public class VirtualBoxInfo : MonoBehaviour {

	
	public int row;
	public int col;
	
	public void Set(int r, int c)
	{
		row = r;
		col = c;
	}
}
