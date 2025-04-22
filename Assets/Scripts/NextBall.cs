using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextBall : MonoBehaviour {
	public Text oneText;
	public Image Ball1;

	public Text twoText;
	public Image Ball2;

	public Text threeText;
	public Image Ball3;

	public Text fourText;
	public Image Ball4;

	public Text fiveText;
	public Image Ball5;

	public Text sixText;
	public Image Ball6;

	public Text sevenText;
	public Image Ball7;

	public Text eightText;
	public Image Ball8;

	public Text nineText;
	public Image Ball9;
	private gems gemScript;


	// Use this for initialization
	void Start () {
		Ball1.enabled = false;
		oneText.enabled = false;

		Ball2.enabled = false;
		twoText.enabled = false;

		Ball3.enabled = false;
		threeText.enabled = false;

		Ball4.enabled = false;
		fourText.enabled = false;

		Ball5.enabled = false;
		fiveText.enabled = false;

		Ball6.enabled = false;
		sixText.enabled = false;

		Ball7.enabled = false;
		sevenText.enabled = false;

		Ball8.enabled = false;
		eightText.enabled = false;

		Ball9.enabled = false;
		nineText.enabled = false;
		if (gemScript == null) 
		{
			gemScript = GameObject.FindObjectOfType<gems> ();
		}


	}
		
	void DestroyObjectWithTag(string tag)
	{
		GameObject obj = GameObject.FindWithTag (tag);
		if (obj != null) 
		{
			Destroy (obj);
		} else 
		{
			print("No GameObject found with the tag: " + tag);
		}
	}

	void PrintBallsArray(){
		string contents = "Balls array";
		for(int i = 0; i < GameFlow.balls.Length; i++){
			contents += GameFlow.balls[i] + " ";
		}

	}
		
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.CompareTag ("Cue Ball")) {
			gemScript.ShowNumbers (0);
			DestroyObjectWithTag ("Cue Ball");
		}
		if (collision.gameObject.CompareTag ("1")) {
			GameFlow.balls [0] = 0;
			gemScript.ShowNumbers (1);
			Ball1.enabled = true;
			oneText.enabled = true;
			DestroyObjectWithTag ("1");
			SaveXML.save_now = true;
		}
		else if (collision.gameObject.CompareTag ("2")) {
			GameFlow.balls [1] = 0;
			gemScript.ShowNumbers (2);
			Ball2.enabled = true;
			twoText.enabled = true; 
			DestroyObjectWithTag ("2");
		}
		else if(collision.gameObject.CompareTag ("3")) {
			GameFlow.balls [2] = 0;
			gemScript.ShowNumbers (3);
			Ball3.enabled = true;
			threeText.enabled = true; 
			DestroyObjectWithTag ("3");
		}
		else if (collision.gameObject.CompareTag ("4")) {
			GameFlow.balls [3] = 0;
			gemScript.ShowNumbers (4);
			Ball4.enabled = true;
			fourText.enabled = true; 
			DestroyObjectWithTag ("4");
		}
		else if (collision.gameObject.CompareTag ("5")) {
			GameFlow.balls [4] = 0;
			gemScript.ShowNumbers (5);
			Ball5.enabled = true;
			fiveText.enabled = true; 
			DestroyObjectWithTag ("5");
		}
		else if (collision.gameObject.CompareTag ("6")) {
			GameFlow.balls [5] = 0;
			gemScript.ShowNumbers (6);
			Ball6.enabled = true;
			sixText.enabled = true;
			DestroyObjectWithTag ("6");
		}
		else if (collision.gameObject.CompareTag ("7")) {
			GameFlow.balls [6] = 0;
			gemScript.ShowNumbers (7);
			Ball7.enabled = true;
			sevenText.enabled = true;
			DestroyObjectWithTag ("7");
		}
		else if (collision.gameObject.CompareTag ("8")) {
			GameFlow.balls [7] = 0;
			gemScript.ShowNumbers (8);
			Ball8.enabled = true;
			eightText.enabled = true;
			DestroyObjectWithTag ("8");
		}
		else if (collision.gameObject.CompareTag ("9")) {
			GameFlow.balls [8] = 0;
			gemScript.ShowNumbers (9);
			Ball9.enabled = true;
			nineText.enabled = true;
			DestroyObjectWithTag ("9");
		}

		PrintBallsArray();
	}
		
}
