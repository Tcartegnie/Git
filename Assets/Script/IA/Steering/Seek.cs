using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : Steering
{
	float ReachDistanceOffset = 0;
	public Seek(Transform entityTr, Transform targetTr, Rigidbody RB, float MaxSpeed, float Wheight, float ReachDistanceOffset) : base(entityTr, targetTr, RB, MaxSpeed,Wheight){}
	

	public Vector3 ComputeSteering()
	{
		Vector3 desiredVelocity = TargetTr.position - EntityTr.position;
		desiredVelocity.Normalize();
		desiredVelocity *= MaxSpeed;

		Vector3 steering = desiredVelocity - Rb.velocity;
		return steering;
	}

	public Vector3 ComputeSteering(Vector3 targetPosition)
	{
		Vector3 desiredVelocity = ((targetPosition - EntityTr.position));//Get the direction
		desiredVelocity.Normalize();
		desiredVelocity *= MaxSpeed;

		Debug.DrawLine(EntityTr.position, EntityTr.position + desiredVelocity);

		Vector3 steering = desiredVelocity - Rb.velocity;//Get velocity to apply
		return steering;
	}

	public bool IsPositionReach()
	{
		if(Vector3.Distance(EntityTr.position,TargetTr.position) < ReachDistanceOffset)
		{
			return true;
		}
		return false;
	}


	public override Vector3 GetSteering()
	{
		return ComputeSteering();
	}

}
