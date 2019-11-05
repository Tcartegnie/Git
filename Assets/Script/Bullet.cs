using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float speed;
	public float damage;
	// Start is called before the first frame update
	public void Update()
	{
		transform.position += transform.forward * (Time.deltaTime * speed);
	}
}
