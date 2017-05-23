using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	private int score;
	public Text scoreText;

	private void Start()
	{
		score = PlayerPrefs.GetInt ("PlayerScore");
		scoreText.text = "Highscore : " + score.ToString();
	}

	public void ToGame()
	{
		Application.LoadLevel("scene00");
	}

}
