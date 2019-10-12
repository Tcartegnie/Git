using UnityEngine;
using UnityEditor;

public class ShipShoot : MonoBehaviour
{
	public Canon LeftCanon;
	public Canon RightCanon;
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
		LeftCanon.Shoot(0);
		RightCanon.Shoot(0);
	}


	public void RotateCanon()
	{
		Vector3 RayDirection = spaceShip.GetCursorRay().direction;
		//		Vector3 TargetPos = transform.position + (RayDirection * CrossPointDistance);



		LeftCanon.TargetPoint(RayDirection);
		RightCanon.TargetPoint(RayDirection);
	}

}


