using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public Transform target;
	public float freeMoveX = 3;
	public float freeMoveY = 1;
	public float speed = 10f;
	public float forsage = 0.3f;
	public Transform borders;
	float currentSpeed;
	
	Vector3 startPos;
	Vector3 currentPos;
	Transform trfm;

	void Awake(){
		startPos = transform.position;
		currentPos = startPos;
		trfm = transform;
	}

	void Update () 
	{
		if(target == null) return;
		float dPlayerPos = transform.position.x - target.position.x;
		if(dPlayerPos > freeMoveX){
			//геройчик слева
			currentPos.x = target.transform.position.x  + freeMoveX/3;
		}else if(dPlayerPos < -freeMoveX){
			//геройчик справа
			currentPos.x = target.transform.position.x  - freeMoveX;
		}

//		if(Mathf.Abs(dPlayerPos) > freeMoveX / 2){
//			currentSpeed = forsage * Time.deltaTime * 1000f;
//		}else{
//			currentSpeed = speed * Time.deltaTime * 1000f;
//		}
//
//		dPlayerPos = transform.position.y - target.position.y;
//		if(dPlayerPos > freeMoveY){
//			//геройчик слева
//			currentPos.y = target.transform.position.y  + freeMoveY;
//		}else if(dPlayerPos < -freeMoveY){
//			//геройчик справа
//			currentPos.y = target.transform.position.y  - freeMoveY;
//		}

//		Vector2 pos = Vector2.MoveTowards(transform.position, currentPos, currentSpeed);
//		currentPos = pos;
		currentPos.z = startPos.z;
		currentPos.y = startPos.y;
		transform.position = currentPos;
		Vector2 bV = borders.position;
		bV.x = trfm.position.x;
		borders.position = bV;
	}

	public void restart(LevelSettings settings){
		transform.position = startPos;
	}
}
