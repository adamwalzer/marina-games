using UnityEngine;
using System.Collections;

public abstract class Contacter : MonoBehaviour
{
	public abstract void onFallInBucket();
	public abstract void onFallPast();
}
