using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
	Rigidbody rb;
	public float Acceleration;
	public float Deceleration;
	private float speed;
	public float angularespeed;
	public float MaxVelocity;

    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

		if(Input.GetKey(KeyCode.Z))
		{
			IncreaseSpeed();
		}

		if (Input.GetKey(KeyCode.S))
		{
			DecreaseSpeed();
		}

		MoveForwarde();
		if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			TurnYaxis(1, angularespeed);
		}
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q))
		{
			TurnYaxis(-1, angularespeed);
		}

		if (Input.GetKey(KeyCode.C) )
		{
			MoveUp(1, speed);
		}
		if (Input.GetKey(KeyCode.X) )
		{
			MoveUp(-1, speed);
		}

		rb.velocity = Vector3.ClampMagnitude(rb.velocity,MaxVelocity);
	}

	public Vector3 GetVelocity()
	{
		return rb.velocity;
	}

	void MoveForwarde()
	{
		rb.velocity += transform.forward * (Time.deltaTime * speed);
	}

	void TurnYaxis(float Direction, float Magnitude)
	{
		rb.angularVelocity += (transform.up * (Time.deltaTime * Magnitude) * Direction);
	}

	void MoveUp(float Direction,float Speed)
	{
		rb.velocity += (transform.up * (Time.deltaTime * Speed) * Direction);
	}

	public void IncreaseSpeed()
	{
		if(speed < MaxVelocity)
		{
			speed += (Time.deltaTime * Acceleration);
			speed = Mathf.Clamp(speed, 0, 10);
		}
	}

	public void DecreaseSpeed()
	{
			speed -= (Time.deltaTime * Deceleration);
			speed =  Mathf.Clamp(speed, -10, 0);
	}
}
