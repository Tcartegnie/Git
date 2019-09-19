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
	public float liftingoffPower;

	float DistanceFromObjectif;

	static public Camera currentCamera;
	public Camera ThirdPersonCam;
	public Camera FirstPersonCam;

	Ray rayDirection;
	Ray ShipOrientationRay;

	Vector3 WorldPosPointed;
	float mouseXAxis;
	float mouseYAxis;

	public float BoosterLVL;
	public float BoosterPower;
	[Space]
	public float BoostAcceleration;
	public float BoostDeceleration;

	public float FloorMinDistance;

	public Vector2 AxisSensitivity;

	public Canon LeftCanon;
	public Canon RightCanon;

	public SpaceShipUI shpashipUI;

	public bool ShipLanded;

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

		

		if (Input.GetMouseButtonDown(0))
		{
			Shoot();
		}

		if (Input.GetMouseButtonUp(1))
		{
			if (ShipLanded)
			{
				LiftOff();
			}
			else
			{
				CallLanding();
			}
		}


		if (!ShipLanded)
		{



			if (forwardInputValue != 0 || horizontalInputValue != 0)
			{
				Straff(horizontalInputValue);
				IncreaseSpeed(forwardInputValue);
				rb.drag = 0;
			}
			else
			{
				DecreaseSpeed();
				rb.drag = 2;
			}


			TurnOnAxis(transform.right, -mouseYAxis, angularespeed);
			TurnOnAxis(transform.up, mouseXAxis, angularespeed);
			TurnOnAxis(transform.forward, -rotattiveInputValue, angularespeed);

			if (Input.GetKeyUp(KeyCode.LeftControl))
			{
				ChangeCamera();
			}



			if (Input.GetKey(KeyCode.LeftShift))
			{
				Booster();
			}
			else
			{
				UnBooste();
			}
			//GetDirectionPoint();
			RotateCanon();
			MoveForwarde();
		}
		rb.velocity = Vector3.ClampMagnitude(rb.velocity, (MaxVelocity + GetBoosterValue()));
	}




	void MoveForwarde()
	{
		rb.velocity += transform.forward * (Time.deltaTime * (speed + GetBoosterValue()));
	}

	void TurnOnAxis(Vector3 RotaionAxis,float Direction, float Magnitude)
	{
		rb.angularVelocity += (RotaionAxis * (Time.deltaTime * Magnitude) * Direction);
	}
	
	public float GetBoosterValue()
	{
		return BoosterPower * BoosterLVL;
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

	//public void OnDrawGizmos()
	//{
	//	Vector3 RayDirection = shpashipUI.GetCursorRay().direction;
	//	Vector3 TargetPos = transform.position + (RayDirection * CrossPointDistance);
	//	Gizmos.DrawSphere(TargetPos, 2);
	//}

	public void RotateCanon()
	{
		Vector3 RayDirection = shpashipUI.GetCursorRay().direction;
//		Vector3 TargetPos = transform.position + (RayDirection * CrossPointDistance);

	

		LeftCanon.TargetPoint(RayDirection);
		RightCanon.TargetPoint(RayDirection);
	}

	public void Shoot()
	{
		LeftCanon.Shoot(speed);
		RightCanon.Shoot(speed);
	}

	public void Booster()
	{
		if(BoosterLVL < 1)
		{
			BoosterLVL += Time.deltaTime / BoostAcceleration;
		}
	}
	public void UnBooste()
	{
		if(BoosterLVL > 0)
		{
			BoosterLVL -= Time.deltaTime / BoostDeceleration;
		}
	}



	public void CallLanding()
	{
		Vector3 Landingpos = new Vector3();
		if(GetFloor(out Landingpos))
		{
			StartCoroutine(Landing( Landingpos));
		}
	}


	public void LiftOff()
	{

		ShipLanded = false;
		Vector3 CurrentPos = transform.position;
		Rigidbody rb = GetComponent<Rigidbody>();
		rb.AddForce(transform.up * liftingoffPower);

	}

	public IEnumerator Landing(Vector3 LandingPos)
	{
		ShipLanded = true;
		Vector3 CurrentPos = transform.position;

		for (float i = 0; i < 1; i += Time.deltaTime)
		{
			transform.position = Vector3.Lerp(CurrentPos,LandingPos + transform.up,i);
			yield return null;
		}

		rb.velocity = new Vector3(0, 0, 0);
		rb.angularVelocity = new Vector3(0, 0, 0);
	}


	public bool GetFloor(out Vector3 LandingPos)
	{
		RaycastHit rayHit = new RaycastHit();
		Ray ray = new Ray(transform.position,-(transform.up));

		if(Physics.Raycast(ray,out rayHit, FloorMinDistance))
		{
			Debug.Log("The floor is touched");
			Debug.DrawRay(transform.position, -(transform.up * FloorMinDistance),Color.green,100f);

			LandingPos = rayHit.point;

			return true;
		}

		LandingPos = new Vector3();
		return false;

	}


}
