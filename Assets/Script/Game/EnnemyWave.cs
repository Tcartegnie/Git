using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum WaveType
{
FastForward,
Random,
Loop,
}


public class EnnemyWave : MonoBehaviour
{
	[SerializeField]
	WaveType waveType;

	GameManager GM;
	public delegate void OnEndWaves();
	public OnEndWaves onEndWaves;

	int WaveIndex = 0;

	public ShipFactory shipFactory;

	GameObject Player;

	List<GameObject> Entities = new List<GameObject>();
	[SerializeField]
	List<EnnemyList> ennemyWave = new List<EnnemyList>();
	[SerializeField]
	List<Transform> Spawns = new List<Transform>();

	public bool IsLooping;


	public void Start()
	{
		GM = GameManager.instance;
		shipFactory = GM.shipFactory;
	}

	public void StartWaves()
	{
		WaveIndex = 0;
		InstanciateWaves(WaveIndex);
	}

	public void InstanciateWaves(int WaveID)//WaveManager
	{
		List<ShipType> ListShip = ennemyWave[WaveID].GetShips();
		for (int i = 0; i < ListShip.Count; i++)
		{
			GameObject Entity = new GameObject();
			Entity = shipFactory.InstantiateShip(ListShip[i], Spawns[Random.Range(0, Spawns.Count)].position);
			Entity.GetComponent<ShipState>().ShipDestroy += OnShipDestroy;
			Entities.Add(Entity);
		}
	}

	public void InstantiateNextWave()//WaveManager
	{
		WaveIndex += 1;
		InstanciateWaves(WaveIndex);
	}

	public void OnEnnemyWaveDone()//WaveManager
	{
		switch (waveType)
		{
			case (WaveType.FastForward):
				PlayNextWave();
				break;

			case (WaveType.Random):
				PlayRandomWave();
				break;

			case (WaveType.Loop):
				LoopWave();
				break;
		}
	}

	public void PlayRandomWave()
	{
		int RandomWave = Random.Range(0, ennemyWave.Count);
		Debug.Log(RandomWave);
		InstanciateWaves(RandomWave);
	}

	public void PlayNextWave()
	{
		if ((WaveIndex + 1) == ennemyWave.Count)
		{
			onEndWaves?.Invoke();
		}
		else
		{
			InstantiateNextWave();
		}
	}

	public void LoopWave()
	{
		if ((WaveIndex + 1) == ennemyWave.Count)
		{
			StartWaves();
		}
		else
		{
			InstantiateNextWave();
		}
	}


	public void OnShipDestroy(GameObject ship)
	{
		Entities.Remove(ship);
		CheckListEmpty();
	}

	private bool IsListEmpty()
	{
		if (Entities.Count == 0)
		{
			return true;
		}
		return false;
	}

	public void CheckListEmpty()
	{
		if (IsListEmpty())
		{
			OnEnnemyWaveDone();
		}
	}

}
