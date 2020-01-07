using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Steering
{
	protected Transform EntityTr;
	protected Transform TargetTr;
	protected Rigidbody Rb;
	protected float MaxSpeed;
	protected float Wheight;

 protected Steering(Transform EntityTr,Transform TargetTr,Rigidbody Rb,float MaxSpeed,float Wheight)
	{
		this.EntityTr = EntityTr;
		this.TargetTr = TargetTr;
		this.Rb = Rb;
		this.MaxSpeed = MaxSpeed;
		this.Wheight = Wheight;
	}

	abstract public Vector3 GetSteering();
	public float GetWheight()
	{
		return Wheight;
	}
}
