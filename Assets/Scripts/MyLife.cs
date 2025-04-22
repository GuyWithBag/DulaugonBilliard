using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyLife : MonoBehaviour {

	public static int numlife;
	public GameObject life1;
	public GameObject life2;
	public GameObject life3;
	public GameObject life4;
	public GameObject life5;
	public GameObject life6;
	public GameObject life7;
	public GameObject life8;
	public GameObject life9;
	void Start () 
	{
		life1.SetActive (false);
		life2.SetActive (false);
		life3.SetActive (false);
		life4.SetActive (false);
		life5.SetActive (false);
		life6.SetActive (false);
		life7.SetActive (false);
		life8.SetActive (false);
		life9.SetActive (false);
	}

	// Update is called once per frame
	void Update () {

		ClearLife ();

		if (numlife == 1) {
			life1.SetActive (true);
		}
		if (numlife == 2) {
			life1.SetActive (true);
			life2.SetActive (true);
		}
		if (numlife == 3) {
			life1.SetActive (true);
			life2.SetActive (true);
			life3.SetActive (true);
		}
		if (numlife == 4) {
			life1.SetActive (true);
			life2.SetActive (true);
			life3.SetActive (true);
			life4.SetActive (true);
		}
		if (numlife == 5) {
			life1.SetActive (true);
			life2.SetActive (true);
			life3.SetActive (true);
			life4.SetActive (true);
			life5.SetActive (true);
		}
		if (numlife == 6) {
			life1.SetActive (true);
			life2.SetActive (true);
			life3.SetActive (true);
			life4.SetActive (true);
			life5.SetActive (true);
			life6.SetActive (true);
		}
		if (numlife == 7) {
			life1.SetActive (true);
			life2.SetActive (true);
			life3.SetActive (true);
			life4.SetActive (true);
			life5.SetActive (true);
			life6.SetActive (true);
			life7.SetActive (true);
		}
		if (numlife == 8) {
			life1.SetActive (true);
			life2.SetActive (true);
			life3.SetActive (true);
			life4.SetActive (true);
			life5.SetActive (true);
			life6.SetActive (true);
			life7.SetActive (true);
			life8.SetActive (true);
		}
		if (numlife == 9) {
			life1.SetActive (true);
			life2.SetActive (true);
			life3.SetActive (true);
			life4.SetActive (true);
			life5.SetActive (true);
			life6.SetActive (true);
			life7.SetActive (true);
			life8.SetActive (true);
			life9.SetActive (true);
		}

	}

	public void ClearLife(){
		life1.SetActive (false);
		life2.SetActive (false);
		life3.SetActive (false);
		life4.SetActive (false);
		life5.SetActive (false);
		life6.SetActive (false);
		life7.SetActive (false);
		life8.SetActive (false);
		life9.SetActive (false);

	}
}
