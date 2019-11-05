using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Ship
{
	public Canon canon;

	public void LookAtTarget(Transform target)
	{
		//Faire de la predicate pour essayé d'ancitiper le target du joueur
		transform.LookAt(target);
	}

	public void Shoot()
	{
		canon.Shoot();
	}

	public void CallShoot()
	{
		StartCoroutine(StartShoot());
	}

	public IEnumerator StartShoot()
	{
		Shoot();
		yield return new WaitForSeconds(canon.ShootCooldown);
		StartCoroutine(StartShoot());
	}

	public void StopShoot()
	{
		StopCoroutine(StartShoot());
	}

	public override void OnGameOver()
	{
		Destroy(gameObject);
	}

}
 