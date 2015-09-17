using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LivesBar : MonoBehaviour 
{
	public Image[] hearts;
	public GameObject addEffect;
	public GameObject removeEffect;
	public Color activeColor;
	public Color inactiveColor;

	int lastIndex = 2;

	public void restart(){
		foreach(Image go in hearts){
			go.color = activeColor;
		}
		addEffect.GetComponent<ParticleEmitter>().emit = false;
		removeEffect.GetComponent<ParticleEmitter>().emit = false;
		lastIndex = hearts.Length - 1;
	}

	public void addLife(){
		lastIndex++;
		hearts[lastIndex].color = activeColor;
		StartCoroutine(showEffect(addEffect));
	}

	public void removeLife(){
		hearts[lastIndex].color = inactiveColor;
		StartCoroutine(showEffect(removeEffect));
		lastIndex--;
	}

	IEnumerator showEffect(GameObject e){
		e.transform.position = hearts[lastIndex].transform.position;
		e.GetComponent<ParticleEmitter>().emit = true;
		yield return new WaitForSeconds(1.0f);
		e.GetComponent<ParticleEmitter>().emit = false;
	}
}
