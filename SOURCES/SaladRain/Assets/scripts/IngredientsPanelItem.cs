using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class IngredientsPanelItem : MonoBehaviour
{
	public Sprite[] steps;
	public Image icon;
	[HideInInspector]
	public List<string> ingredientNames;
	public GameObject hudText;
	public Color addColor;
	public Color removeColor;
	int meterStep;
	int count;
	int max;
	float colorParam;

	public void init (RecipeItem recipe, int step)
	{
		for (int i = 0; i < recipe.items.Length; i++) {		
			ingredientNames.Add (recipe.items [i].name);
		}
		icon.overrideSprite = recipe.icon;
		icon.SetNativeSize ();
		meterStep = step;
		count = 0;
		max = (steps.Length - 1) * step;
		colorParam = recipe.colorParam;
	}

	public void add ()
	{
		if(count >= max){return;}
		count++;
		int i;
		if(count > max){
			i = max / meterStep;
		}else{
			i = count / meterStep;
		}
		GetComponent<Image> ().overrideSprite = steps [i];
		showText(1);
		if(count == max){
			States.State<StateGame>().onRecipeFull(colorParam);
		}
	}

	public bool remove(){
		if(count == 0) return false;
		count --;
		int i;
		if(count > max){
			i = max / meterStep;
		}else{
			i = count / meterStep;
		}
		GetComponent<Image> ().overrideSprite = steps [i];
		showText(-1);
		return true;
	}

	public int currentCount{
		get{
			return count;
		}
	}

	public bool isFull{
		get{
			return count >= max;
		}
	}

	void showText(int count){
		GameObject go = Global.instance.pool.get(hudText, transform.position);
		go.GetComponent<HUDText>().init(count, count > 0 ? addColor : removeColor);
	}
}
