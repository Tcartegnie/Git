using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separation : Steering
{

	/*
	- Get the distance from target 
	*/
	float DIstanceOfAvoiding;
	List<Transform> Targets;
	public Separation(Transform entityTr, Transform targetTr, Rigidbody RB, float MaxSpeed, float Wheight, float DistanceOfAvoiding) : base(entityTr, targetTr, RB, MaxSpeed, Wheight)
	{
		Targets = new List<Transform>();
		DIstanceOfAvoiding = DistanceOfAvoiding;
	}

	public override Vector3 GetSteering()
	{
		return ComputSteering();
	}

	public void AddTarget(Transform target)
	{
		Targets.Add(target);
	}

	public void RemoveTarget(Transform target)
	{
		Targets.Remove(target);
	}

	public Vector3 ComputSteering()
	{
		Vector3 StrengeRepulsion = new Vector3();


		foreach (Transform target in Targets)
		{
			float Distance = Vector3.Distance(EntityTr.position,target.position);

			if (Distance < DIstanceOfAvoiding)
			{
				StrengeRepulsion += GetRepulsion(target.position,Distance);
			}
		}

		Debug.DrawLine(EntityTr.position, EntityTr.position + StrengeRepulsion * MaxSpeed);

		return StrengeRepulsion;
	}

	public Vector3 GetRepulsion(Vector3 targetPosition, float DistanceFromTarget)
	{
		Vector3 EntityDirection = EntityTr.position - targetPosition;
		EntityDirection.Normalize();
		return (EntityDirection * ( MaxSpeed * (1 * DistanceFromTarget)));
	}

}
