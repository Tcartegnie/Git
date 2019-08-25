using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpaceShipUI : MonoBehaviour
{

	public ShipController shipController;
		 

	public RectTransform ShipCursor;

	public Vector2 DistanceCursorMax;

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

		Debug.DrawRay(shipController.transform.position, GetCursorRay().direction * 1000, Color.red);
	} 

	public void MoveCursor(float X, float Y)
	{

		 Vector2 cursorPos = new Vector2(DistanceCursorMax.x * X, DistanceCursorMax.y * Y);

		ShipCursor.localPosition = cursorPos;

	}

	public Ray GetCursorRay()
	{
		return ShipController.currentCamera.ScreenPointToRay(ShipCursor.position);
	}

}
