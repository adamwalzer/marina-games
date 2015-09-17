using UnityEngine;
using System.Collections;

// state to load state scene
public class LoadSceneState : BaseState
{
	public bool asyncLoad;
	public float minStep = 0.01f;
	public float maxStep = 0.05f;

	[HideInInspector]
	public BaseState
		stateToLoad;

	protected virtual void init() {

	}
	protected virtual void setProgress(float progress) {
	}
	protected virtual void loadingCompleted() {
		Resources.UnloadUnusedAssets();
		StartCoroutine(changeStateAtNextFrame());
	}
	IEnumerator changeStateAtNextFrame() {
		yield return null;
		States.Change(stateToLoad);
	}

	public override void OnEnter(){
		base.OnEnter();
		if (States.IsLogEnabled()) {
			Debug.Log(string.Format("[LoadSceneState] Loading scene \"{0}\" for state {1} ({2})", stateToLoad.stateScene, stateToLoad, asyncLoad ? "async mode" : "sync mode"));
		}
		
		if (!asyncLoad) {
			StartCoroutine(LoadSceneStateOnNextFrame(stateToLoad));
		} else {
			StartCoroutine(AsyncLoadStateScene(stateToLoad));
		}
	}
	
	public override void OnExit(){
		base.OnExit();
	}

	IEnumerator LoadSceneStateOnNextFrame(BaseState state) {
		Application.LoadLevel(state.stateScene);
		yield return null;
		States.Change(state);
	}
	
	IEnumerator AsyncLoadStateScene(BaseState state)
	{
		Application.LoadLevel(stateScene);

		yield return null;

		init();

		// 2) init progress bar
		setProgress(0f);
		
		// 3) start async loading
		AsyncOperation aop = Application.LoadLevelAsync(state.stateScene);
		
		if (aop != null) {
			float curProgress = 0;
			float oldProgress = 0;
			
			// 4) wait loading complete
			while (aop != null && !aop.isDone || curProgress < 1) {
				float loadProgress = aop.progress;

				float fakeProgress = curProgress + minStep;
				
				float newP = curProgress;
				if (loadProgress > fakeProgress || loadProgress > oldProgress) {
					newP = loadProgress;
				} else {
					if (fakeProgress < 1.0f) {
						newP = fakeProgress;
					}
				}
				
				// РѕРіСЂР°РЅРёС‡РёРј СЃРІРµСЂС…Сѓ
				if ((newP - curProgress) > maxStep)
					newP = curProgress + maxStep;
				
				curProgress = newP;			
				
				setProgress(curProgress);
				oldProgress = curProgress;
				
				yield return null; // wait one frame
			}
			aop = null;
		}

		setProgress(1f);
		loadingCompleted();
	}
}

