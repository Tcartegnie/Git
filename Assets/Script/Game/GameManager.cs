using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public ShipFactory shipFactory;
	static GameManager instance;
	public GameObject Player;
	public AsteroïdField asteroidfield;
	public static GameManager Instance { get => instance; set => instance = value; }
	[SerializeField]
	EnnemyList ennemyList;

	public List<Transform> Spawns = new List<Transform>();

	public List<GameObject> InstanciedEntitys = new List<GameObject>();

	public void Start()
	{
		InstanciateEnnemiesWaves();
	}

	public void InstanciateEnnemiesWaves()
	{
		List<ShipType> currentEnnemyList = ennemyList.GetShips();
		for(int i = 0; i < currentEnnemyList.Count; i++)
		{
			shipFactory.InstantiateShip(currentEnnemyList[i], Spawns[Random.Range(0,Spawns.Count)].position);
		}

		InstanciedEntitys = shipFactory.GetEnnemiesList();

		for(int i = 0; i < InstanciedEntitys.Count;i++)
		{
			InstanciedEntitys[i].GetComponent<IAMove>().target = Player.transform;
			InstanciedEntitys[i].GetComponent<IAMove>().colliders = asteroidfield.Asteroids;
			InstanciedEntitys[i].GetComponent<AiAttack>().target = Player;
		}
	}
}
