using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	public ShipFactory shipFactory;
	public GameObject Player;

	public AsteroïdField asteroidfield;

	public static GameManager instance;

	public delegate void OnVictory();
	public delegate void OnGameOver();

	public OnVictory onVictory;
	public OnGameOver onGamover;


	[SerializeField]
	List<EnnemyList> ennemyWave;

	int WaveIndex = 0;


	public List<GameObject> InstanciedEntitys = new List<GameObject>();

	public void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if(instance!= null)
		{
			Destroy(instance);
		}

		Player.GetComponentInParent<ShipState>().ShipDestroy += EndGameGameOver;

	}

	public void Start()
	{
		shipFactory.onEnnemyListEmpty += OnEnnemyWaveDone;
		InstanciateWaves(WaveIndex);
	}

	public void InstanciateWaves(int WaveID)
	{
		shipFactory.InstantiateWave(ennemyWave[WaveID].GetShips());
		InstanciedEntitys = shipFactory.GetEnnemiesList();
		shipFactory.InitEnnemies(Player,asteroidfield);
	}
	  
	public void InstantiateNextWave()
	{
		WaveIndex += 1;
		InstanciateWaves(WaveIndex);
	}

	public void OnEnnemyWaveDone()
	{
		if ((WaveIndex +1) == ennemyWave.Count)
		{
			EndGameVictory();
		}
		else
		{
			InstantiateNextWave();
		}
	}

	public void EndGameVictory()
	{
		onVictory?.Invoke();
	}

	public void EndGameGameOver(GameObject test)
	{
		onGamover?.Invoke();
	}


}
