using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDText : MonoBehaviour 
{
	public Animator anima;
	public Text text;

	public void init(int count, Color color){
		gameObject.SetActive(true);
		text.color = color;
		init(count);
	}

	public void init(int count){
		gameObject.SetActive(true);

		if(count > 0){
			text.text = "+" + count;
			anima.SetTrigger("Add");
		}else{
			text.text = count.ToString();
			anima.SetTrigger("Remove");
		}
	}

	public void init(string str, bool up){
		gameObject.SetActive(true);
		text.text = str;
		if(up){
			anima.SetTrigger("Add");
		}else{
			anima.SetTrigger("Remove");
		}
	}

	public void deinit(){
		gameObject.SetActive(false);
	}
}
