using UnityEngine;
using System.Collections;

public class MovingBackground : MonoBehaviour 
{
	public Transform neighbour;
	Vector3 halfWidth;
	Vector3 startPos;
	Vector2 grassDencity;
	bool isVis = false;

	void Awake(){
		halfWidth.x = 2 * GetComponent<SpriteRenderer>().bounds.extents.x;
		startPos = transform.position;
//		grass = GetComponent<BackgroundGrass>();
	}

	void OnBecameVisible(){
		isVis = true;
		neighbour.position = transform.position + halfWidth;
		neighbour.GetComponent<MovingBackground>().init();
	}

	void OnBecameInvisible(){
		isVis = false;
//		if(grass != null){
//			grass.deinit();
//		}
	}

	public void init(){
//		if(grass != null){
//			grassDencity = States.State<StateGame>().settings.plantsDencity;
//			grass.init((int)Random.Range(grassDencity.x, grassDencity.y));
//		}
	}

	public void reset(){
//		if(grass != null){
//			grass.deinit();
//		}
		transform.position = startPos;
		if(isVis){
			neighbour.position = transform.position + halfWidth;
			neighbour.GetComponent<MovingBackground>().init();
		}
	}
}
