﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugScreen : MonoBehaviour
{

	public	Canvas DebugCanvas;
	public ShipMovement Currentship;
	public Text SpeedShip;
	public Text VelocityShip;
	public Text ScreenWorldPos;
	public Text ShieldEnable;

	string [] EnableDisable = { "Eneable", "Diseable" };

    // Update is called once per frame
    void Update()
    {
		if (Currentship != null)
		{
			SpeedShip.text = "Speed : " + Currentship.GetSpeed();

			VelocityShip.text = "Velocity : " + Currentship.GetVelocity().magnitude;
		}

		if (Input.GetKeyDown(KeyCode.P))
		{
			TurnOnOffDebugScreen(!DebugCanvas.enabled);
		}

		if(Input.GetKeyDown(KeyCode.X))
		{
			Debug.Break();
		}

	}


	public void TurnOnOffDebugScreen(bool State)
	{
		DebugCanvas.enabled = State;
	}

}
