using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollider : Ship
{
	public string HitBulletTag;


	public void OnTriggerEnter(Collider other)
	{

		if (other.tag == HitBulletTag)
		{
		//	Debug.Log(other.name + " have touched : " + gameObject.name);
			ShipState.OnHit(other.GetComponent<Bullet>().GetDamage());
			Destroy(other.gameObject);
		}
	}


	public void OnCollisionEnter(Collision collision)
	{
		ShipState.OnHit(10);
	}

}
