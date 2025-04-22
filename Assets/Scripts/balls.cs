  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balls : MonoBehaviour {

	public Rigidbody[] obj; 
	public Collider[] obj2; 

	public static int balls_moving = 0;

	// Use this for initialization
	void Start () {
		obj = GetComponentsInChildren<Rigidbody> ();
		obj2 = GetComponentsInChildren<Collider> ();

	}

	// Update is called once per frame
	void Update () {
		balls_moving = 0;

		foreach (Rigidbody t in obj) 
		{
			if (t != null) 
			{
				if (t.velocity != Vector3.zero) {
					balls_moving++;
				}
			}
		}


		if (balls_moving > 0) {

			Debug.Log (balls_moving + "ball/s moving");
		} 
		else 
		{
			Debug.Log ("No balls moving");
		}

	}
}
