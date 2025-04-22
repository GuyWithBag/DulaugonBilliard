using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gems : MonoBehaviour {

	public GameObject[] gemsArray;
	public TextMesh[] numbers;
	private int[] activeNumbers = new int[100];
	private int activeCount = 0;


	void Start () {
		numbers = GetComponentsInChildren<TextMesh>(true);
		MyLife.numlife = 3;
		HideallGems ();
		HideallNumbers ();

	}

	public void ShowNumbers(int value)
	{
		bool IsCorrectNumber = false;
		for (int i = 5; i < numbers.Length; i++) 
		{
			if (numbers [i] != null) 
			{
				int num;
				if (int.TryParse (numbers [i].text, out num) && num == value) 
				{
					IsCorrectNumber = true;
					break;
				}
			}
		}

		if (!IsCorrectNumber) {
			MyLife.numlife -= 1;
			print ("Life decreased! Current Life: " + MyLife.numlife);
			Audience.c = 0;
			return;
		}

		if (!IsNumberActive (value)) 
		{
			activeNumbers [activeCount] = value;
			activeCount++;
			MyLife.numlife += 1;
			print("Life increased! Current Life: " + MyLife.numlife);
			Audience.c = 1;
		}

		UpdateVisibility ();

	}

	private bool IsNumberActive(int value)
	{
		for (int i = 0; i < activeCount; i++) 
		{
			if (activeNumbers [i] == value)
				return true;
		}
		return false;
	}

	private void HideallGems(){

		foreach (GameObject gem in gemsArray)
			gem.SetActive (false);
	}

	private void HideallNumbers(){
		for (int i = 5; i < numbers.Length; i++) 
		{
			if (numbers [i] != null)
				numbers [i].gameObject.SetActive (false);
		}
	}

	private void UpdateVisibility()
	{

		for (int j = 0; j < activeCount; j++) 
		{
			int activeValue = activeNumbers [j];
			for (int i = 5; i < numbers.Length; i++) 
			{
				if (numbers [i] != null && int.Parse (numbers [i].text) == activeValue)
					numbers [i].gameObject.SetActive (true);
			}
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
