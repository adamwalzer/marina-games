using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndLevelWindow : MonoBehaviour
{
	public Text score;
	public Text time;
	public Text interestText;
	public Text cash;
	public Text interest;
	public int animTime = 2;
	public float animStep = 0.1f;
	public AudioClip counterSound;
	public ParticleSystem interestPS;
	public Animator interestAnimator;
	StateGame sG;

	bool readyToGo = false;

	public void show ()
	{
		sG = States.State<StateGame>();
		time.text = sG.getTimeResult;
		score.text = sG.scores.ToString();
		readyToGo = false; 
		cash.text = sG.scores.ToString ();
		interest.text = sG.scores.ToString ();
		gameObject.SetActive (true);
		if (sG.currentLevel == 1) {
			interestText.text = "Smart Apple" + "\n " + "Savers" + "\n" + "Account";
		} else {
			interestText.text = "PLUS CD with" + "\n" + ".25% Interest";
		}
		StartCoroutine(wait());
	}
	
	public void hide ()
	{
		StopCoroutine("animate");
		gameObject.SetActive (false);
	}
	
	public void onNextClick(){
		if(!readyToGo) return;
		States.State<PStateEndLevel>().onNextClick();
	}
	
	public void onReplayClick(){
		if(!readyToGo) return;
		States.State<PStateEndLevel>().onReplayClick();
	}

	public void onAnimationEnded(){
		StartCoroutine("animate");
	}

	IEnumerator wait(){
		yield return new WaitForSeconds(0.5f);
		readyToGo = true;
//		interestAnimator.ResetTrigger ("Show");
	}

	IEnumerator animate(){
		int val = sG.scores;
		int start = val;
		int dif = (int)(val * (sG.currentLevel == 1 ? 0.2f : 0.25f));
		int d = (int)(dif / animTime * animStep);
		bool anim = true;
		interestPS.Play ();
		States.State<StateGame> ().addScore (start + dif);
		while (anim) {
			val += d;
			interest.text = val.ToString();
			anim = val < start + dif;
			Global.instance.soundManager.playSound(counterSound);
			yield return new WaitForSeconds (animStep);
		}
		interestPS.Stop ();
	}
}
