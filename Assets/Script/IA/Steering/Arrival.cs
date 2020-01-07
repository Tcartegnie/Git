using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrival : Steering
{
	float SlowingDistance = new float();
	float DistanceFromTarget;
	 Vector3 Offset = new Vector3();
	public Arrival(Transform entityTr, Transform targetTr, Rigidbody RB, float MaxSpeed,float Wheight,float SlowingDistance,float DesiredOffsetFromTarget) : base(entityTr, targetTr, RB, MaxSpeed, Wheight)
	{
		this.SlowingDistance = SlowingDistance;
		DistanceFromTarget = DesiredOffsetFromTarget;
	}

	public void SetOffset(Vector3 offset)
	{
		Offset = offset;
	}

	public Vector3 ComputOffset()
	{
		Vector3 FinalOffset = new Vector3();

		FinalOffset += TargetTr.forward * Offset.z;
		FinalOffset += TargetTr.right * Offset.x;
		FinalOffset += TargetTr.up * Offset.y;


		return FinalOffset;
	}

	public  Vector3 ComputeSteering()
	{
		Vector3 targetOffset = TargetTr.position - EntityTr.position;

		Vector3 NormalizedTargetOffset = targetOffset.normalized;

		targetOffset -= NormalizedTargetOffset * DistanceFromTarget;

		targetOffset -= ComputOffset();

		Debug.DrawLine(EntityTr.position, EntityTr.position + (targetOffset),Color.red);

		float distance = targetOffset.magnitude;
		float FinalSpeed = 0;
		//Debug.Log(distance);
	
			float rampedSpeed = MaxSpeed * (distance / SlowingDistance);
			float clippedSpeed = Mathf.Min(rampedSpeed, MaxSpeed);
			FinalSpeed = clippedSpeed / distance;
		
	

		Vector3 desiredVelocity = new Vector3(targetOffset.x * FinalSpeed, 
										      targetOffset.y * FinalSpeed, 
											  targetOffset.z * FinalSpeed);


		Vector3 steering = desiredVelocity - Rb.velocity;


		return steering;

	}


	public Vector3 ComputeSteering(Vector3 TargetPosition)
	{
		Vector3 targetOffset = TargetPosition - EntityTr.position;

		Vector3 NormalizedTargetOffset = targetOffset.normalized;

		targetOffset -= NormalizedTargetOffset * DistanceFromTarget;

		targetOffset -= ComputOffset();

		Debug.DrawLine(EntityTr.position, EntityTr.position + (targetOffset), Color.red);

		float distance = targetOffset.magnitude;
		float FinalSpeed = 0;
		//Debug.Log(distance);

		float rampedSpeed = MaxSpeed * (distance / SlowingDistance);
		float clippedSpeed = Mathf.Min(rampedSpeed, MaxSpeed);
		FinalSpeed = clippedSpeed / distance;



		Vector3 desiredVelocity = new Vector3(targetOffset.x * FinalSpeed,
											  targetOffset.y * FinalSpeed,
											  targetOffset.z * FinalSpeed);


		Vector3 steering = desiredVelocity - Rb.velocity;


		return steering;

	}


	public override Vector3 GetSteering()
	{
		return ComputeSteering();
	}

}
