using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stick : MonoBehaviour
{
	public Text scoreText1;
	public Text scoreText2;
	public Rigidbody rb;
	public static float ScrollSpeed;
	public GameObject cueBall;
	private bool isWaitingForBallsToStop = false;
	private float checkInterval = 0.5f;
	private float lastCheckTime;
	private GameObject newStick;
	private bool hasCreatedNewStick = false;  // Flag to track if we've created a new stick
	private Vector3 originalPosition;  // Store the original position
	private float rotationSpeed = 50f;
// 0.693 -0.194 -6.453
	// Use this for initialization
	void Start()
	{
		GetComponent<ConstantForce> ().enabled = false;
		rb = GetComponent<Rigidbody> ();
		rb.isKinematic = true; 
		lastCheckTime = Time.time;
		originalPosition = transform.position;  // Store the initial position
	}

	// Update is called once per frame
	void Update()
	{
		if (ScrollSpeed <= 2500) 
			ScrollSpeed += 15;
		 else 
			ScrollSpeed = 0; 
		

		if (Input.GetButtonUp("Fire1")) {
			rb.isKinematic = false;
			GetComponent<ConstantForce> ().enabled = true;
			rb.AddForce(transform.up * ScrollSpeed, ForceMode.Force);
			isWaitingForBallsToStop = true;
		}

		// Check if all balls have stopped moving
		if (isWaitingForBallsToStop && Time.time - lastCheckTime >= checkInterval)
		{
			lastCheckTime = Time.time;
			if (AreAllBallsStopped())
			{
				isWaitingForBallsToStop = false;
				SwitchPlayerTurn();
				if (!hasCreatedNewStick)  // Only create new stick if we haven't already
				{
					CreateNewStick();
					hasCreatedNewStick = true;  // Mark that we've created a new stick
				}
			}
		}
	}

	void SwitchPlayerTurn()
	{
		GameFlow.playerturn = GameFlow.playerturn == 1 ? 2 : 1;
	}

	void CreateNewStick()
	{
		if (cueBall != null)
		{
			transform.position = originalPosition;
			GetComponent<ConstantForce> ().enabled = false;
			// Make the stick invisible
			GetComponent<MeshRenderer>().enabled = true;
			// Disable physics
			GetComponent<Collider>().enabled = true;
			// Disable the Rigidbody
			GetComponent<Rigidbody>().isKinematic = true;
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("2"))
		{
			if (GameFlow.playerturn == 1)
			{
				scoreText1.text = "player 1 score: you lost";
				scoreText2.text = "player 2 score: " + GameFlow.score2;
			}
			if (GameFlow.playerturn == 2)
			{
				scoreText1.text = "player 2 score: you lost";
				scoreText2.text = "player 1 score: " + GameFlow.score1;
			}
		}
		if (other.gameObject.CompareTag("Cue Ball"))
		{
			// Make the stick invisible
			GetComponent<MeshRenderer>().enabled = false;
			// Disable physics
			GetComponent<Collider>().enabled = false;
			// Disable the Rigidbody
			GetComponent<Rigidbody>().isKinematic = true;
		}
	}

	private bool AreAllBallsStopped()
	{
		// Implement the logic to check if all balls have stopped moving
		// This is a placeholder and should be replaced with the actual implementation
		return false;
	}
}