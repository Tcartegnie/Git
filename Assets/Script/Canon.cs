using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{

	public GameObject Bullet;
	public float ShootCooldown;
	public Transform CanonForward;
	public float Yoffset;

	public void TargetPoint(Vector3 Point)
	{

		//Debug.DrawRay(transform.position, Point * 1000 , Color.green);
		//Add an offset 
		transform.rotation = Quaternion.LookRotation(Point);
	}

	public void Shoot(float ShipSpeed)
	{
		GameObject GO = Instantiate(Bullet, CanonForward.position,transform.rotation);
		GO.GetComponent<Bullet>().speed += ShipSpeed;
	}
}
