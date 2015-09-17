using UnityEngine;
using System;
using System.Collections;

public class WarningWindow : PopWindow
{
	Action _onCancel;
	Action _onConfirm;

	public void show(Action onCancel_, Action onConfirm_){
		_onCancel = onCancel_;
		_onConfirm = onConfirm_;
		gameObject.SetActive(true);
	}

	public void onQuitClick(){
		if(_onConfirm != null){
			_onConfirm();
		}
	}

	public void onCancelClick(){
		if(_onCancel != null){
			_onCancel();
		}
	}
}
