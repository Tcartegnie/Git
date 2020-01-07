using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnnemyList", menuName = "EnnemyList", order = 1)]
public class EnnemyList : ScriptableObject
{
	[SerializeField]
    List<ShipType> shipTypes = new List<ShipType>();
	public List<ShipType>GetShips()
	{
		return shipTypes;
	}
}
