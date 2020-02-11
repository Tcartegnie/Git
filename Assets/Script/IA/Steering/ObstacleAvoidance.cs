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

		Debug.DrawLine(EntityTr.position, GetHalfAhead(), Color.red);
		Debug.DrawLine(GetAhead(), GetHalfAhead(), Color.blue);

		List<Vector3> ObjectsTransformsPositons = GetCollidersInRange();
		
		Vector3 ClosestPosition = GetTheClosestCollider(ObjectsTransformsPositons);

		if (ClosestPosition.magnitude != 0)
		{
			return ComputeAvoidanceForce(ClosestPosition);
		}
		return new Vector3();
	}

	public float GetDynamicLenght()
	{
		float DynamicLenght = Rb.velocity.magnitude / MaxHead;
		return DynamicLenght;
	}

	public Vector3 GetAhead()
	{
		Vector3 ahead = new Vector3();
		ahead = EntityTr.position + Rb.velocity.normalized * GetDynamicLenght();
		return ahead;
	}

	public Vector3 GetHalfAhead()
	{
		Vector3 HalfAhead = new Vector3();
		HalfAhead = EntityTr.position + (Rb.velocity.normalized * (GetDynamicLenght() * 0.5f));
		return HalfAhead;
	}

	List<Vector3> GetCollidersInRange()
	{
		List<Vector3> ObjectsTransformsPositons = new List<Vector3>();
		for (int i = 0; i < colliders.Count; i++)
		{
			Collider collider = colliders[i].GetComponent<Collider>();

			Vector3 ColliderMaxBound = 	collider.bounds.max;
			

			if (Vector3.Distance(collider.transform.position, GetAhead()) < ColliderMaxBound.magnitude + EntityTr.localScale.magnitude
		   || (Vector3.Distance(collider.transform.position, GetHalfAhead()) < ColliderMaxBound.magnitude + EntityTr.localScale.magnitude)
		   && Vector3.Distance(collider.transform.position, EntityTr.position) < ColliderMaxBound.magnitude + EntityTr.localScale.magnitude)
			{
				ObjectsTransformsPositons.Add(colliders[i].transform.position);
			}
		}
		return ObjectsTransformsPositons;
	}


	Vector3 ComputeAvoidanceForce(Vector3 ClosestPosition)
	{
		Vector3 avoidance_force = GetAhead() - ClosestPosition;
		avoidance_force = avoidance_force.normalized * MaxAvoidanceForce;
		return avoidance_force;
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
