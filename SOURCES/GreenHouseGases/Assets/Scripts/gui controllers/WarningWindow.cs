using UnityEngine;
using System;
using System.Collections;

public class WarningWindow : MonoBehaviour 
{
	Action onConfirm;
	Action onCancel;

	public void show(Action onConfirm_, Action onCancel_){
		onConfirm = onConfirm_;
		onCancel = onCancel_;
		gameObject.SetActive(true);
	}

	public void hide(){
		gameObject.SetActive(false);
	}

	public void onConfirmClick(){
		onConfirm();
	}

	public void onCancelClick(){
		onCancel();
	}
}
