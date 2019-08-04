using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugScreen : MonoBehaviour
{
	public ShipController Currentship;
	public Text SpeedShip;


    // Update is called once per frame
    void Update()
    {
		SpeedShip.text = "Speed : " + Currentship.GetVelocity();
	}
}
