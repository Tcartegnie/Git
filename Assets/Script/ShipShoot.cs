using UnityEngine;
using UnityEditor;

public class ShipShoot : ScriptableObject
{
	public Canon LeftCanon;
	public Canon RightCanon;
	public void upadte()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Shoot();
		}
		RotateCanon(new Vector3());
	}


	public void Shoot()
	{
		LeftCanon.Shoot(0);
		RightCanon.Shoot(0);
	}


	public void RotateCanon(Vector3 Direction)
	{
		//Vector3 RayDirection = shpashipUI.GetCursorRay().direction;
		//		Vector3 TargetPos = transform.position + (RayDirection * CrossPointDistance);



		LeftCanon.TargetPoint(Direction);
		RightCanon.TargetPoint(Direction);
	}

}


