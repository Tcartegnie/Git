using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShoot : MonoBehaviour
{
	public GameObject target;
	public float RangeDetection;
	public float DistanceDetection;
	public List<Canon> Canons;
	public List<Turret> Turrets;


    // Update is called once per frame
    void Update()
    {
		if(CheckTargetInRange())
		{
			TurretsLookat();
			ShootEnnemy();
		}
		else
		{
			TurretStopShoot();
		}
	}

	public float GetTargetPositionInRange()
	{
		Vector3 pos = target.transform.position - transform.position;
		float DotProduct = Vector3.Dot(transform.forward, Vector3.Normalize(pos));
		return DotProduct;
	}

	public float GetDistanceFromTarget()
	{
		float distance = Vector3.Distance(target.transform.position, transform.position);
		return distance;
	}

	public bool CheckTargetInRange()
	{
		if ((GetTargetPositionInRange() > RangeDetection) && (GetDistanceFromTarget() < DistanceDetection))
			return true;
		else
			return false;
	}

	public void ShootEnnemy()
	{
		foreach(Canon canon in Canons)
		{
			canon.Shoot(0);
		}
	}

	public void TurretsLookat()
	{
		foreach (Turret turret in Turrets)
		{
			turret.LookAtTarget(target.transform);
			if(target.layer == 9)
			{
				turret.CallShoot();
			}
		}
	}

	public void TurretStopShoot()
	{
		foreach (Turret turret in Turrets)
		{
			turret.StopShoot();
		}
	}




}
