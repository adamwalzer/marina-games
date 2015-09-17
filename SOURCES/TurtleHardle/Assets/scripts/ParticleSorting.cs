using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ParticleSorting : MonoBehaviour {
	public string layer = "Foreground";
	public int sortOrder;

	// Use this for initialization
	void Start () {
		Renderer pR = GetComponent<Renderer>();
		if(pR != null){
			pR.sortingLayerName = layer;
			pR.sortingOrder = sortOrder;
		}
//		else{
//			ParticleSystem pS = GetComponent<ParticleSystem>();
//			pS.renderer.sortingLayerName = layer;
//			pS.renderer.sortingOrder = sortOrder;
//		}
	}
}
