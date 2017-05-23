using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour 
{
	public static LevelManager Instance{ set; get; }
	private int hitpoint = 3;
	private int score = 0;
	public Transform spawnPosition;
	public Transform playerTransform;

	public Text scoreText;
	public Text hitpointText;

	private void Awake()
	{
		Instance = this;
		scoreText.text = "Punkte : " + score.ToString();
		hitpointText.text = "Leben : " + hitpoint.ToString();
	}

	private void Update()
	{
		
		if (playerTransform.position.y < -5) 
		{
			playerTransform.position = spawnPosition.position;
			hitpoint--;
			hitpointText.text = "Leben : " + hitpoint.ToString();
			if (hitpoint <= 0) 
			{
				Application.LoadLevel("sceneMenu");
			}
		}
	}

	public void win()
	{
		if (PlayerPrefs.GetInt ("PlayerScore") < score)
		{
			PlayerPrefs.SetInt ("PlayerScore", score);
		}
			Application.LoadLevel("sceneMenu");
	}

	public void CollectCoin()
	{
		score++;
		scoreText.text = "Punkte : " + score.ToString();
	}
}