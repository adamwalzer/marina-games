using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {
public GameObject [] objects;
public Vector2 [] arrows;
public Sprite []arrowsImage;

public int opt=0;
public bool run=true;

void Awake()
{
	Debug.Log(" i am in awake function of tutorial ");
		run=true;
	
	
}
	// Use this for initialization
	void OnEnable () {
		if(run){
			opt=0;
			for(int i=0;i<objects.Length;i++)
			{
			
				if(i==6)
				{
					Debug.Log(objects[7].GetComponent<RectTransform>().sizeDelta);
				}
				objects[i].SetActive(false);
			}
			
			Next();
			run=false;
		}
		else{
		
			Controller.GetInstance().TutorialFinished();
			}
	
	}
	
	
	public void Next()
	{
	
	switch(opt)
	{
		case 0:
			objects[9].SetActive(true);
				break;
		case 1:
			SoundManager.Instance.PlayCanon();
			objects[9].SetActive(false);
			objects[0].SetActive(true);
			objects[6].SetActive(true);
			objects[6].GetComponent<RectTransform>().localPosition = new Vector3(arrows[opt-1].x,arrows[opt-1].y,0f);
			objects[6].GetComponent<Image>().sprite=arrowsImage[opt-1];
			break;
		case 2:
			SoundManager.Instance.PlayCanon();
			objects[1].SetActive(true);
			objects[6].GetComponent<RectTransform>().localPosition = new Vector3(arrows[opt-1].x,arrows[opt-1].y,0f);
			objects[6].GetComponent<Image>().sprite=arrowsImage[opt-1];
			break;
			
		case 3:
			SoundManager.Instance.PlayCanon();
			objects[2].SetActive(true);
			objects[6].GetComponent<RectTransform>().localPosition = new Vector3(arrows[opt-1].x,arrows[opt-1].y,0f);
			objects[6].GetComponent<Image>().sprite=arrowsImage[opt-1];
			break;	
			
		case 4:
			SoundManager.Instance.PlayCanon();
			objects[3].SetActive(true);
			objects[6].SetActive(false);
			objects[7].SetActive(true);
			objects[7].GetComponent<RectTransform>().localPosition = new Vector3(arrows[opt-1].x,arrows[opt-1].y,0f);
			objects[7].GetComponent<RectTransform>().sizeDelta= new Vector2 (230f,226f);
			objects[7].GetComponent<Image>().sprite=arrowsImage[opt-1];
			break;	
		case 5:
			SoundManager.Instance.PlayCanon();
			objects[4].SetActive(true);
			objects[7].GetComponent<RectTransform>().localPosition = new Vector3(arrows[opt-1].x,arrows[opt-1].y,0f);
			
	
			objects[7].GetComponent<Image>().sprite=arrowsImage[opt-1];
			break;	
		case 6:
			SoundManager.Instance.PlayCanon();
			objects[5].SetActive(true);
			objects[5].GetComponent<RectTransform>().localPosition = new Vector3(-2f,98f,0f);
			objects[7].GetComponent<RectTransform>().localPosition = new Vector3(arrows[opt-1].x,arrows[opt-1].y,0f);
			objects[7].GetComponent<RectTransform>().sizeDelta= new Vector2 (230f,293f);
			//objects[7].GetComponent<RectTransform>().sizeDelta.y =293f;
			
			objects[7].GetComponent<Image>().sprite=arrowsImage[opt-1];
			break;	
		case 7:
			
			objects[5].GetComponent<RectTransform>().localPosition = new Vector3(-2f,-1f,0f);
			//objects[7].GetComponent<RectTransform>().localPosition = new Vector3(arrows[opt-1].x,arrows[opt-1].y,0f);
			//objects[7].GetComponent<RectTransform>().sizeDelta= new Vector2 (230f,293f);
			//objects[7].GetComponent<RectTransform>().sizeDelta.y =293f;
			
			//objects[7].GetComponent<Image>().sprite=arrowsImage[opt-1];
			break;	
		case 8:
			SoundManager.Instance.PlayFiveGroupCleared();
			objects[8].SetActive(true);
			Invoke("closeblast",0.2f);
			break;	
			
		case 9:
			Controller.GetInstance().TutorialFinished();
			break;	
		
	}
		opt++;
}

	void closeblast()
	{
		for(int i=0;i<objects.Length;i++)
		{
			objects[i].SetActive(false);
		}
		objects[9].SetActive(true);
	}
}
