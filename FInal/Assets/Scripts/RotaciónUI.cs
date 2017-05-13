using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaciónUI : MonoBehaviour {
	private float Grados;

	// Use this for initialization
	void Start () {


	}

	// Update is called once per frame
	void Update () {
		Grados = GameObject.Find ("Main Camera").GetComponent<Camara> ().gradosZ;

		if (Grados < 2) {
			transform.rotation = Quaternion.Euler (0, 0, -0);
		}

		if (Grados < 100 && Grados > 80) {
			transform.rotation = Quaternion.Euler (0, 0, -90);
		}

		if (Grados < 190 && Grados > 170) {
			transform.rotation = Quaternion.Euler (0, 0, 180);
		}

		if (Grados < 280 && Grados > 260) {
			transform.rotation = Quaternion.Euler (0, 0, 90);
			
		}

		Debug.Log (Grados);

	}
}

	
