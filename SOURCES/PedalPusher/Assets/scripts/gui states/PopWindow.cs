using UnityEngine;
using System.Collections;

public class PopWindow : MonoBehaviour 
{
	public virtual void show(){
		gameObject.SetActive(true);
	}
	
	public virtual void hide(){
		gameObject.SetActive(false);
	}
}
