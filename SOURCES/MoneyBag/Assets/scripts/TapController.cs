using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class TapController : MonoBehaviour, IPointerDownHandler, IDragHandler
{
	public Camera cam;
	public Saladbowl saladbowl;

	float xSize;
	bool playing = false;

	void Start(){
		xSize = -cam.ScreenToWorldPoint(Vector2.zero).x - 1;
	}

	void Update(){
		if(!playing) return;
		saladbowl.move(getMouseX());
	}

	public void play(){
		playing = true;
	}

	public void stop(){
		playing = false;
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		if(!playing) return;
		saladbowl.move(getMouseX());
	}

	public void OnDrag (PointerEventData eventData)
	{
		if(!playing) return;
		saladbowl.move(getMouseX());
	}

	float getMouseX(){
		float x = cam.ScreenToWorldPoint(Input.mousePosition).x;
		if(x < -xSize){
			x = -xSize;
		}else if(x > xSize){
			x = xSize;
		}
		return x;
	}
}
