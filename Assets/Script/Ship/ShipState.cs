using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipState : MonoBehaviour
{

	public delegate void Visibility();
	public Visibility visibility;
	public Visibility Novisibility;

	public delegate void OnGameOver();


	public OnGameOver Gameover;
	

	public GameObject targetLocked;

	public float Life;
	public float LifeMax;

	public float Shield;
	public float ShieldMax;

	public bool IsGameover;

	public float ShieldRegenerationRate;

	float ShieldCoolDown;
	public float ShieldCoolDownMax;
	public bool AutomatedShootEnable;

	public GameObject TargetLocked { get => targetLocked; set => targetLocked = value; }



	public void OnHit(float Damage)
	{
		if (Shield > 0)
		{
			HitShield(Damage);
		}
		else
		{
			HitLife(Damage);
		}
	}

	public void HitShield(float value)
	{
		ShieldCoolDown = ShieldCoolDownMax;
		Shield -= value;
	}

	public void HitLife(float value)
	{
		Life -= value;
		CheckLife();
	}


	public void CheckLife()
	{
		if(Life <= 0)
		{
			ShipExplosion();
		}
	}

	public void ShipExplosion()
	{
		Destroy(gameObject);
		IsGameover = true;
		Gameover();
	}

	public void ShieldCoolDownCompute()
	{
		if (ShieldCoolDown > 0)
		{
			ShieldCoolDown -= Time.deltaTime;
		}
		else if (Shield < ShieldMax)
		{
			Shield += Time.deltaTime * ShieldRegenerationRate;
		}

	}

	public float GetNormalizedLife()
	{
		return (Life / LifeMax);
	}

	public float GetNormalizedShield()
	{
		return (Shield / ShieldMax);
	}


	private void OnBecameVisible()
	{
		Debug.Log("Im in the camera");
		visibility?.Invoke();
	}

	private void OnBecameInvisible()
	{
		Debug.Log("Im out of the camera");
		Novisibility?.Invoke();
	}


}
