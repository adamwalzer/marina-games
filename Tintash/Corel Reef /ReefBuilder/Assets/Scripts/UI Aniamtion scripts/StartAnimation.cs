using UnityEngine;
using System.Collections;

public class StartAnimation : MonoBehaviour {

	// Use this for initialization
	public Animator animator;
	public void RunStarAnimation()
	{
		if(PlayerPrefs.GetInt("LevelIsComplete")==1)
		animator.SetBool("RunStar",true);
	}
}
