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
	public float MaxSpeed;
	public float Straffspeed;

	float DistanceFromObjectif;



	static public Camera currentCamera;
	public Camera ThirdPersonCam;
	public Camera FirstPersonCam;

	Ray rayDirection;
	Ray ShipOrientationRay;

	Vector3 WorldPosPointed;
	float mouseXAxis;
	float mouseYAxis;

	public Vector2 AxisSensitivity;

	public Canon LeftCanon;
	public Canon RightCanon;

	public SpaceShipUI shpashipUI;

	//Poissible class for shoot only


	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		currentCamera = ThirdPersonCam;
		//Cursor.lockState = CursorLockMode.Locked;
	}

	// Update is called once per frame
	void Update()
	{
		float forwardInputValue = Input.GetAxis("Vertical");
		float horizontalInputValue = Input.GetAxis("Horizontal");
		float rotattiveInputValue = Input.GetAxis("CentralRotation");

			mouseXAxis += Input.GetAxis("Mouse X") * (Time.deltaTime * AxisSensitivity.x);
			mouseXAxis = Mathf.Clamp(mouseXAxis, -1, +1);

			mouseYAxis += Input.GetAxis("Mouse Y") * (Time.deltaTime * AxisSensitivity.y);
			mouseYAxis = Mathf.Clamp(mouseYAxis, -1, +1);
		

		if (forwardInputValue != 0 || horizontalInputValue != 0)
		{
			Straff(horizontalInputValue);
			IncreaseSpeed(forwardInputValue);
			rb.drag = 0;
		}
		else
		{
			DecreaseSpeed();
			rb.drag = MaxVelocity * 0.25f;
		}


		TurnOnAxis(transform.right,-mouseYAxis, angularespeed);
		TurnOnAxis(transform.up,mouseXAxis, angularespeed);
		TurnOnAxis(transform.forward,-rotattiveInputValue, angularespeed);

		if (Input.GetKeyUp(KeyCode.LeftControl))
		{
			ChangeCamera();
		}

		if(Input.GetMouseButtonDown(0))
		{
			Shoot();
		}

		//GetDirectionPoint();
		RotateCanon();
		MoveForwarde();
		rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxVelocity);
	}




	void MoveForwarde()
	{
		rb.velocity += transform.forward * (Time.deltaTime * speed);

	}

	void TurnOnAxis(Vector3 RotaionAxis,float Direction, float Magnitude)
	{
		rb.angularVelocity += (RotaionAxis * (Time.deltaTime * Magnitude) * Direction);
	}
	


	void Straff(float direction)
	{
		rb.velocity += (transform.right * (Time.deltaTime* Straffspeed)) * direction;
	}

	void MoveUp(float Direction, float Speed)
	{
		rb.velocity += (transform.up * (Time.deltaTime * Speed) * Direction);
	}

	public void DecreaseSpeed()
	{
		if(speed > 0)
		{
			speed -= Time.deltaTime * Deceleration;
		}

	else if (speed < 0)
		{
			speed += Time.deltaTime * Deceleration;
		}
	}
	public void IncreaseSpeed(float Direction)
	{
			speed += (Time.deltaTime * Acceleration) * Direction;
			speed = Mathf.Clamp(speed, -MaxSpeed, MaxSpeed);
	}
	public Vector3 GetVelocity()
	{
		return rb.velocity;
	}

	public Vector3 GetWorldPosPointed()
	{
		return WorldPosPointed;
	}

	public float GetSpeed()
	{
		return speed;
	}


	
	public void ChangeCamera()
	{
		if (FirstPersonCam.gameObject.activeInHierarchy)
		{
			FirstPersonCam.gameObject.SetActive(false);
			ThirdPersonCam.gameObject.SetActive(true);
			currentCamera = ThirdPersonCam;
		}

		else
		{
			FirstPersonCam.gameObject.SetActive(true);
			ThirdPersonCam.gameObject.SetActive(false);
			currentCamera = FirstPersonCam;
		}
	}

	public Vector3 GetRotationAxis()
	{
		return new Vector3(mouseXAxis, mouseYAxis, 0);
	}


	public void RotateCanon()
	{
		Vector3 RayDirection = shpashipUI.GetCursorRay().direction;

		LeftCanon.CanonLookInDirectionOf(RayDirection);
		RightCanon.CanonLookInDirectionOf(RayDirection);
	}
	public void Shoot()
	{
		LeftCanon.Shoot(speed);
		RightCanon.Shoot(speed);
	}


}
