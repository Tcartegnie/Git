using System.Collections;
using UnityEngine;

public class ShipController : Ship
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
	private float speed;
	public float MaxVelocity;
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
	public float BoostAcceleration;
	public float BoostDeceleration;
	[Space]
	[Header("Landing")]
	public float FloorMinDistance;
	public Vector2 AxisSensitivity;
	public bool ShipLanded;

	//Poissible class for shoot only


	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		CurrentCamera = ThirdPersonCam;
	
		//Cursor.lockState = CursorLockMode.Locked;

	//Coroutine RegenCorroutine =	StartCoroutine(RegenShield(ShieldMax));
	}

	// Update is called once per frame
	void Update()
	{
		if (!ShipState.IsGameover)
		{
			CallLateralAndForardMove();
			CallMouseAxisInput();


			if (Input.GetKey(KeyCode.LeftAlt))
			{
				ShipState.OnHit(1f);
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

				CallAxisRotation();
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

				MoveForwarde();
			}

			ShipState.ShieldCoolDownCompute();
		}
		rb.velocity = Vector3.ClampMagnitude(rb.velocity, (MaxVelocity + GetBoosterValue()));
	}


	public void CallMouseAxisInput()
	{
	
		mouseXAxis += Input.GetAxis("Mouse X") * (Time.deltaTime * AxisSensitivity.x);
		mouseXAxis = Mathf.Clamp(mouseXAxis, -1, +1);

		mouseYAxis += Input.GetAxis("Mouse Y") * (Time.deltaTime * AxisSensitivity.y);
		mouseYAxis = Mathf.Clamp(mouseYAxis, -1, +1);
	}

	public void CallLateralAndForardMove()
	{
		float forwardInputValue = Input.GetAxis("Vertical");
		float horizontalInputValue = Input.GetAxis("Horizontal");


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
	}

	public void CallAxisRotation()
	{
		float rotattiveInputValue = Input.GetAxis("CentralRotation");
		TurnOnAxis(transform.right, -mouseYAxis, angularespeed);
		TurnOnAxis(transform.up, mouseXAxis, angularespeed);
		TurnOnAxis(transform.forward, -rotattiveInputValue, angularespeed);
	}


	void MoveForwarde()
	{
		rb.velocity += transform.forward * (Time.deltaTime * (speed + GetBoosterValue()));
	}//Movement
	void TurnOnAxis(Vector3 RotaionAxis, float Direction, float Magnitude)
	{
		rb.angularVelocity += (RotaionAxis * (Time.deltaTime * Magnitude) * Direction);
	}//Movement
	public float GetBoosterValue()
	{
		return BoosterPower * BoosterLVL;
	}
	void Straff(float direction)
	{
		rb.velocity += (transform.right * (Time.deltaTime * Straffspeed)) * direction;
	}//Movement
	void MoveUp(float Direction, float Speed)
	{
		rb.velocity += (transform.up * (Time.deltaTime * Speed) * Direction);
	}//Movement
	public void DecreaseSpeed()
	{
		if (speed > 0)
		{
			speed -= Time.deltaTime * Deceleration;
		}

		else if (speed < 0)
		{
			speed += Time.deltaTime * Deceleration;
		}
	}//Movement
	public void IncreaseSpeed(float Direction)
	{
		speed += (Time.deltaTime * Acceleration) * Direction;
		speed = Mathf.Clamp(speed, -MaxSpeed, MaxSpeed);
	}//Movement

	public void Booster()
	{
		if (BoosterLVL < 1)
		{
			BoosterLVL += Time.deltaTime / BoostAcceleration;
		}
	}//Movement
	public void UnBooste()
	{
		if (BoosterLVL > 0)
		{
			BoosterLVL -= Time.deltaTime / BoostDeceleration;
		}
	}//Movement
	public void CallLanding()
	{
		Vector3 Landingpos = new Vector3();
		if (GetFloor(out Landingpos))
		{
			StartCoroutine(Landing(Landingpos));
		}
	}//Movement


	public Vector3 GetVelocity()
	{
		return rb.velocity;
	}//Data ?

	public float GetSpeed()
	{
		return speed;
	}//Data ?
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
	}//Camera script
	public Vector3 GetRotationAxis()
	{
		return new Vector3(mouseXAxis, mouseYAxis, 0);
	}//Data ?
	public float GetNormalizedSpeed()
	{
		return	((speed) / MaxSpeed);
	}//Data ?
	public float GetNormalizedVelocity()
	{
		return ((rb.velocity.magnitude) / (MaxVelocity + BoosterPower));
	}//Data ?
	



	public void LiftOff()
	{

		ShipLanded = false;
		Vector3 CurrentPos = transform.position;
		Rigidbody rb = GetComponent<Rigidbody>();
		rb.AddForce(transform.up * liftingoffPower);

	}//Landing ?
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
	}//Landing ?
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

	}//Lanfing ?


	public override void OnGameOver()
	{
		rb.velocity = new Vector3();
	}




}
