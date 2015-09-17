using UnityEngine;
using System.Collections;

public class BackgroundTransporter : MonoBehaviour 
{
	public Transform part1;
	public Transform part2;
	public Transform cam;
	public SpriteRenderer spr1;
	public SpriteRenderer spr2;
	public Sprite daySprite;
	public Sprite nightSprite;

	float partWidth;
	Transform left;
	Transform right;
	bool isInited = false;



	void Awake(){
//		partWidth = 2 * part1.GetComponent<SpriteRenderer>().bounds.extents.x - 0.1f;
	}

//	void Start(){
//		init();
//	}


	void Update(){
		if(!isInited) return;
		if(cam.position.x - left.position.x >= partWidth){
			switchParts();
		}
	}

	public void init(bool day){
		left = part1;
		right = part2;
		if(day){
			spr1.sprite = daySprite;
			spr2.sprite = daySprite;
		}else{
			spr1.sprite = nightSprite;
			spr2.sprite = nightSprite;
		}
		partWidth = 2 * part1.GetComponent<SpriteRenderer>().bounds.extents.x - 0.1f;
		left.position = Vector3.zero;
		right.position = left.position + Vector3.right * partWidth;
		isInited = true;

	}

	void switchParts(){
		if(left == part1){
			left = part2;
			right = part1;
		}else{
			left = part1;
			right = part2;
		}
		right.position = left.position + Vector3.right * partWidth;
	}
}
