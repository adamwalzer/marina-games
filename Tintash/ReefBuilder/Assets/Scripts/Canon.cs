using UnityEngine;
using System.Collections;

public class Canon : MonoBehaviour {

public Animator animator;
public AnimationClip clip;



	// Use this for initialization
	void Start () {
	animator =GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	public void startAnimation()
	{
	
	 animator.SetBool("Canon",true);
	GetComponent<AudioSource>().Play();
	  Invoke("StopAnimation",clip.length);
	}
	
	public void StopAnimation()
	{
		animator.SetBool("Canon",false);
	}
}
