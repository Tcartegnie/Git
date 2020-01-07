using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
	public delegate void Visibility();
	public Visibility visibility;
	public Visibility Novisibility;
	public ShipState ShipState;
	// Start is called before the first frame update
	protected void Start()
    {
		ShipState.Gameover += OnGameOver;
	}

	public virtual void OnGameOver()
	{ }

	private void OnBecameVisible()
	{
		visibility?.Invoke();
	}

	private void OnBecameInvisible()
	{
		Novisibility?.Invoke();
	}
}
