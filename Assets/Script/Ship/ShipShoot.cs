using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ShipShoot : Ship
{

	public List<Canon> canons;
	public SpaceShipUI spaceShipUI;

	public void Update()
	{
		if (!ShipState.IsGameover)
		{
			if (Input.GetMouseButtonDown(0) )
			{
				LookFOrTarget();
				Shoot();
			}

			if (ShipState.targetLocked != null )
			{
				spaceShipUI.SetPredicatedLock(PredicateTargetPosition());
			}
			else
			{
				OnTargetLost();
			}

			RotateCanon();
		}
	}

	public void OnTargetLost()
	{
		ShipState.AutomatedShootEnable = false;
		spaceShipUI.ResetPredicatedLock();
		spaceShipUI.SetInterfaceVisibility(false);
	}//In another script ?

	public void OnTargetLocked()
	{
		spaceShipUI.ResetPredicatedLock();
		spaceShipUI.SetInterfaceVisibility(true);
	}

	public void LookFOrTarget()
	{
		RaycastHit rayHit = new RaycastHit();
		Ray ray = new Ray(transform.position, spaceShipUI.GetCursorRay().direction);
		if(Physics.Raycast(ray,out rayHit))
		{
			GameObject target = rayHit.collider.gameObject;

			if (target.GetComponentInParent<ShipState>() != null)
			{
				ShipState.TargetLocked = target;
				OnTargetLocked();
				Debug.Log("Target is : " + target.name);
				ShipState stat = target.GetComponentInParent<ShipState>();
				stat.visibility += OnTargetLocked;
				stat.Novisibility += OnTargetLost;
			}
		}

	}//I another script ?

	public float GetTimeToReachTarget()
	{
		if(ShipState.TargetLocked != null)
		{
			foreach (Canon canon in canons)
			{
				float DistanceFromTarget = Vector2.Distance(canon.transform.position,ShipState.TargetLocked.transform.position);
				float TimeToReachTarget = DistanceFromTarget / (canon.Speed);
				//Debug.Log("For a distance of : " + DistanceFromTarget + " the will take : " + TimeToReachTarget + " : Secondes");
				return TimeToReachTarget;
			}
		}
		return 0.0f;
	}

	public Vector3 PredicateTargetPosition()
	{
		Vector3 predicatedPosition = new Vector3();


		if (ShipState.TargetLocked != null)
		{
			GameObject targetLocked = ShipState.TargetLocked;
			predicatedPosition = targetLocked.transform.position + (targetLocked.GetComponentInParent<Rigidbody>().velocity * ( GetTimeToReachTarget()));
			//Debug.DrawLine(transform.position, predicatedPosition, Color.red);
		}
		return predicatedPosition;
	}

	public void Shoot()
	{
		foreach(Canon canon in canons)
		{
			canon.Shoot();
		}
	}

	public Vector3 GetExactePredicationShoot()
	{
		Vector3 heading = PredicateTargetPosition() - transform.position;
		float distance = heading.magnitude;
		return (heading / distance);
	}

	public void RotateCanon()
	{
		Vector3 RayDirection = new Vector3();
		if (ShipState.AutomatedShootEnable)
		{
			RayDirection = GetExactePredicationShoot();
		}
		else
		{
			RayDirection = spaceShipUI.GetCursorRay().direction;
			//		Vector3 TargetPos = transform.position + (RayDirection * CrossPointDistance);
		}

		foreach (Canon canon in canons)
		{
			canon.TargetPoint(RayDirection);
		}
	}

}


