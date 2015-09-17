using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour 
{
	public Vector2 sizeRange = new Vector2(0.75f, 1.25f);

//	void OnTriggerEnter2D(Collider2D c){
//		if(c.gameObject.tag == "Player"){
//			c.gameObject.GetComponent<Flyer>().slowDown(transform.position);
//		}
//	}

	public void init(LevelSettings settings_){
		place();
	}

	void place(){
		SpriteRenderer s = GetComponent<SpriteRenderer>();
		float w1 = s.bounds.extents.y;
		transform.localScale = Vector3.one * Random.Range(sizeRange.x, sizeRange.y);
		float w2 = s.bounds.extents.y;
		Vector2 pxPos = Camera.main.WorldToScreenPoint(transform.position);
		pxPos.y -= (w1-w2)/2;
		Vector2 wPos = Camera.main.ScreenToWorldPoint(pxPos);
		transform.position = wPos;
	}
}
