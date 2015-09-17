using UnityEngine;
using System.Collections;

public class BonusePlace : MonoBehaviour
{
	Transform trfm;
//	BoxCollider2D area;

	void Awake(){
		trfm = transform;
//		area = GetComponent<BoxCollider2D>();
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector2(0.1f, 0.75f));
	}

	public Vector2 getPoint(){
//		float x = Random.Range(-area.bounds.extents.x, area.bounds.extents.x);
//		float y = Random.Range(-area.bounds.extents.y, area.bounds.extents.y);
//
//		return new Vector2(trfm.position.x + area.offset.x + x, trfm.position.y + area.offset.y + y);
		return trfm.position;
	}
}
