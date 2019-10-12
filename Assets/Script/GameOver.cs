using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

	public float FadeTime;
	public Image image;
	public Text GameOverText;
	public RectTransform ButtonRect;
	public ShipState shipstate;
	public void Start()
	{
		shipstate.Gameover += FadeGameOver;
	}

	public void FadeGameOver()
	{
		StartCoroutine(FadeScreenGameOver());
	}

	public IEnumerator FadeScreenGameOver()
	{
		for(float time = 0.0f; time < 1; time+= Time.deltaTime/FadeTime)
		{
		
			image.color = new Color(0,0,0,time);
			Color color = GameOverText.color;
			GameOverText.color = new Color(color.a,color.b,color.g,time);
			yield return null;
		}
		ButtonRect.gameObject.SetActive(true);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
}
