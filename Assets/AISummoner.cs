using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISummoner : MonoBehaviour
{
	public EnnemyWave waveManager;

	private void Start()
	{
		

		waveManager.StartWaves();
	}

}
