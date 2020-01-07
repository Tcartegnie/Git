using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRendering : MonoBehaviour
{
	public delegate void Visibility();

	public Visibility visibility;
	public Visibility Novisibility;

	private void OnBecameVisible()
	{
		Debug.Log("Im in the camera");
		visibility?.Invoke();
	}

	private void OnBecameInvisible()
	{
		Debug.Log("Im out of the camera");
		Novisibility?.Invoke();
	}



}
