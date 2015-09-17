using UnityEngine;
using System.Collections;

public class LivesBar : MonoBehaviour 
{
	public GameObject[] hearts;
	public GameObject addEffect;
	public GameObject removeEffect;

	int lastIndex = 2;

	public void restart(){
		foreach(GameObject go in hearts){
			go.SetActive(true);
		}
		addEffect.GetComponent<ParticleEmitter>().emit = false;
		removeEffect.GetComponent<ParticleEmitter>().emit = false;
		lastIndex = hearts.Length - 1;
	}

	public void addLife(){
		lastIndex++;
		hearts[lastIndex].SetActive(true);
		StartCoroutine(showEffect(addEffect));
	}

	public void removeLife(){
		hearts[lastIndex].SetActive(false);
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
