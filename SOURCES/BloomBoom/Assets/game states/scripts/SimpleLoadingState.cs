using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SimpleLoadingState : LoadSceneState {
	private GameObject progressBar;
	private Image progressBarSprite;

	protected override void init(){
		progressBar = GameObject.Find("progress").gameObject;
		DontDestroyOnLoad(progressBar.transform.parent.parent);
		progressBarSprite = progressBar.GetComponent<Image>();
		
		progressBarSprite.fillAmount = 0f;
	}

	public override void OnExit(){
		base.OnExit();
		progressBar = null;
		progressBarSprite = null;
	}

	protected override void setProgress (float progress)
	{
		progressBarSprite.fillAmount = progress;
	}

	protected override void loadingCompleted ()
	{
		StartCoroutine(changeStateAtNextFrame());
	}
	IEnumerator changeStateAtNextFrame() {
		yield return null;
		Destroy (progressBar.transform.parent.parent.gameObject);
		Resources.UnloadUnusedAssets();
		States.Change(stateToLoad);
	}

}
