using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collision){
		if (collision.gameObject.CompareTag ("Cue Ball")) {
			Cueball.ResetCueballPosition(collision.gameObject);
			Debug.Log("ASD");
			return;
		} 
		if (collision.gameObject && collision.gameObject.GetComponent<SafeBox>()) {
			Destroy(collision.gameObject);
		}
	}
}
