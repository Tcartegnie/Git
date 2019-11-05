using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	float speed;
    float damage;
	public float lifetime;

	public float Speed { get => speed; set => speed = value; }

	// Start is called before the first frame update
	public void Update()
	{
		transform.position += transform.forward * (Time.deltaTime * Speed);
		lifetime -= Time.deltaTime;
		if(lifetime < 0)
		{
			Destroy(gameObject);
		}
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
