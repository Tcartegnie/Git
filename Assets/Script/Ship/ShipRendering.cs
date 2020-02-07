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
		visibility?.Invoke();
	}

	private void OnBecameInvisible()
	{
		Novisibility?.Invoke();
	}



}
