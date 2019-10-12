using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{

	public ShipState ShipState;
	// Start is called before the first frame update
	void Start()
    {
		ShipState.Gameover += OnGameOver;
	}

	public virtual void OnGameOver()
	{ }

}
