using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpaceShipUI : MonoBehaviour
{

	public ShipMovement shipController;
	public ShipState stateShip;

	public Canvas SpaceshipUI;

	public RectTransform ShipCursor;

	public Vector2 DistanceCursorMax;

	public RectTransform SpeedBarMask;

	public RectTransform LifeBarMask;

	public RectTransform ShieldBarMask;

	public RectTransform CircleTarget;

	public RectTransform TargetLine;
	public RectTransform ShipCenterLandmark;

	public float MinDistanceFromeTarget;

	public Text SpeedText;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		stateShip = shipController.ShipState;
		GameManager GM = GameManager.instance;	
		GM.onGamover += GameOver;
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
			ScopeCloseEnouhtToEnabeleAutomatedShoot();
		}


		
		SetLineTarget();
		SetLineRotation();
		//if (stateShip.targetLocked != null)
		//{
		//	SetLinePosition();
		//}
	
		DisplaySpeedInText();
		//Debug.DrawRay(shipController.transform.position, GetCursorRay().direction * 1000, Color.red);
	} 


	public void SetInterfaceVisibility(bool value)
	{
		SetEnableCircleTarget(value);
		SetEnableTargetLine(value);
	}

	public void SetEnableCircleTarget(bool value)
	{
		CircleTarget.gameObject.SetActive(value);
	}
	public void SetEnableTargetLine(bool value)
	{
		TargetLine.gameObject.SetActive(value);
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

	public Ray GetCursorRay()
	{
		return ShipMovement.CurrentCamera.ScreenPointToRay(ShipCursor.position);
	}


	public void SetSpeedBar()
	{
		float NormalizedVelocity = shipController.GetNormalizedVelocity();
	//	Debug.Log(NormalizedVelocity);
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


	public void SetPredicatedLock(Vector3 PredicatedPosition)
	{
		Camera currentcamera = ShipMovement.CurrentCamera;
		Vector3 predicatedPositionOnScreen = currentcamera.WorldToScreenPoint(PredicatedPosition);
		CircleTarget.position = predicatedPositionOnScreen;
	}

	public void ResetPredicatedLock()
	{
		CircleTarget.localPosition = Vector3.zero;
	}

	public void SetLinePosition()
	{
		Camera currentcamera = ShipMovement.CurrentCamera;
		Vector3 predicatedPositionOnScreen = currentcamera.WorldToScreenPoint(stateShip.targetLocked.transform.position);
		TargetLine.localPosition = predicatedPositionOnScreen;//A REFACTO
	}

	public void SetLineTarget()
	{
		//Vector3 [] TargetsLine = new Vector3[] { new Vector3(CircleTarget.position.x,0,CircleTarget.position.y), new Vector3(ShipCenterLandmark.position.x,0,ShipCenterLandmark.position.y) };
		//TargetLine.localPosition = Camera.current.
		float DistanceFromTarget = Vector2.Distance(CircleTarget.localPosition, ShipCenterLandmark.localPosition);
		TargetLine.sizeDelta = new Vector2(DistanceFromTarget,TargetLine.sizeDelta.y);
	}

	public void SetLineRotation()
	{
		Vector2 TargetPosition = new Vector2((CircleTarget.position.x - ShipCenterLandmark.position.x), (CircleTarget.position.y - ShipCenterLandmark.position.y));
		TargetLine.rotation = Quaternion.FromToRotation(Vector3.right, TargetPosition);
	}
		
	public float GetScopeDistanceFromeTarget()
	{
		return Vector2.Distance(ShipCursor.localPosition, CircleTarget.localPosition);
	}

	public void ScopeCloseEnouhtToEnabeleAutomatedShoot()
	{
	//	Debug.Log(GetScopeDistanceFromeTarget());
		if (GetScopeDistanceFromeTarget() < MinDistanceFromeTarget)
		{
			stateShip.AutomatedShootEnable = true;
		}
		else
			stateShip.AutomatedShootEnable = false;
	}

	public void DisplaySpeedInText()
	{
		SpeedText.text = (int)shipController.GetVeloctiyMagnitude() + "/" + shipController.MaxSpeed + "Kmh"; 
	}




}
