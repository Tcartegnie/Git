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
	public EnnemyWave waveManager;





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
		waveManager.onEndWaves += EndGameVictory;
		waveManager.StartWaves();
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
