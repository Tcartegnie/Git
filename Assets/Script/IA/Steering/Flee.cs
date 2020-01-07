using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : Steering
{
	public Flee(Transform entityTr, Transform targetTr, Rigidbody RB, float MaxSpeed, float Wheight) : base(entityTr, targetTr, RB, MaxSpeed, Wheight) { }

	public 	Vector3 ComputeSteering()
	{
		Vector3 desiredVelocity = (EntityTr.position - TargetTr.position).normalized * MaxSpeed;
		Vector3 steering = desiredVelocity - Rb.velocity;//Get velocity to apply
		return steering;
	}

	public Vector3 ComputeSteering(Vector3 targetPosition)
	{
		Vector3 desiredVelocity = (EntityTr.position - targetPosition).normalized * MaxSpeed;
		Vector3 steering = desiredVelocity - Rb.velocity;//Get velocity to apply
		return steering;
	}

	public override Vector3 GetSteering()
	{
		return ComputeSteering();
	}
}
