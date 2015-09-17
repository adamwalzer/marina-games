using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDText : IClearable 
{
	public Animator anima;
	public Text text;

	public void init(int count){
		gameObject.SetActive(true);

		if(count > 0){
			text.text = "+" + count;
			anima.SetTrigger("Add");
		}else{
			text.text = "-" + count;
			anima.SetTrigger("Remove");
		}
	}

	public override void deinit(){
		gameObject.SetActive(false);
	}
}
