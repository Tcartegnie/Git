using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroïdField : MonoBehaviour
{

	public GameObject asteroide;
	public float NumberOfAsteroid;
	public int RangeOfField;
	public IAMove scoutIAmove;
	//Generate a random objectfield.

	private void Start()
	{
		GenerateAsteroidField();
	}

	public void GenerateAsteroidField()
	{
		for(int i = 0; i < NumberOfAsteroid;i++)
		{
			Vector3 RandomPosition = new Vector3(Random.Range(0, RangeOfField), Random.Range(0, RangeOfField), Random.Range(0, RangeOfField));
			GenerateAsteroide(RandomPosition);
		}

	}

	public void GenerateAsteroide(Vector3 Position)
	{
		GameObject GO = Instantiate(asteroide, Position, new Quaternion());
		scoutIAmove.colliders.Add(GO);
	}

}
