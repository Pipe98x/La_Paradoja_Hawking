using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menumuerte : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void toggleEndmenu(float score) {
		gameObject.SetActive (true);
	}
}
