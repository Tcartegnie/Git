using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{

	public GameObject Bullet;
	public float ShootCooldown;
	public float Damage;
	public float Speed;
	public Transform CanonForward;
	bool CanShoot = true;

	public void TargetPoint(Vector3 Point)
	{
		transform.rotation = Quaternion.LookRotation(Point);
	}

	public void Shoot()
	{
		if (CanShoot)
		{
			GameObject GO = Instantiate(Bullet, CanonForward.position, CanonForward.rotation);
			GO.GetComponent<Bullet>().Speed = Speed;
			GO.GetComponent<Bullet>().SetDamage(Damage);
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
