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
			ShipState.OnHit(other.GetComponentInParent<Bullet>().GetDamage());
			Destroy(other.gameObject);
		}
	}

}
