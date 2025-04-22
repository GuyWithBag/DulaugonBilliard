using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrollbar : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.GetComponent<Scrollbar> ().size = 0;
	}

	// Update is called once per frame
	void Update () {
		transform.GetComponent<Scrollbar> ().size = (stick.ScrollSpeed) / 2500;
	}
}