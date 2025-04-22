using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Assign to billboard
public class Math : MonoBehaviour {

	public TextMesh[] num;

	void Start() 
	{
		
		num = GetComponentsInChildren<TextMesh>();
		AssignRandomNumbers();
		ComputeAndDisplaySolution();
	}

	void AssignRandomNumbers() 
	{
		for (int i = 0; i < 5 && i < num.Length; i++) 
		{
			int randomValue = Random.Range(1, 10);
			num[i].text = randomValue.ToString();
		}
	}

	void ComputeAndDisplaySolution() 
	{
		if (num.Length < 5) {
			Debug.LogError("Not enough elements in an array");
			return;
		}

		int[] multiplicandDigits = new int[3];
		for (int i = 0; i < 3; i++) 
		{
			if (!int.TryParse(num[i].text, out multiplicandDigits[i])) 
			{  
				Debug.LogError($"Invalid digit at num[{i}]");
				return;
			}
		}

		int[] multiplierDigits = new int[2];
		for (int i = 3; i < 5; i++) 
		{
			if (!int.TryParse(num[i].text, out multiplierDigits[i - 3])) {  
				Debug.LogError($"Invalid digit at num[{i}]");
				return;
			}
		}

		int multiplicand = multiplicandDigits[2] * 100 + multiplicandDigits[1] * 10 + multiplicandDigits[0];

		int multiplier = multiplierDigits[1] * 10 + multiplierDigits[0];

		int partial1 = multiplicand * (multiplier % 10);
		int partial2 = multiplicand * (multiplier / 10);
		int total = multiplicand * multiplier;  

		int[] partial1Digits = GetDigits(partial1, 4);
		int[] partial2Digits = GetDigits(partial2, 4);
		int[] totalDigits = GetDigits(total, 6);

		for (int i = 0; i < 4; i++) 
		{
			int index = 5 + i;
			if (index < num.Length) 
				num[index].text = partial1Digits[i].ToString();	
		}

		for (int i = 0; i < 4; i++) 
		{
			int index = 9 + i;
			if (index < num.Length) 
				num[index].text = partial2Digits[i].ToString();
			
		}

		for (int i = 0; i < 6; i++) 
		{
			int index = 13 + i;
			if (index < num.Length) 
				num[index].text = totalDigits[i].ToString();
			
		}
	}

	void Update() { }

	int[] GetDigits(int number, int requiredLength) {
		string numberStr = number.ToString().PadLeft(requiredLength, '0');

		int[] digits = new int[requiredLength];
		for (int i = 0; i < requiredLength; i++) {
			digits[i] = numberStr[numberStr.Length - 1 - i] - '0';
		}
		return digits;
	}
}
