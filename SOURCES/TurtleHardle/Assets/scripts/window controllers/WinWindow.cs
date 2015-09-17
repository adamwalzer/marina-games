using UnityEngine;
using System.Collections;

public class WinWindow : PopWindow 
{
	public override void show(){
		base.show();
	}
	
	public override void hide(){
		base.hide();
	}
	
	public void onOKClick(){
		States.State<PStateWin>().onCompleteGameClick();
	}
}
