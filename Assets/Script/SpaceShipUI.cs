using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpaceShipUI : MonoBehaviour
{

	public ShipController shipController;
	public ShipState stateShip;

	public Canvas SpaceshipUI;

	public RectTransform ShipCursor;

	public Vector2 DistanceCursorMax;

	public RectTransform SpeedBarMask;

	public RectTransform LifeBarMask;

	public RectTransform ShieldBarMask;

	

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		stateShip = shipController.ShipState;
		stateShip.Gameover += GameOver;
	}

	// Update is called once per frame
	void Update()
    {
		if (shipController != null)
		{
			Vector2 mouseAxis = shipController.GetRotationAxis();

			MoveCursor(mouseAxis.x, mouseAxis.y);

			SetSpeedBar();
			SetLifeBar();
			SetShieldBar();
		}
		//Debug.DrawRay(shipController.transform.position, GetCursorRay().direction * 1000, Color.red);
	} 

	public void GameOver()
	{
		TurnOnOffDebugScreen(!SpaceshipUI.enabled);
	}

	public void TurnOnOffDebugScreen(bool State)
	{
		SpaceshipUI.enabled = State;
	}


	public void MoveCursor(float X, float Y)
	{

		 Vector2 cursorPos = new Vector2(DistanceCursorMax.x * X, DistanceCursorMax.y * Y);

		ShipCursor.localPosition = cursorPos;

	}

	public Ray GetCursorRay()//This must go in the part of the script
	{
		return ShipController.CurrentCamera.ScreenPointToRay(ShipCursor.position);
	}


	public void SetSpeedBar()
	{
		float NormalizedVelocity = shipController.GetNormalizedVelocity();
		//Debug.Log(NormalizedVelocity);
		SpeedBarMask.anchorMax = new Vector2(NormalizedVelocity,1);
	}


	public void SetLifeBar()
	{
		float NormalizedLife = stateShip.GetNormalizedLife();
		LifeBarMask.anchorMax = new Vector2(NormalizedLife, 0.5f);
	}

	public void SetShieldBar()
	{
		float NormalizedShield = stateShip.GetNormalizedShield();
		ShieldBarMask.anchorMax = new Vector2(NormalizedShield, 0.5f);
	}

}
