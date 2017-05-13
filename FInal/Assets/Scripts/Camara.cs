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
	public float gradosZ;
	private int tiempo = 20;
	private float donde;
	public Text segundos;
	private float avisando = 4f;


	// Use this for initialization
	void Start () {
		MirarA = GameObject.FindGameObjectWithTag ("Player").transform; // que siga al jugador
		IniciarEn = transform.position - MirarA.position; // que comience un poco antes para lograr la vista en tercera persona
		GameObject.Find ("Aviso").GetComponent<Text> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		gradosZ = transform.rotation.eulerAngles.z;
		movimiento = MirarA.position + IniciarEn;
		Vector3 newrotatio = transform.position;
		donde = GameObject.FindGameObjectWithTag ("Player").transform.position.z;
		segundos.text = ((int)avisando).ToString();

		///x
		movimiento.x = 0;  // para que la camara no se mueva lateralmente
		movimiento.y = 7.3F;
		transform.position = movimiento;

		if ((Grados == 0)) {
		StartCoroutine (volteo ());
		StartCoroutine (aviso ());
		}
		if (Grados == 1) {
			StartCoroutine (normal ());
			StartCoroutine (aviso2 ());
		}

		if (donde <= 400)
			tiempo = 20;
		if ((donde > 400) && (donde <=800))
			tiempo = 15;
		if ((donde > 800) &&(donde <=1500))
			tiempo = 10;
		if (donde > 1500)
			tiempo = 5;

		if (avisando < 1.3) {
			GameObject.Find ("Aviso").GetComponent<Text> ().enabled = false;
		}
	}
		
	IEnumerator normal()
	{
		yield return new WaitForSeconds(18);
		if (puede == true) {
			GameObject.Find ("Aviso").GetComponent<Text> ().enabled = false;
			transform.Rotate(0,0,Rotaciones[Random.Range(0,4)]);
			puede = false;
			Grados = 0;
			avisando = 4f;
		}

	}

	IEnumerator volteo ()
	{
		yield return new WaitForSeconds(18);
		if (puede == false) {
			GameObject.Find ("Aviso").GetComponent<Text> ().enabled = false;
			transform.Rotate(0,0,Rotaciones[Random.Range(0,4)]);
			puede = true;
			Grados = 1;
			avisando = 4f;
		
				}
	}

	IEnumerator aviso ()
	{
		yield return new WaitForSeconds (15);
		if (puede == false) {
			GameObject.Find ("Aviso").GetComponent<Text> ().enabled = true;
			if (avisando > 1.2) {
				avisando -= Time.deltaTime;
			}
		}
	}

	IEnumerator aviso2 ()
	{
		yield return new WaitForSeconds (15);
		if (puede == true) {
			GameObject.Find ("Aviso").GetComponent<Text> ().enabled = true;
			if (avisando > 1.2) {
				avisando -= Time.deltaTime;
			}
		}
	}

}
	


	



