using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollider : Ship
{
	public void OnTriggerEnter(Collider other)
	{
		Debug.Log(gameObject.name);
		Debug.Log(other.tag + " is triggering with " + gameObject.name);
		if (other.tag == "Bullet")
		{
			ShipState.OnHit(other.GetComponentInParent<Bullet>().damage);
			Destroy(other.gameObject);
		}
	}

}
