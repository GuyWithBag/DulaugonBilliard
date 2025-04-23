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

	// Use this for initialization
	void Start()
	{
		GetComponent<ConstantForce>().enabled = false;
		rb = GetComponent<Rigidbody>();
		rb.isKinematic = true;
		lastCheckTime = Time.time;
	}

	// Update is called once per frame
	void Update()
	{
		if (ScrollSpeed <= 2500)
			ScrollSpeed += 15;
		else
			ScrollSpeed = 0;

		if (Input.GetButtonUp("Fire1"))
		{
			rb.isKinematic = false;
			GetComponent<ConstantForce>().enabled = true;
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
				CreateNewStick();
			}
		}
	}

	bool AreAllBallsStopped()
	{
		Rigidbody[] allBalls = FindObjectsOfType<Rigidbody>();
		foreach (Rigidbody ballRb in allBalls)
		{
			if (ballRb.velocity.magnitude > 0.1f)
			{
				return false;
			}
		}
		return true;
	}

	void SwitchPlayerTurn()
	{
		GameFlow.playerturn = GameFlow.playerturn == 1 ? 2 : 1;
	}

	void CreateNewStick()
	{
		if (cueBall != null)
		{
			// Create a new stick at the cue ball's position
			newStick = new GameObject("PoolStick");
			newStick.transform.position = cueBall.transform.position + new Vector3(0, 0, -1f);

			// Add and set up the Rigidbody
			Rigidbody newRb = newStick.AddComponent<Rigidbody>();
			newRb.isKinematic = true;

			// Add the stick script
			stick newStickScript = newStick.AddComponent<stick>();
			newStickScript.scoreText1 = scoreText1;
			newStickScript.scoreText2 = scoreText2;
			newStickScript.cueBall = cueBall;

			// Add ConstantForce component
			ConstantForce newCf = newStick.AddComponent<ConstantForce>();
			newCf.enabled = false;

			// Copy the mesh and collider from the original stick
			MeshFilter originalMesh = GetComponent<MeshFilter>();
			MeshCollider originalCollider = GetComponent<MeshCollider>();

			if (originalMesh != null)
			{
				MeshFilter newMesh = newStick.AddComponent<MeshFilter>();
				newMesh.sharedMesh = originalMesh.sharedMesh;
			}

			if (originalCollider != null)
			{
				MeshCollider newCollider = newStick.AddComponent<MeshCollider>();
				newCollider.sharedMesh = originalCollider.sharedMesh;
			}

			// Copy the material
			MeshRenderer originalRenderer = GetComponent<MeshRenderer>();
			if (originalRenderer != null)
			{
				MeshRenderer newRenderer = newStick.AddComponent<MeshRenderer>();
				newRenderer.material = originalRenderer.material;
			}
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
			Destroy(gameObject);
		}
	}
}