using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{

	public GameObject Bullet;
	public Transform CanonOffset;
	public float CoolDown;
	public float Damage;
	public float Speed;
	bool CanShoot = true;

	public void TargetPoint(Vector3 targetedpoint)
	{
		transform.rotation = Quaternion.LookRotation(targetedpoint);
	}

	public void Shoot()
	{
		if (CanShoot)
		{
			GameObject GO = Instantiate(Bullet, CanonOffset.position, CanonOffset.rotation);
			GO.GetComponent<Bullet>().Speed = Speed;
			GO.GetComponent<Bullet>().SetDamage(Damage);
			CanShoot = false;
			StartCoroutine(CoolDownShoot());
		}
	}


	public IEnumerator CoolDownShoot()
	{
		yield return new WaitForSeconds(CoolDown);
		CanShoot = true;
	}

}
