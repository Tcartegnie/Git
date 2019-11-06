using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	float speed;
    float damage;
	public float lifetime;
	Rigidbody RB;
	public float SpeedMax;
	public float Speed { get => speed; set => speed = value; }



	public void Start()
	{
		RB = GetComponent<Rigidbody>();
		MoveForward();
	}
	// Start is called before the first frame update
	public void Update()
	{
		MoveForward();
		lifetime -= Time.deltaTime;
		CheckLife();
	}

	public void CheckLife()
	{
		if (lifetime < 0)
		{
			Destroy(gameObject);
		}
	}

	public void MoveForward()
	{
		RB.velocity = transform.forward *  Speed;
	}

	public void SetDamage(float damage)
	{
		this.damage = damage;
	}
	public float GetDamage()
	{
		return damage;
	}
}
