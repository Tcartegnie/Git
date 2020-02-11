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
	[Header("Generale")]
	public Transform target;
	public Rigidbody rb;
	public float MaxSpeed;
	public float MaxDistanceFromTarget;
	public float DetectionRange;
	public float AttackModeRange;


	[Space]
	[Header("Obstacle avoidance")]
	public float MAXAHEAD;
	public float MaxAvoidanceForce;
	public List<GameObject> colliders = new List<GameObject>();
	[Space]
	[Header("Arrival")]
	public float SlowingDistance;
	public Vector3 TargetOffset = new Vector3();
	[Space]
	[Header("Wanders")]
	public float RadiusWanders;
	public float LittleRadiusWanders;
	[Space]
	[Header("Separation")]
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

	BehaviourTreeConstructor BTconstructor;
	IBehaviourTreeNode BTCloseTarget;
	BehaviourTreeConstructor BTFarTarget;

	IBehaviourTreeNode tree;
	IBehaviourTreeNode Closetree;
	IBehaviourTreeNode Fartree;

	bool TargetDetected;

	private void Start()
	{

		GameManager GM = GameManager.instance;
		target = GM.Player.transform;

		rb = GetComponent<Rigidbody>();
		seek = new Seek(transform,target,rb,MaxSpeed,1,0, new Vector3(-10,-10,0));
		flee = new Flee(transform, target, rb, MaxSpeed,1);
		arrival = new Arrival(transform, target, rb, MaxSpeed,0.5f,SlowingDistance, MaxDistanceFromTarget);
		arrival.SetOffset(TargetOffset);
		pursuite = new Pursuite(transform, target, rb, MaxSpeed,1, MaxDistanceFromTarget);
		obstacleAvoidance = new ObstacleAvoidance(transform, target, rb, MaxSpeed,1f, MAXAHEAD,MaxAvoidanceForce, colliders);
		separation = new Separation(transform, target, rb, MaxSpeed,1.0f, AvoidTargetDistance);
		wanders = new Wanders(transform, target, rb, MaxSpeed,1.0f, RadiusWanders, LittleRadiusWanders);

		BTconstructor = new BehaviourTreeConstructor();


		//BTCloseTarget = new BehaviourTreeConstructor();

		//BTFarTarget = new BehaviourTreeConstructor();

		//BTFarTarget.Condition("DistanceTest", IsTargetOutOfRange).Do("Seek", AddSeek).End().Build();

		tree = BTconstructor.Sequence("Root").Sequence("BasicSequence")
			.Selector("Test")
				.Selector("TRackBehaviour")
					.Sequence("InTargetRange")

						.Condition("IsInAttackRange ?", IsTargetInAttackRange)
							.Selector("PlayerInMove")
							
								.Sequence("Check Player is in move")

									.InvertedCondition("IsPlayerInMove", TargetIsMoving)
									.Do("Wander", AddWanders)
									.Do("LookForward", LookForward)
									.End()
								.Sequence("In case of player was not in move")
								
									.Do("Arrival",AddArrival)
									.Do("LookForward", LookOnTarget)
								.End()
							.End()
						
						.End()


					.Sequence("InDetectionRange")

						.Condition("IsInDetectionRange", IsTargetInDetectionRange)
						.Do("Seek", AddSeek)
						.Do("LookForward",LookForward)

					.End()
			
			.End()

		.End()

				.Do("ObstacleAvoidance", AddObstaclAvoidance)
		.End()
		
	.Build();

		//behaviorTreeStearingLeaf = new BehaviorTreeStearingLeaf(seek.GetSteering);
		//DistanceCheckPlayer = new ConditionNode(IsTargetInRange,behaviorTreeStearingLeaf);
		//root = new BehaviourTreeRoot(DistanceCheckPlayer);

		//root.Init(root);
	}


	void FixedUpdate()
    {
		CheckTargetDetection();
		CurrentBehaviours.Clear();
		tree.Tick(Time.deltaTime);
		ComputeSteering();
	}

	public void ComputeSteering()
	{
		Vector3 Steering = new Vector3();

		for(int i  = 0; i < CurrentBehaviours.Count;i++)
		{
			Steering += CurrentBehaviours[i].GetSteering();
		}

		rb.AddForce(Steering);
		
	}




	public float GetDistanceFromTarget()
	{
		return Vector2.Distance(target.position,transform.position);
	}

	public BehaviourTreeStatus SéquenceTest (float test)
	{

		return BehaviourTreeStatus.Continue;
	}

	public BehaviourTreeStatus ComputeSteering(float deltatime)
	{
		return BehaviourTreeStatus.Succes;
	}



	public BehaviourTreeStatus AddWanders(float DeltaTime)
	{
		Debug.Log("In Wanders");
		CurrentBehaviours.Add(wanders);
		return BehaviourTreeStatus.Succes;
	}

	public BehaviourTreeStatus AddObstaclAvoidance(float DeltaTime)
	{
		Debug.Log("In Obstacle avoidance");
		CurrentBehaviours.Add(separation);
		return BehaviourTreeStatus.Succes;
	}

	public BehaviourTreeStatus AddSeparation(float DeltaTime)
	{
		Debug.Log("In Separation");
		CurrentBehaviours.Add(separation);
		return BehaviourTreeStatus.Succes;
	}

	public BehaviourTreeStatus AddSeek(float DeltaTime)
	{
		Debug.Log("In seek");
		CurrentBehaviours.Add(seek);
		return BehaviourTreeStatus.Succes;
	}

	public BehaviourTreeStatus AddArrival(float DeltaTime)
	{
		Debug.Log("In arrival");
		CurrentBehaviours.Add(arrival);
		return BehaviourTreeStatus.Succes;
	}

	public BehaviourTreeStatus DebugStuff(float time)
	{
		Debug.Log("Pattroling");
		return BehaviourTreeStatus.Succes;
	}

	public BehaviourTreeStatus LookForward(float time)
	{
		transform.LookAt(transform.position + transform.GetComponent<Rigidbody>().velocity * (Time.deltaTime * 10)); 
		return BehaviourTreeStatus.Succes;
	}


	public BehaviourTreeStatus LookOnTarget(float time)
	{
		transform.LookAt(target.transform);
		return BehaviourTreeStatus.Succes;
	}

	public void CheckTargetDetection()
	{
		if (IsTargetInDetectionRange(0.0f))
		{
			TargetDetected = true;
		}
		else
		{
			TargetDetected = false;
		}
	}

	public bool IsTargetDetected(float DeltaTime)
	{
		return TargetDetected;
	}

	public bool IsTargetInDetectionRange(float Adegager)
	{
		if (GetDistanceFromTarget() < DetectionRange)
		{
			return true;
		}
		return false;
	}

	public bool IsTargetInAttackRange(float ADegager)
	{
		if(GetDistanceFromTarget() < AttackModeRange)
		{
			return true;
		}
		return false;
	}


	public bool TargetIsMoving(float ADegager)
	{
		if(target.GetComponentInParent<Rigidbody>().velocity.magnitude > 1.0f)
		{
			return true;
		}
		return false;
	}


	public void OnDestroy()
	{
		removeFromSeparation?.Invoke(this.transform);
	}


	public void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Ennemy")
		{
			if (other.GetComponent<IAMove>() != null)
			{
				other.GetComponent<IAMove>().removeFromSeparation += separation.RemoveTarget;
				separation.AddTarget(other.transform);
			}
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



