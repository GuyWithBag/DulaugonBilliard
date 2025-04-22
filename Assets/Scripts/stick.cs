using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stick : MonoBehaviour {

	public Text scoreText1; 
	public Text scoreText2;
	public Rigidbody rb; 
	public static float ScrollSpeed;

	// Use this for initialization
	void Start () {
		GetComponent<ConstantForce> ().enabled = false;
		rb = GetComponent<Rigidbody> ();
		rb.isKinematic = true; 
	}

	// Update is called once per frame
	void Update () {
		if (ScrollSpeed <= 2500) 
			ScrollSpeed += 15;
		 else 
			ScrollSpeed = 0; 
		

		if (Input.GetButtonUp("Fire1")) {
			rb.isKinematic = false;
			GetComponent<ConstantForce> ().enabled = true;
			rb.AddForce(transform.up * ScrollSpeed, ForceMode.Force);
		}
	}

	void OnCollisionEnter(Collision other) 
	{
		if (other.gameObject.CompareTag("2")) 
		{
			if (GameFlow.playerturn == 1) {
				scoreText1.text = "player 1 score: you lost";
				scoreText2.text = "player 2 score: " + GameFlow.score2;
			}
			if (GameFlow.playerturn == 2) {
				scoreText1.text = "player 2 score: you lost";
				scoreText2.text = "player 1 score: " + GameFlow.score1;
			}
		}
		if (other.gameObject.CompareTag ("Cue Ball")) {
			Destroy (gameObject);
		}
	}
}