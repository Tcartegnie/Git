using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShipType
{
	Scout = 0,
	Cruiser = 1
}

public class ShipFactory : MonoBehaviour
{
	public GameObject Scout;	
	public GameObject Cruiser;
	public GameObject Player;


	Dictionary<ShipType, GameObject> PrefabShip = new Dictionary<ShipType, GameObject>();





	public void Awake()
	{
		PrefabShip.Add(ShipType.Scout, Scout);
		PrefabShip.Add(ShipType.Cruiser, Cruiser);
	}

	public GameObject GetShipGameObject(ShipType ShipType)
	{
		return PrefabShip[ShipType];
	}

	public GameObject InstantiateShip(ShipType shiptype,Vector3 Position)
	{
		GameObject entity = Instantiate(GetShipGameObject(shiptype),Position, new Quaternion());
		InitShip(entity);
		return entity;

	}

	public void InitShip(GameObject Ship)
	{
		//Ship.GetComponent<ShipState>().ShipDestroy += OnShipDestroy;//?
		if (Ship.GetComponent<IAMove>()!= null)
		{
			Ship.GetComponent<IAMove>().target = Player.transform;
			//Ennemies[i].GetComponent<IAMove>().colliders = asteroïdField.Asteroids;
		}

		if (Ship.GetComponent<AiAttack>() != null)
		{
			Ship.GetComponent<AiAttack>().target = Player;
		}	
	}
	

}
