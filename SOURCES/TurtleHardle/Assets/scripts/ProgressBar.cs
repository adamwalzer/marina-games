using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBar : MonoBehaviour
{
	public RectTransform bar;
	public Text count;
	int max;
	int current;

	public void init(int currValue, int maxVal){
		current = currValue;
		max = maxVal;
		updateView();
	}

	public void changeValue (int newValue)
	{
		current = newValue;
		if (current > max) {
			current = max;
		} else if (current < 0) {
			current = 0;
		}
		updateView ();
	}

	void updateView ()
	{
		Vector2 v = bar.anchorMax;
		v.x = (float)current / max;
		bar.anchorMax = v;
		if(count != null){
			count.text = ((int)current).ToString();
		}
	}
}
