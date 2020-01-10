using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShipType
{
	Scout = 0
}

public class ShipFactory : MonoBehaviour
{

	public delegate void OnEnnemyListEmpty();

	public OnEnnemyListEmpty onEnnemyListEmpty;

	public GameObject Scout;

	List<GameObject> Ennemies = new List<GameObject>();

	Dictionary<ShipType, GameObject> PrefabShip = new Dictionary<ShipType, GameObject>();

	public List<Transform> Spawns = new List<Transform>();




	public List<GameObject> GetEnnemiesList()
	{
		return Ennemies;
	}

	public void Awake()
	{
		PrefabShip.Add(ShipType.Scout, Scout);
	}

	public GameObject GetShipGameObject(ShipType ShipType)
	{
		return PrefabShip[ShipType];
	}

	public void InstantiateWave(List<ShipType>ListShip)
	{
		for (int i = 0; i < ListShip.Count; i++)
		{
			InstantiateShip(ListShip[i], Spawns[Random.Range(0, Spawns.Count)].position);
		}
	}

	public void InstantiateShip(ShipType shiptype,Vector3 Position)
	{
		GameObject go = Instantiate(GetShipGameObject(shiptype),Position, new Quaternion());
		go.GetComponent<ShipState>().ShipDestroy += OnShipDestroy;
		Ennemies.Add(go);
	}

	public void InitEnnemies(GameObject Player,AsteroïdField asteroïdField)
	{
		for (int i = 0; i < Ennemies.Count; i++)
		{
			Ennemies[i].GetComponent<IAMove>().target = Player.transform;
			Ennemies[i].GetComponent<IAMove>().colliders = asteroïdField.Asteroids;
			Ennemies[i].GetComponent<AiAttack>().target = Player;
		}
	}
	
	public void OnShipDestroy(GameObject ship)
	{
		Ennemies.Remove(ship);
		CheckListEmpty();
	}
	
	private bool IsListEmpty()
	{
		if(Ennemies.Count == 0)
		{
			return true;
		}
		return false;
	}

	public void CheckListEmpty()
	{
		if(IsListEmpty())
		{
			onEnnemyListEmpty();
		}
	}

}
