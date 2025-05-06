using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stick_move : MonoBehaviour {
	Vector3 tempPos;
	public ArUcoPoseReceiver arUcoPoseReceiver;
	private GameObject cueBall;
	private Vector3 offsetFromCueBall = new Vector3(0, 0, -1f);
	private float rotationSpeed = 50f;
	// Use this for initialization
	void Start () {
		cueBall = gameObject;  // Get the GameObject this script is attached to
		if (cueBall != null)
		{
			// transform.position = cueBall.transform.position + offsetFromCueBall;
		}
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 temp;

		if(Input.GetKey("w")){
			transform.Rotate (0,0,1);
		}
		if(Input.GetKey("s")){
			transform.Rotate (0,0,-1);
		}
		if(Input.GetKey("a")){
			transform.Rotate (0,1,1);
		}
		if(Input.GetKey("d")){
			transform.Rotate (0,-1,0);
		}
		if (Input.GetKey ("right")){
			temp = transform.position;
			tempPos.x += 0.05f;
			transform.position = tempPos;
		}
		if (Input.GetKey ("left")){
			tempPos = transform.position;
			tempPos.x -= 0.05f;
			transform.position = tempPos;
		}
		if (Input.GetKey ("up")){
			tempPos = transform.position;
			tempPos.z += 0.05f;
			transform.position = tempPos;
		}
		if (Input.GetKey ("down")){
			tempPos = transform.position;
			tempPos.z -= 0.05f;
			transform.position = tempPos;
		}

		if (arUcoPoseReceiver == null || cueBall == null) return;

		// Get the direction from cue ball to ArUco marker
		// Vector3 directionToMarker = arUcoPoseReceiver.transform.position - cueBall.transform.position;
		
		// Print ArUco marker position
		// Debug.Log("ArUco Position: " + arUcoPoseReceiver.transform.position);
		// Debug.Log("ArUco Rotation: " + arUcoPoseReceiver.transform.rotation.eulerAngles);
		transform.rotation = arUcoPoseReceiver.transform.rotation;
		// Only rotate if we have a valid direction
		//if (directionToMarker.magnitude > 0.1f)
		//{
			// Calculate the target rotation
			//Quaternion targetRotation = Quaternion.LookRotation(-directionToMarker);
			
			// Smoothly rotate towards the target
			//transform.rotation = Quaternion.Slerp(
			//	transform.rotation,
			//	targetRotation,
			//	rotationSpeed * Time.deltaTime
			//);

			// Update position to maintain offset from cue ball
			// transform.position = cueBall.transform.position + transform.rotation * offsetFromCueBall;
		//}

		
	}
}
