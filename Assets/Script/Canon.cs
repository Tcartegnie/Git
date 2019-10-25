using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{

	public GameObject Bullet;
	public float ShootCooldown;
	public Transform CanonForward;
	bool CanShoot = true;

	public void TargetPoint(Vector3 Point)
	{
		transform.rotation = Quaternion.LookRotation(Point);
	}

	public void Shoot(float ShipSpeed)
	{
		if (CanShoot)
		{
			GameObject GO = Instantiate(Bullet, CanonForward.position, CanonForward.rotation);
			GO.GetComponent<Bullet>().speed += ShipSpeed;
			CanShoot = false;
			StartCoroutine(CoolDownShoot());
		}
	}

	public IEnumerator CoolDownShoot()
	{
		yield return new WaitForSeconds(ShootCooldown);
		CanShoot = true;
	}
}
