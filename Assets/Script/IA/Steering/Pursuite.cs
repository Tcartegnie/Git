using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuite : Steering
{
	float MaxDistanceFromTarget;
	public Pursuite(Transform entityTr, Transform targetTr, Rigidbody RB, float MaxSpeed,float Wheight,float MaxDIstanceFromTarget) : base(entityTr, targetTr, RB, MaxSpeed,Wheight)
	{
		MaxDistanceFromTarget = MaxDIstanceFromTarget;
	}

	public override Vector3 GetSteering()
	{
		return ComputeSteering();
	}

	Vector3 ComputeSteering()
	{
		Vector3 TargetOffset = EntityTr.position - TargetTr.position;

		float fT = Mathf.Min(MaxDistanceFromTarget, TargetOffset.magnitude);


		Vector3 Prediction = (EntityTr.position - (TargetTr.forward)) + TargetTr.GetComponent<Rigidbody>().velocity * (Time.deltaTime * fT);
		 
		Debug.DrawLine(EntityTr.position, Prediction, Color.green);

		Vector3 DesiredVelocity = Prediction - EntityTr.position;
		DesiredVelocity.Normalize();
		DesiredVelocity *= MaxSpeed;
		Vector3 steering = DesiredVelocity - Rb.velocity;
		return steering;
	}
}
