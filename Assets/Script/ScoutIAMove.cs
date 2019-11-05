using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoutIAMove : MonoBehaviour
{
	public Transform target;
	public float RayCastOffset;
	public float RayCastRange;
	public float rotationRange;
	public float Speed;
    

    // Update is called once per frame
    void Update()
    {
		MoveInDirection(transform.forward);
		FollowTarget();
    }


	void Turn()
	{
		Vector3 pos = target.position - transform.position;
		Quaternion rotation = Quaternion.LookRotation(pos);
		transform.rotation = Quaternion.Slerp(transform.rotation,rotation, rotationRange * Time.deltaTime);
	}

	public void MoveInDirection(Vector3 Direction)
	{
		transform.position += Direction * (Time.deltaTime*Speed); 
	}

	public void FollowTarget()
	{
	
		Vector3 Right = transform.position + transform.right * RayCastOffset;
		Vector3 Left = transform.position - transform.right * RayCastOffset;
		Vector3 Up = transform.position + transform.up * RayCastOffset;
		Vector3 Down = transform.position - transform.up * RayCastOffset;

		RaycastHit hit = new RaycastHit();

		Vector3 rayCastOffset = Vector3.zero;

		if (Physics.Raycast(Right, transform.forward, out hit, RayCastRange))
		{
			rayCastOffset -= Vector3.right;
		}
		else if (Physics.Raycast(Left, transform.forward, out hit, RayCastRange))
		{
			rayCastOffset += Vector3.right;
		}

		if (Physics.Raycast(Up,transform.forward,out hit,RayCastRange ))
		{
			rayCastOffset += Vector3.up;
		}
		else if (Physics.Raycast(Down, transform.forward, out hit, RayCastRange))
		{
			rayCastOffset -= Vector3.up;
		}


		if (rayCastOffset != Vector3.zero)
			transform.Rotate(rayCastOffset * 5f * Time.deltaTime);
		else
			Turn();
	}
}
