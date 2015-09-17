using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ParticleSorting : MonoBehaviour {
	public string layer = "Foreground";

	// Use this for initialization
	void Start () {
		ParticleRenderer pR = GetComponent<ParticleRenderer>();
		if(pR != null){
			pR.GetComponent<Renderer>().sortingLayerName = layer;
		}else{
			ParticleSystem pS = GetComponent<ParticleSystem>();
			pS.GetComponent<Renderer>().sortingLayerName = layer;
		}

	}
}
