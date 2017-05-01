using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

	private int jugador = 1;
	public GameObject[] jugadoresPrefab;


	// Use this for initialization
	void Start () {
		if (jugador == 1) {
			GameObject go;
			go = Instantiate (jugadoresPrefab [0]) as GameObject;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
