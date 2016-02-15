using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Levels : MonoBehaviour {

	enum UIObjectList
	{
		levelOneWindow=0 ,
		levelTwoWindow=1,
		levelCompleWindow=2,
		levelfailedWindow=3,
		window=4,
		title=5,
		nextButton=6,
		replaybutton=7,
		timePanel=8,
		Displaypane=9,
		scorePanel=10,
		replaybuttonagain=11,
		levelThreeWindow=12,
		levelfourWindow=13,
		levelFiveWindow=14,
		gameComplete=15,
		}
	;

	public GameObject  	[]UIGameObjectArray;
	public GameObject 	MainUI;
	public Vector3 		replayButtonIntialPos;
	public bool 		levelComplete;
	public Sprite 		[]tileImages;
	


	// Use this for initialization
	void Start () {
		intialize();
		MainUI.SetActive(true);
		replayButtonIntialPos=UIGameObjectArray[(int)UIObjectList.replaybutton].GetComponent<RectTransform>().position;
		levelComplete=false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void intialize()
	{
		for(int i=0;i<UIGameObjectArray.Length;i++)
		UIGameObjectArray[i].SetActive(false);
	}
	
	public void PlayButtonClick()
	{
	SoundManager.Instance.ClickSound();
	MainUI.SetActive(false);
	
		
		IntailSetup(0); //here's the deal 
		UIGameObjectArray[(int)UIObjectList.levelOneWindow].SetActive(true);
		UIGameObjectArray[(int)UIObjectList.nextButton].SetActive(true);
		
		
	
	}
	
	void IntailSetup(int opt)
	{
		UIGameObjectArray[(int)UIObjectList.title].SetActive(true);
		
		UIGameObjectArray[(int)UIObjectList.title].GetComponent<Image>().sprite=tileImages[opt];
		UIGameObjectArray[(int)UIObjectList.window].SetActive(true);
		
		
	
	}
	
	public void nextButtonPressed(int CL,int PL)
	{
		intialize();
		if(CL==PL)
		{
		UIGameObjectArray[(int)UIObjectList.timePanel].SetActive(true);
		UIGameObjectArray[(int)UIObjectList.scorePanel].SetActive(true);
		UIGameObjectArray[(int)UIObjectList.Displaypane].SetActive(true);
		}
		else
		{	intialize();
			if(CL==1)
			{
				IntailSetup(0);// here's the deal
				UIGameObjectArray[(int)UIObjectList.levelOneWindow].SetActive(true);
				UIGameObjectArray[(int)UIObjectList.nextButton].SetActive(true);
				Controller.GetInstance().Previouslevel=CL;
			}
			else
			if(CL==2)
			{
				IntailSetup(0);// here's the deal
				UIGameObjectArray[(int)UIObjectList.levelTwoWindow].SetActive(true);
				UIGameObjectArray[(int)UIObjectList.nextButton].SetActive(true);
				Controller.GetInstance().Previouslevel=CL;
			}
			else
				if(CL==3)
			{
				IntailSetup(0);// here's the deal
				UIGameObjectArray[(int)UIObjectList.levelThreeWindow].SetActive(true);
				UIGameObjectArray[(int)UIObjectList.nextButton].SetActive(true);
				Controller.GetInstance().Previouslevel=CL;
			}
			else
			if(CL==4)
			{
				IntailSetup(0);// here's the deal
				UIGameObjectArray[(int)UIObjectList.levelfourWindow].SetActive(true);
				UIGameObjectArray[(int)UIObjectList.nextButton].SetActive(true);
				Controller.GetInstance().Previouslevel=CL;
			}
			else
				if(CL==5)
			{
				IntailSetup(0);// here's the deal
				UIGameObjectArray[(int)UIObjectList.levelFiveWindow].SetActive(true);
				UIGameObjectArray[(int)UIObjectList.nextButton].SetActive(true);
				Controller.GetInstance().Previouslevel=CL;
			}
			else
				if(CL==6)
			{
				IntailSetup(3);// game complete 
				UIGameObjectArray[(int)UIObjectList.gameComplete].SetActive(true);
				UIGameObjectArray[(int)UIObjectList.nextButton].SetActive(true);
				Controller.GetInstance().currentlevel+=1;
				//Controller.GetInstance().Previouslevel=CL;
			}
			else
				if(CL==7)
				{
				intialize();
				MainUI.SetActive(true);
				Controller.GetInstance().currentlevel=1;
				Controller.GetInstance().Previouslevel=1;
				Controller.GetInstance().IntializeLevelScoreArray();
				
				}
		}
		
	}
	
	public void LevelcompleFuntion(int score, float time,bool hit )
	{
		GameObject  win;
		UIGameObjectArray[(int)UIObjectList.scorePanel].transform.FindChild("Image").GetComponentInChildren<Text>().text="0";
		intialize();
		if(hit || score==0)
		{
		SoundManager.Instance.LevelFailed();
			IntailSetup(2); // level failed 
			win = UIGameObjectArray[(int)UIObjectList.levelfailedWindow];
			UIGameObjectArray[(int)UIObjectList.replaybuttonagain].SetActive(true);
			
			levelComplete=false;

		}
		else{
			SoundManager.Instance.LevelComplete();
			IntailSetup(1);// level complete 
			win= UIGameObjectArray[(int)UIObjectList.levelCompleWindow];
			UIGameObjectArray[(int)UIObjectList.replaybutton].SetActive(true);
			UIGameObjectArray[(int)UIObjectList.nextButton].SetActive(true);
			Controller.GetInstance().currentlevel+=1;
			levelComplete=true;
		}
		win.SetActive(true);
		win.transform.FindChild("Score").GetComponent<Text>().text="Score : "+score.ToString();
		if(!levelComplete)
		win.transform.FindChild("Time").GetComponent<Text>().text = "Time :"+CalTime(time);
	} 
	
	public string CalTime(float t)
	{
		t =( (2*60)+30)-t;
		if(t>0){
		
			int minutes = Mathf.FloorToInt (t / 60F);
			int seconds = Mathf.FloorToInt (t - minutes * 60);
		
		string niceTime = string.Format (" {00:0} : {1:00}", minutes, seconds);
		return niceTime;
	} else
		return " 0:00";
	}
	
	public void ReplayButtonPressed()
	{
		if(levelComplete){
			Controller.GetInstance().currentlevel-=1;
			levelComplete=false;
			}
		UIGameObjectArray[(int)UIObjectList.replaybutton].GetComponent<RectTransform>().position=replayButtonIntialPos;
		Controller.GetInstance().ReplayButtonPressed();
	}
}
