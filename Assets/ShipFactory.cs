using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShipType
{
	Scout = 0
}

public class ShipFactory : MonoBehaviour
{

	public GameObject Scout;

	List<GameObject> Ennemies = new List<GameObject>();

	Dictionary<ShipType, GameObject> PrefabShip = new Dictionary<ShipType, GameObject>();

	public void Awake()
	{
		PrefabShip.Add(ShipType.Scout, Scout);
	}



	public GameObject GetShipGameObject(ShipType ShipType)
	{
		return PrefabShip[ShipType];
	}

	public void InstantiateShip(ShipType shiptype,Vector3 Position)
	{

		GameObject go = Instantiate(GetShipGameObject(shiptype),Position, new Quaternion());
		Ennemies.Add(go);
	}

	public List<GameObject> GetEnnemiesList()
	{
		return Ennemies;
	}

}
