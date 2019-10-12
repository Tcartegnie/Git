using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Ship
{
	public Transform target;
	public Canon canon;

	public void Start()
	{
		StartCoroutine(ShootPlayer());
	}
	// Update is called once per frame
	void Update()
    {
		LookAtTarget();
	}

	public void LookAtTarget()
	{
		transform.LookAt(target);
	}

	public void Shoot()
	{
		Debug.Log("Shoot");
		canon.Shoot(0);
	}

	public IEnumerator ShootPlayer()
	{
		Shoot();
		yield return new WaitForSeconds(canon.ShootCooldown);
		StartCoroutine(ShootPlayer());
	}

	public override void OnGameOver()
	{
		Destroy(gameObject);
	}

}
