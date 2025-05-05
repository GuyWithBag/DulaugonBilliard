using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cueball : MonoBehaviour
{
	public Text scoreText1;
	public Text scoreText2;
	private Rigidbody rb;
	public static float ScrollSpeed;
	public GameObject stick;
	private GameObject newStick;
	public GameObject originalPositionObject;
	private float rotationSpeed = 50f;
	bool isFiring = false;
	public Timer fireCooldown;
	public GameObject objectCollider;

	// Use this for initialization
	void Start()
	{
		rb = stick.GetComponent<Rigidbody>();
	}

	// for cueball
	public static void ResetCueballPosition(GameObject obj) {
		obj.transform.position = new Vector3(0.693f, -0.194f, -6.453f);
		Cueball script = obj.GetComponent<Cueball>(); 
		script.ResetStick(); 
	}

	// Update is called once per frame
	void Update()
	{
		// if (stick != null)
		// {
		// 	// Calculate direction to cue ball, ignoring Y-axis
		// 	Vector3 directionToCueBall = stick.transform.position - transform.position;
		// 	directionToCueBall.y = 0; // Flatten to XZ plane
		// 	if (directionToCueBall != Vector3.zero)
		// 	{
		// 		transform.rotation = Quaternion.LookRotation(directionToCueBall);
		// 	}
		// }
		// Debug.Log(balls.is_moving);
		Vector3 myVector = gameObject.GetComponent<Rigidbody>().velocity;
		Debug.Log(myVector);
		if (myVector.x != 0 && myVector.y != 0 && myVector.z != 0) {
			Disable(true);
			
		} else {
			Disable(false);
		}
		

		if (balls.is_moving) {
			ResetStick();
			return;
		}

		if (ScrollSpeed <= 2500) 
		{
			ScrollSpeed += 25;
			
		}
		else 
		{
			ScrollSpeed = 0; 
		}
		
		float forceApplied = ScrollSpeed * 0.02f;
		//Debug.Log(forceApplied);
		if (Input.GetButtonUp("Fire1") && !fireCooldown.isRunning) {
			rb.isKinematic = false;
			ApplyForce(forceApplied); 	
			isFiring = true;
			fireCooldown.StartTimer();
		}

		if (!isFiring) {
			ResetStick();
		}

	}
	
	void SwitchPlayerTurn()
	{
		GameFlow.playerturn = GameFlow.playerturn == 1 ? 2 : 1;
	}

	public void ResetStick()
	{
		stick.transform.position = originalPositionObject.transform.position;
		stick.transform.rotation = originalPositionObject.transform.rotation;
		rb.velocity = Vector3.zero; // Stop linear movement
        rb.angularVelocity = Vector3.zero; // Stop rotation
		// Disable(false);
		// Debug.Log("WHYY");
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
		isFiring = false;
		// if (other.gameObject.CompareTag("Cue Ball"))
		// {
		// 	Disable(true);
		// }
	}

	private void Disable(bool value) {
		if (value) {
			// Make the Cueball invisible
			stick.GetComponent<MeshRenderer>().enabled = false;
			objectCollider.GetComponent<MeshRenderer>().enabled = false;
			// Disable physics
			objectCollider.GetComponent<Collider>().enabled = false;
			// Disable the Rigidbody
			rb.isKinematic = true;
			return;
		}
		stick.GetComponent<MeshRenderer>().enabled = true;
		objectCollider.GetComponent<MeshRenderer>().enabled = true;
		// Disable physics
		objectCollider.GetComponent<Collider>().enabled = true;
		// Disable the Rigidbody
		rb.isKinematic = true;
	}

	private void ApplyForce(float magnitude) {
		// gameObject.transform.LookAt(stick.transform);
		Vector3 forceDirection = stick.transform.up;
		rb.AddForce(forceDirection * magnitude, ForceMode.Impulse);

	}
}