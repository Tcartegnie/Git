using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ShipShoot : Ship
{

	public List<Canon> canons;
	public SpaceShipUI spaceShip;
	public void Update()
	{
		LookFOrTarget();
		PredicateTargetPosition();
		if (Input.GetMouseButtonDown(0))
		{		
			Shoot();
		}
		RotateCanon();
	}

	public void LookFOrTarget()
	{
		RaycastHit rayHit = new RaycastHit();
		Ray ray = new Ray(transform.position, spaceShip.GetCursorRay().direction);
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
				return TimeToReachTarget;
			}
		}
		return 0.0f;
	}

	public void PredicateTargetPosition()
	{
		
		if (ShipState.TargetLocked != null)
		{
			GameObject targetLocked = ShipState.TargetLocked;
			Vector3 predicatedPosition = targetLocked.transform.position + (targetLocked.GetComponent<Rigidbody>().velocity * ( GetTimeToReachTarget()));
			Debug.DrawLine(transform.position, predicatedPosition, Color.red);
		}
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
		Vector3 RayDirection = spaceShip.GetCursorRay().direction;
		//		Vector3 TargetPos = transform.position + (RayDirection * CrossPointDistance);


		foreach (Canon canon in canons)
		{
			canon.TargetPoint(RayDirection);
		}
	}

}


