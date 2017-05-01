using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Camara : MonoBehaviour {

	private Transform MirarA;
	private Vector3 IniciarEn;
	private Vector3 movimiento;
	private Transform rotacion;
	private float giro;
	private int Grados=0;
	public int[] Rotaciones;
	private bool puede = false;
	private int rnd;
	private float gradosZ;
	private int tiempo = 20;
	private float donde;


	// Use this for initialization
	void Start () {
		MirarA = GameObject.FindGameObjectWithTag ("Player").transform; // que siga al jugador
		IniciarEn = transform.position - MirarA.position; // que comience un poco antes para lograr la vista en tercera persona
	}
	
	// Update is called once per frame
	void Update ()
	{
		gradosZ = transform.rotation.eulerAngles.z;
		movimiento = MirarA.position + IniciarEn;
		Vector3 newrotatio = transform.position;
		donde = GameObject.FindGameObjectWithTag ("Player").transform.position.z;

		///x
		movimiento.x = 0;  // para que la camara no se mueva lateralmente
		movimiento.y = 7.3F;
		transform.position = movimiento;

		if ((Grados == 0)) {
		StartCoroutine (volteo ());
		}
		if (Grados == 1) {
			StartCoroutine (normal ());
		}

		if (donde <= 400)
			tiempo = 20;
		if ((donde > 400) && (donde <=800))
			tiempo = 15;
		if ((donde > 800) &&(donde <=1500))
			tiempo = 10;
		if (donde > 1500)
			tiempo = 5;
			



	}
		
	IEnumerator normal()
	{
		yield return new WaitForSeconds(15);
		if (puede == true) {
			transform.Rotate(0,0,Rotaciones[Random.Range(0,4)]);
			puede = false;
			Grados = 0;
		}

	}

	IEnumerator volteo ()
	{
		yield return new WaitForSeconds(15);
		if (puede == false) {
			transform.Rotate(0,0,Rotaciones[Random.Range(0,4)]);
			puede = true;
			Grados = 1;
		
				}
	}


	}



