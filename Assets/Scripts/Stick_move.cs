using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stick_move : MonoBehaviour {
	Vector3 tempPos;

	// Use this for initialization
	void Start () {
		
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
	}
}
