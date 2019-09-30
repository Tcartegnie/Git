using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpaceShipUI : MonoBehaviour
{

	public ShipController shipController;
		 

	public RectTransform ShipCursor;

	public Vector2 DistanceCursorMax;

	public RectTransform SpeedBarMask;

	public RectTransform LifeBarMask;

	public RectTransform ShieldBarMask;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	// Update is called once per frame
	void Update()
    {
		Vector2 mouseAxis = shipController.GetRotationAxis();

		MoveCursor(mouseAxis.x, mouseAxis.y);

		SetSpeedBar();
		SetLifeBar();
		SetShieldBar();
		//Debug.DrawRay(shipController.transform.position, GetCursorRay().direction * 1000, Color.red);
	} 

	public void MoveCursor(float X, float Y)
	{

		 Vector2 cursorPos = new Vector2(DistanceCursorMax.x * X, DistanceCursorMax.y * Y);

		ShipCursor.localPosition = cursorPos;

	}

	public Ray GetCursorRay()//This must go in the part of the script
	{
		return ShipController.currentCamera.ScreenPointToRay(ShipCursor.position);
	}


	public void SetSpeedBar()
	{
		float NormalizedVelocity = shipController.GetNormalizedVelocity();
		//Debug.Log(NormalizedVelocity);
		SpeedBarMask.anchorMax = new Vector2(NormalizedVelocity,1);
	}


	public void SetLifeBar()
	{
		float NormalizedLife =	shipController.GetNormalizedLife();
		LifeBarMask.anchorMax = new Vector2(NormalizedLife, 0.5f);
	}

	public void SetShieldBar()
	{
		float NormalizedShield = shipController.GetNormalizedShield();
		ShieldBarMask.anchorMax = new Vector2(NormalizedShield, 0.5f);
	}

}
