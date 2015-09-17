using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBar : MonoBehaviour
{
	public Slider slider;
	public Image icon;
	public Sprite[] icons;

	public bool changeColor = false;
	public Color normalColor;
	public Color lowColor;
	public Image fillImage;

	int max;
	int current;

	public void init(int currValue, int maxVal, int characterIndex){
		current = currValue;
		max = maxVal;
		icon.sprite = icons[characterIndex];
		slider.value = current/(float)max;
	}

	public void changeValue (int newValue)
	{
		current = newValue;
		if (current > max) {
			current = max;
		} else if (current < 0) {
			current = 0;
		}
		slider.value = current/(float)max;
		if(changeColor){
			if(slider.value < 0.2f)
				fillImage.color = lowColor;
			else
				fillImage.color = normalColor;
		}
	}
}
