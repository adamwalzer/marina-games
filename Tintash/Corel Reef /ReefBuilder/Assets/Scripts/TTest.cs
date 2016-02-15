using UnityEngine;
using System.Collections;

public class TTest : MonoBehaviour {


	/*public GameObject barreal;
	public GameObject anim;
	
	public int i;
	public float scale;
	// Use this for initialization
	void OnEnable () {
	
		barreal.transform.localPosition =Controller.GetInstance().GetVirtualBoxCenter(i,0);
		anim.transform.localPosition =Controller.GetInstance().GetVirtualBoxCenter(i,0);
		anim.transform.localScale =new Vector3(anim.transform.localScale.x,i*scale,anim.transform.localScale.z);
		
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}*/
	IEnumerator Start() {
		AsyncOperation async = Application.LoadLevelAsync("Game");
		yield return async;
		Debug.Log("Loading complete");
	}
}
