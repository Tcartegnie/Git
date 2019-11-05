using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ShipShoot : MonoBehaviour
{

	public List<Canon> canons;
	public SpaceShipUI spaceShip;
	public void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Shoot();
		}
		RotateCanon();
	}


	public void Shoot()
	{
		foreach(Canon canon in canons)
		{
			canon.Shoot(0);
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


