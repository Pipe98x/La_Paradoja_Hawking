using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour {
	public GameObject[] powerups;
	private Transform jugadorTransform;

	// Use this for initialization
	void Start () {
		jugadorTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		StartCoroutine (crearpower ());
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator crearpower() {
		yield return new WaitForSeconds (Random.Range(15,30));
		GameObject go;
		go = Instantiate (powerups[Random.Range(0,2)]) as GameObject;
		go.transform.position = new Vector3 (jugadorTransform.position.x, jugadorTransform.position.y + 0.5f, jugadorTransform.position.z + 40);

	
	}
}
