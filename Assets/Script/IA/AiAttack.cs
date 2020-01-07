using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAttack : MonoBehaviour
{
	public GameObject target;

	public List<Turret> Turrets;
	public List<Canon> Canons;
	public float DistanceShoot;

	public float GetTargetPositionInRange(Vector3 DirectionToWatch, Vector3 TargetPosition)
	{
		Vector3 TargetDirection = TargetPosition - transform.position;
		float DotProduct = Vector3.Dot(DirectionToWatch, Vector3.Normalize(TargetDirection));
		return DotProduct;
	}



	public float GetDistanceFromTarget(Vector3 TargetPosition)
	{
		float distance = Vector3.Distance(TargetPosition, transform.position);
		return distance;
	}

	public void SetTurretTarget()
	{
		foreach(Turret turret in Turrets )
		{
			turret.SetTarget(target);
		}
	}


	public void Update()
	{
		if (target != null && IsTargetInRange())
		{
			foreach (Canon canon in Canons)
			{
				canon.Shoot();
			}
		}
	}

	public bool IsTargetInRange()
	{
		if(GetTargetPositionInRange(transform.forward,target.transform.position) > 0.8f &&  GetDistanceFromTarget(target.transform.position) < DistanceShoot)
		{
			//Debug.Log("CanShoot");
			return true;
		}
		return false;
	}


	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			target = other.gameObject;
			SetTurretTarget();

		}
	}


	public void OnTriggerExit(Collider other)
	{
		//Retours au mode patrouille
	}

}
