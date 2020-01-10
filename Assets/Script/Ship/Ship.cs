using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{

	public ShipState ShipState;
	// Start is called before the first frame update
	protected void Start()
    {
		GameManager GM = GameManager.instance;
		GM.onGamover += OnGameOver;
	}

	public virtual void OnGameOver()
	{ }

}
