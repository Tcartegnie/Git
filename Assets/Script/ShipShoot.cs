using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ShipShoot : Ship
{

	public List<Canon> canons;
	public SpaceShipUI spaceShipUI;
	public void Update()
	{
	
		if (Input.GetMouseButtonDown(0))
		{
			LookFOrTarget();
			Shoot();
		}

		if(ShipState.targetLocked != null)
		{
			spaceShipUI.SetPredicatedLock(PredicateTargetPosition());
		}

		RotateCanon();
	}

	public void LookFOrTarget()
	{
		RaycastHit rayHit = new RaycastHit();
		Ray ray = new Ray(transform.position, spaceShipUI.GetCursorRay().direction);
		if(Physics.Raycast(ray,out rayHit))
		{
			if(rayHit.collider.GetComponent<Ship>() != null)
			{
				Debug.Log("Target is : " + rayHit.collider.name);
				ShipState.TargetLocked = rayHit.collider.gameObject;
			}
		}

	}

	public float GetTimeToReachTarget()
	{
		if(ShipState.TargetLocked != null)
		{
			foreach (Canon canon in canons)
			{
				float DistanceFromTarget = Vector2.Distance(canon.transform.position,ShipState.TargetLocked.transform.position);
				float TimeToReachTarget = DistanceFromTarget / canon.Speed;
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
			predicatedPosition = targetLocked.transform.position + (targetLocked.GetComponent<Rigidbody>().velocity * ( GetTimeToReachTarget()));
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


	public void RotateCanon()
	{
		Vector3 RayDirection = spaceShipUI.GetCursorRay().direction;
		//		Vector3 TargetPos = transform.position + (RayDirection * CrossPointDistance);
		Vector3 heading = PredicateTargetPosition() - transform.position;

		float distance = heading.magnitude;
		Vector3 direction = heading / distance;

		foreach (Canon canon in canons)
		{
			canon.TargetPoint(RayDirection);
		}
	}

}


