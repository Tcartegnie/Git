using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : Steering
{

	float MaxHead;
	float MaxAvoidanceForce;
	List<GameObject> colliders = new List<GameObject>();
	public ObstacleAvoidance(Transform entityTr, Transform targetTr, Rigidbody RB, float MaxSpeed,float Wheight,float MAXHEAD,float MaxAvoidanceForce,List<GameObject>Colliders) : base(entityTr, targetTr, RB, MaxSpeed,Wheight)
	{
		MaxHead = MAXHEAD;
		colliders = Colliders;
		this.MaxAvoidanceForce = MaxAvoidanceForce;
	}

	Vector3 ComputeSteering()
	{
		Vector3 ahead = new Vector3();
		Vector3 HalfAhead = new Vector3();
		float DynamicLenght = Rb.velocity.magnitude / MaxHead;
		ahead = EntityTr.position + Rb.velocity.normalized * DynamicLenght;
		HalfAhead = EntityTr.position + (Rb.velocity.normalized * (DynamicLenght * 0.5f));

		Debug.DrawLine(EntityTr.position, HalfAhead, Color.red);
		Debug.DrawLine(ahead, HalfAhead, Color.blue);

		List<Vector3> ObjectTransformPositon = new List<Vector3>();

		for (int i = 0; i < colliders.Count; i++)
		{
			if (Vector3.Distance(colliders[i].transform.position, ahead) < colliders[i].transform.localScale.magnitude + EntityTr.localScale.magnitude
		   || (Vector3.Distance(colliders[i].transform.position, HalfAhead) < colliders[i].transform.localScale.magnitude + EntityTr.localScale.magnitude)
		   && Vector3.Distance(colliders[i].transform.position, EntityTr.position) < colliders[i].transform.localScale.magnitude + EntityTr.localScale.magnitude)
			{
				ObjectTransformPositon.Add(colliders[i].transform.position);
			}
		}
		Vector3 ClosestPosition = GetTheClosestCollider(ObjectTransformPositon);

		if (ClosestPosition.magnitude != 0)
		{
			Vector3 avoidance_force = ahead - ClosestPosition;
			avoidance_force = avoidance_force.normalized * MaxAvoidanceForce;
			return avoidance_force;
		}
		return new Vector3();
	}

	public Vector3 GetTheClosestCollider(List<Vector3> ColliderPosition)
	{
		float LastClosestDistance = Mathf.Infinity;
		Vector3 LastClosestposition = new Vector3();
		for (int i = 0; i < ColliderPosition.Count; i++)
		{
			float currentDistance = Vector3.Distance(EntityTr.position, ColliderPosition[i]);
			if (currentDistance < LastClosestDistance)
			{
				LastClosestDistance = currentDistance;
				LastClosestposition = ColliderPosition[i];
			}
		}
		return LastClosestposition;
	}

	public override Vector3 GetSteering()
	{
		return ComputeSteering();
	}
}
