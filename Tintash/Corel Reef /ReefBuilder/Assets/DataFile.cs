using UnityEngine;
using System.Collections;

public  class DataFile  {

	public static float width 	= 2.05f;//2.0203f;///* 1.74758f;//*/1.415f;//1.67f;//1.73f;
	public static float height 	= 2.05f;//*1.413087f;//*/1.14705f;//1.14755f;//1.35f;//1.36f;-3.7975
	
	public static float min = 2.0f;
	public static float sec = 30f;

	public static int 	rows	= 07;
	public static int	cols	= 10;//12;
	
	public static Vector3 statingPos = new Vector3 ( -10.065f,-5.765f,0);//-9.7125f,-6.21455f,0f);///*( -10.02f,-4.673f,0f);//*/(-10.02f,-4.94455f,0f);//Vector3(-9.645f,-4.94455f,0f);// Vector3(-9.5f,-4.839f,0f);
	
	public static float CorealSpeed = 450f;
	
	public static	int squirrel		=	550;
	public static	int ant		=	750;
	public static	int rat 		=	750;
	public static	int snake		=	850;
	public static	int frog		=	850;
	public static	int gem  		=   1000;
	public static	int barrel	=	1000;
	public static	int crystal			=	2000;
	public static	int stoneScore		=	150;
	public static 	int heartScore			=	4;
	public static 	int GoldScore			=	3;
	
	public  enum SoundAssest
	{
		
		gameComplete=15,
	}
	;
}
