using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wanders : Steering
{

	Vector3 LastRadius;
	float greatRadius;
	float littleradius;
	float timer;
	Arrival arrival;
	bool test = true;
	Vector3 DesiredPosition = new Vector3(0,0,0);
	public Wanders(Transform entityTr, Transform targetTr, Rigidbody RB, float MaxSpeed,float Wheigt, float Radius, float LittleRadius) : base(entityTr, targetTr, RB, MaxSpeed,Wheigt)
	{
		LastRadius = new Vector3();
		greatRadius = Radius;
		littleradius = LittleRadius;
		arrival = new Arrival(entityTr, targetTr, RB, MaxSpeed, 1, 50, 0);
	}


	public Vector3 GetWandersPositon()
	{
		Vector3 dirction = Rb.velocity;
		dirction.Normalize();

		Vector3 center = dirction * (MaxSpeed - greatRadius);
		double value = (Random.Range(0, 360));

		Vector3 r = new Vector3(Mathf.Sin((float)value * Mathf.PI / 180.0f) * littleradius, Mathf.Cos((float)value * Mathf.PI / 180.0f) * littleradius, Mathf.Tan((float)value * Mathf.PI / 180.0f) * littleradius);
		Vector3 R = LastRadius;
		R += r;

		Vector3 WandersForce = center +R;

		R.Normalize();
		R *= greatRadius;
		LastRadius = R;

		return WandersForce;
	}

	public Vector3 GetWandersPositon(Vector3 CenterPosition)
	{

		Vector3 center = CenterPosition;
		double value = (Random.Range(0, 360));

		Vector3 r = new Vector3(Mathf.Sin((float)value * Mathf.PI /180.0f) * littleradius, Mathf.Cos((float)value * Mathf.PI / 180.0f) * littleradius, Mathf.Tan((float)value * Mathf.PI / 180.0f) * littleradius);

		r.Normalize();
		r *= greatRadius;
	
		Vector3 WandersForce = center + r;


		return WandersForce;
	}

	public bool IsTargetReach()
	{
		//Debug.Log(Vector3.Distance(EntityTr.position, DesiredPosition));

		if(Vector3.Distance(EntityTr.position, DesiredPosition) <= 20)
		{
			return true;
		}
		return false;
	}


	public Vector3 ComputeSteering()
	{
		if (IsTargetReach() || test)
		{
			Debug.Log("New wander position");
			DesiredPosition = GetWandersPositon(TargetTr.position);
			test = false;
		}

		Debug.DrawLine(TargetTr.position, DesiredPosition, Color.green);

		Vector3 DesiredVelocity = new Vector3();

		DesiredVelocity = arrival.ComputeSteering(DesiredPosition);

		return DesiredVelocity;
	}

	public override Vector3 GetSteering()
	{
		return ComputeSteering();
	}
	
}
