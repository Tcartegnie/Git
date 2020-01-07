using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{


	[Range(0, 1)]
	public float RangeDetection;
	public float DistanceDetection;

	public List<Canon> Canons;
	public ShipState shipstate;
	public GameObject Target;



	public void Start()
	{
		shipstate.Gameover += OnGameOver;
	}

	public void LookAtTarget()
	{
		transform.LookAt(GetTargetPredictedVelocity());
	}
	public void Update()
	{
		if (Target != null)
		{
			LookAtTarget();
			if (TargetIsInRange())
			{
				Shoot();
			}
		}

	}

	public bool TargetIsInRange()
	{
		if(CheckTargetPositions())
			{
				return true;
			}
			return false;
	}

	public bool CheckTargetPositions()
	{
		float DotResult =	Vector3.Dot(transform.forward, GetTargetPredictedVelocity());
	if(DotResult < RangeDetection)
		{
			return true;
		}
		else
		{
			return false;
		}
	}


	public void SetTarget(GameObject Target)
	{
		this.Target = Target;
	}


	public float GetTimeToReachTarget()
	{
		if (Target != null)
		{
		
				float DistanceFromTarget = Vector2.Distance(transform.position, Target.transform.position);
			float TimeToReachTarget = DistanceFromTarget / (400);
				return TimeToReachTarget;
			
		}
		return 0.0f;
	}

	public Vector3 GetTargetPredictedVelocity()
	{
		Vector3 predictedVelocity = new Vector3();
		Rigidbody rb = Target.GetComponentInParent<Rigidbody>();

		predictedVelocity = Target.transform.position + rb.velocity * (GetTimeToReachTarget());


		return predictedVelocity;

	}


	public void Shoot()
	{
		foreach(Canon canon in Canons)
		{
			canon.Shoot();
		}
	}


	public void OnGameOver()
	{
		Destroy(gameObject);
	}

}
 