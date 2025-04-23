using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFlow : MonoBehaviour
{

	public static int score1 = 0;
	public static int score2 = 0;
	public static int playerturn = 1;
	public static int misstarget = 0;
	public static int balltohit = 1;
	public static int[] balls = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
	public Text turnText;
	public Text scoreText1;
	public Text scoreText2;

	// Use this for initialization
	void Start()
	{
		UpdateUI();
	}

	// Update is called once per frame
	void Update()
	{
		UpdateUI();
	}

	void UpdateUI()
	{
		if (turnText != null)
		{
			turnText.text = "Player " + playerturn + "'s Turn";
		}
		if (scoreText1 != null)
		{
			scoreText1.text = "Player 1 Score: " + score1;
		}
		if (scoreText2 != null)
		{
			scoreText2.text = "Player 2 Score: " + score2;
		}
	}

	public static void SwitchTurn()
	{
		playerturn = playerturn == 1 ? 2 : 1;
	}

	public static void AddScore(int points)
	{
		if (playerturn == 1)
		{
			score1 += points;
		}
		else
		{
			score2 += points;
		}
	}
}
