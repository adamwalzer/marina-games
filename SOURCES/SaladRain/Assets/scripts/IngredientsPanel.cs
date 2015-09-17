using UnityEngine;
using System.Collections.Generic;

public class IngredientsPanel : MonoBehaviour 
{
	public GameObject itemPrefab;
	public int meterStep;

	List<IngredientsPanelItem> items;
	IngredientsPanelItem lastItem;

	public void init(Recipe recipe){
		if(items != null && items.Count > 0) deinit();

		items = new List<IngredientsPanelItem>();
		foreach(RecipeItem ri in recipe.items){
			createItem(ri);
		}
	}

	public void deinit(){
		foreach(IngredientsPanelItem ipi in items){
			Destroy(ipi.gameObject);
		}
		items.Clear();
		lastItem = null;
	}

	public void addIngredient(string title){
		foreach(IngredientsPanelItem ipi in items){
			if(ipi.ingredientNames.Find(item => item == title) != null){
				ipi.add();
				lastItem = ipi;
			}
		}
	}

	public void addJunk(){
		if(lastItem != null){
			if(!lastItem.remove()){
				IngredientsPanelItem ipi = items.Find(item => item.currentCount > 0);
				if(ipi != null){
					ipi.remove();
					lastItem = ipi;
				}
			}
		}
	}

	public bool isMissionCompleted(){
		return items.Find(item => !item.isFull) == null;
	}

	void createItem(RecipeItem recipe){
		GameObject go = Instantiate(itemPrefab) as GameObject;
		go.GetComponent<RectTransform>().SetParent(GetComponent<RectTransform>(), false);
		go.transform.localScale = Vector3.one;
		IngredientsPanelItem ipi = go.GetComponent<IngredientsPanelItem>();
		ipi.init(recipe, meterStep);
		items.Add(ipi);
	}
}
