using UnityEngine;
using System;

public class WarningWindow : PopWindow
{
	Action onQuit;
	Action onCancel;

	public void show(Action onQuit_, Action onCancel_){
		base.show();
		onQuit = onQuit_;
		onCancel = onCancel_;
	}

	public void onQuitClick(){
		onQuit();
	}

	public void onCancelClick(){
		onCancel();
	}
}
