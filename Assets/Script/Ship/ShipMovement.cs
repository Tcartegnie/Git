using System.Collections;
using UnityEngine;

public class ShipMovement : Ship
{
	Rigidbody rb;
	[Space]
	static public Camera CurrentCamera;
	public Camera ThirdPersonCam;
	public Camera FirstPersonCam;
	[Space]
	[Header("Acceleration")]
	public float Acceleration;
	public float Deceleration;
	[Space]
	[Header("Speed")]
	private float Speed;
	public float MaxSpeed;

	public float Straffspeed;
	public float liftingoffPower;
	public float angularespeed;
	[Space]
	[Header("Axis move")]
	float mouseXAxis;
	float mouseYAxis;
	[Space]
	[Header("Booster")]
	public float BoosterLVL;
	public float BoosterPower;
	[Space]
	public float BoostMaxAccelerationTime;
	public float BoosterMaxDecelerationTime;

	public Vector2 AxisSensitivity;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		CurrentCamera = ThirdPersonCam;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		float MaxVelocity = MaxSpeed + GetBoosterValue();
		rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxVelocity);
	}


	public void CallMouseAxisInput()
	{
		mouseXAxis += Input.GetAxis("Mouse X") * (Time.deltaTime * AxisSensitivity.x);
		mouseXAxis = Mathf.Clamp(mouseXAxis, -1, +1);

		mouseYAxis += Input.GetAxis("Mouse Y") * (Time.deltaTime * AxisSensitivity.y);
		mouseYAxis = Mathf.Clamp(mouseYAxis, -1, +1);
	}

	public void CallLateralAndForardMove(float forwardInputValue, float horizontalInputValue)
	{
		if (forwardInputValue != 0 || horizontalInputValue != 0)
		{
			Straff(transform.right,horizontalInputValue);
			IncreaseSpeed(forwardInputValue);
			//rb.drag = 0.5f;
		}
		else
		{
			DecreaseSpeed();
			//rb.drag = 2;
		}
	}

	public void CallAxisRotation()
	{
		float rotattiveInputValue = Input.GetAxis("CentralRotation");
		TurnOnAxis(transform.right, -mouseYAxis, angularespeed);
		TurnOnAxis(transform.up, mouseXAxis, angularespeed);
		TurnOnAxis(transform.forward, -rotattiveInputValue, angularespeed);
	}


	public void MoveForwarde()
	{
		rb.AddForce(transform.forward * ((Speed) + GetBoosterValue()));
	}

	void TurnOnAxis(Vector3 RotaionAxis, float Direction, float Magnitude)
	{
		rb.angularVelocity += (RotaionAxis * (Time.deltaTime * Magnitude) * Direction);
	}

	public float GetBoosterValue()
	{
		return BoosterPower * BoosterLVL;
	}

	void Straff(Vector3 direction,float magnitude)
	{
		rb.AddForce((direction * (Time.deltaTime * Straffspeed)) * magnitude);
	}

	public void DecreaseSpeed()
	{
		if (Speed > 0)
		{
			Speed -= Time.deltaTime * Deceleration;
		}

		else if (Speed < 0)
		{
			Speed += Time.deltaTime * Deceleration;
		}
	}

	public void IncreaseSpeed(float Direction)
	{
		Speed += (Time.deltaTime * Acceleration) * Direction;
		Speed = Mathf.Clamp(Speed, -Acceleration, Acceleration);
	}

	public void Booster()
	{
		if (BoosterLVL < 1)
		{
			BoosterLVL += Time.deltaTime / BoostMaxAccelerationTime;
		}
		BoosterLVL = Mathf.Clamp(BoosterLVL, 0,1);

	}


	public void UnBooste()
	{
		if (BoosterLVL > 0)
		{
			BoosterLVL -= Time.deltaTime / BoosterMaxDecelerationTime;
		}

		BoosterLVL = Mathf.Clamp(BoosterLVL, 0, 1);
	}



	public Vector3 GetVelocity()
	{
		return rb.velocity;
	}

	public float GetSpeed()
	{
		return Speed;
	}

	public void ChangeCamera()
	{
		if (FirstPersonCam.gameObject.activeInHierarchy)
		{
			FirstPersonCam.gameObject.SetActive(false);
			ThirdPersonCam.gameObject.SetActive(true);
			CurrentCamera = ThirdPersonCam;
		}

		else
		{
			FirstPersonCam.gameObject.SetActive(true);
			ThirdPersonCam.gameObject.SetActive(false);
			CurrentCamera = FirstPersonCam;
		}
	}//Camera script//UI ?


	public Vector3 GetRotationAxis()
	{
		return new Vector3(mouseXAxis, mouseYAxis, 0);
	}

	public float GetNormalizedVelocity()
	{
		return (GetVeloctiyMagnitude() / (MaxSpeed));
	}

	public float GetVeloctiyMagnitude()
	{
		return rb.velocity.magnitude;
	}

	public override void OnGameOver()
	{
		rb.velocity = new Vector3();
	}
}
                