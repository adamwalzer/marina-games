using UnityEngine;
using System.Collections;

public class BonusePlace : MonoBehaviour
{
	Transform trfm;

	void Awake(){
		trfm = transform;
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector2(0.1f, 0.75f));
	}

	public Vector2 getPoint(){
		return trfm.position;
	}
}
