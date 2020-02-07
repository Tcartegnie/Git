using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : Steering
{
	float ReachDistanceOffset = 0;
	Vector3 offset;
	public Seek(Transform entityTr, Transform targetTr, Rigidbody RB, float MaxSpeed, float Wheight, float ReachDistanceOffset, Vector3 offset) : base(entityTr, targetTr, RB, MaxSpeed,Wheight)
	{
		this.offset = offset; 
	}
	

	public Vector3 ComputeSteering()
	{
		Vector3 desiredVelocity = (TargetTr.position + ComputeOffset()) - EntityTr.position;
		desiredVelocity.Normalize();
		desiredVelocity *= MaxSpeed;

		Vector3 steering = desiredVelocity - Rb.velocity;
		return steering;
	}



	public Vector3 ComputeSteering(Vector3 targetPosition)
	{
		Vector3 desiredVelocity = (((targetPosition + ComputeOffset()) - EntityTr.position));//Get the direction
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

	public Vector3 ComputeOffset()
	{
		Vector3 FinalOffset =	new Vector3(
		 TargetTr.right.x + offset.x,
		 TargetTr.up.y + offset.y,
		 TargetTr.forward.z + offset.z
		 );
		return FinalOffset;
	}


	public override Vector3 GetSteering()
	{
		return ComputeSteering();
	}

}
