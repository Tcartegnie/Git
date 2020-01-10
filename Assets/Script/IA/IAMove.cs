using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
1 - Veillez a ce que mes ennemies pointes leur canon vers moi.
2 - Veillez a ce que les ennemeies me tournent autour quand je suis a l'arret
*/


public enum State
{
	Search,
	Target,
	Destroy,
}

public class IAMove : MonoBehaviour
{
	public Transform target;
	public float RayCastOffset;
	public float RayCastRange;
	public float rotationRange;
	public float Speed;
	public float PursuiteRadius;
	public float MaxSpeed;
	public float MAXAHEAD;
	public float MaxAvoidanceForce;
	public float MaxDistanceFromTarget;
	public float SlowingDIstance;
	public float RadiusWanders;
	public float LittleRadiusWanders;
	public Rigidbody rb;
	public State state;
	public List<GameObject> colliders = new List<GameObject>();
	public Vector3 TargetOffset = new Vector3();
	public float SeparationStrenght;

	public float AvoidTargetDistance;

	public delegate void RemoveFromSeparation(Transform transform);
	RemoveFromSeparation removeFromSeparation;


	//Steering
	List<Steering> CurrentBehaviours = new List<Steering>();
	Seek seek;
	Flee flee;
	Arrival arrival;
	Pursuite pursuite;
	ObstacleAvoidance obstacleAvoidance;
	Separation separation;
	Wanders wanders;
	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		state = State.Search;
		seek = new Seek(transform,target,rb,MaxSpeed,1,0, new Vector3(-10,-10,0));
		flee = new Flee(transform, target, rb, MaxSpeed,1);
		arrival = new Arrival(transform, target, rb, MaxSpeed,0.5f,SlowingDIstance, MaxDistanceFromTarget);
		pursuite = new Pursuite(transform, target, rb, MaxSpeed,1, MaxDistanceFromTarget);
		obstacleAvoidance = new ObstacleAvoidance(transform, target, rb, MaxSpeed,1f, MAXAHEAD,MaxAvoidanceForce, colliders);
		separation = new Separation(transform, target, rb, MaxSpeed,1.0f, AvoidTargetDistance);
		wanders = new Wanders(transform, target, rb, MaxSpeed,1.0f, RadiusWanders, LittleRadiusWanders);
	}

	void FixedUpdate()
    {
		//Debug.Log(GetDistanceFromTarget());
		CheckIAstate();
		CallIABehaviour();
	//	Debug.Log(state);
		rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxSpeed);
	}


	public void CallIABehaviour()
	{
		Vector3 Steering = new Vector3();

		for(int i  = 0; i < CurrentBehaviours.Count;i++)
		{
			Steering += CurrentBehaviours[i].GetSteering() * CurrentBehaviours[i].GetWheight();
		}

		rb.AddForce(Steering);


		//Debug.DrawLine(transform.position, transform.position + (rb.velocity * MaxSpeed));

		transform.LookAt(transform.position + transform.GetComponent<Rigidbody>().velocity * (Time.deltaTime * 10)); 
	}

	public void CheckIAstate()
	{
		if(target == null)
		{
			state = State.Search;
		}

		else if(target != null && GetDistanceFromTarget() > MaxDistanceFromTarget)
		{
			state = State.Target;
		}

		if(target != null && GetDistanceFromTarget() < MaxDistanceFromTarget)
		{
			state = State.Destroy;
		}

		SetStatusBehaviour(state);
	}


	public void SetStatusBehaviour(State state)
	{
		CurrentBehaviours.Clear();
		switch(state)
		{
			case (State.Search):
			//	CurrentBehaviours.Add(pursuite);
				CurrentBehaviours.Add(separation);
			
				break;

			case (State.Target):
				CurrentBehaviours.Add(obstacleAvoidance);
				CurrentBehaviours.Add(seek);
				CurrentBehaviours.Add(separation);
				break;

			case (State.Destroy):
				CurrentBehaviours.Add(obstacleAvoidance);
				CurrentBehaviours.Add(seek);
				CurrentBehaviours.Add(separation);
				break;
		}
	}

	public float GetDistanceFromTarget()
	{
		return Vector2.Distance(target.position,transform.position);
	}

	public void OnDestroy()
	{
		removeFromSeparation?.Invoke(this.transform);
	}


	public void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Ennemy")
		{
			other.GetComponent<IAMove>().removeFromSeparation += separation.RemoveTarget;
			separation.AddTarget(other.transform);
		}
		else if(other.tag == "Player")
		{
			separation.AddTarget(other.transform);
		}
	}

	public void OnTriggerExit(Collider other)
	{
		separation.RemoveTarget(other.transform);
	}

}



