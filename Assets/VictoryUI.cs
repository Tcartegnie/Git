using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryUI : MonoBehaviour
{
	public GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
		GM = GameManager.instance;
		GM.onVictory += OnVictory;
	}

	public float FadeTime;
	public Image image;
	public Text VictoryText;


	public void OnVictory()
	{
		StartCoroutine(FadeScreenGameOver());
	}

	public IEnumerator FadeScreenGameOver()
	{
		for (float time = 0.0f; time < 1; time += Time.deltaTime / FadeTime)
		{
			image.color = new Color(0, 0, 0, time);
			Color color = VictoryText.color;
			VictoryText.color = new Color(color.a, color.b, color.g, time);
			yield return null;
		}
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
}
