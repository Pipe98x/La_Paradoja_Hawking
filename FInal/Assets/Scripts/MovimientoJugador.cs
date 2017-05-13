using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovimientoJugador : MonoBehaviour 
{
	private CharacterController controller;
	private float Velocidad = 0;
	private float VelocidadP = 10.0f;
	private float Vertical = 0.0f;
	private float gravedad = 10.0f;
	private Vector3 moveVector;
	private int carril = 0;
	public float distancia = 0;
	private float donde;
	public AudioClip sonidoMoneda = null;
	public AudioClip sonidosalto = null;
	public AudioClip sonidolados = null;
	public AudioClip sonidofondo = null;
	private int metros;
	public Text textometros;
	private int monedas;
	public Text textomonedas;
	public Animator anim;
	public bool estamuerto = false;
	public Menumuerte gameover;
	public Text textometrosfinal;
	public Text textomonedasfinal;
	public Text Record;
	private bool inmunidad = false;
	public Material[] texturas;
	private float grados;

	// para deslizar dedos

	public float MaxtimeSwipe;
	public float MindistanceSwipe;
	private float starttime;
	private float endtime;
	private float SwipeDistance;
	private float SwipeTime;

	private Vector3 Startposicion;
	private Vector3 Endposicion;
	private Renderer Color;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		carril = 0;
		controller = GetComponent<CharacterController> ();   // componente del personaje
		textometros.text= metros.ToString();
		textomonedas.text = monedas.ToString ();
		Color = GetComponent<Renderer> ();
		Color.enabled = true;
		Color.sharedMaterial = texturas [1];



	}
	
	// Update is called once per frame
	void Update () {
		
		if (estamuerto) {
			anim.Stop ();
			return;
		} // si esta muerto no lee todo el codigo restante del Update

		// deteccion de toque con deslizamiento
		if (Input.touchCount > 0) {

			Touch touch = Input.GetTouch (0); // toma el primer toque y lo almacena en touch

			if (touch.phase == TouchPhase.Began) {  // cuando empieza a tcar la pantalla
				starttime = Time.time;				// almacena el momento en que la tocó
				Startposicion = touch.position;		// almacena la posicion en que la tocó


			}else if (touch.phase == TouchPhase.Ended) { // cuando deja de tocar la pantalla
				endtime = Time.time;						
				Endposicion = touch.position;

				SwipeDistance = (Endposicion - Startposicion).magnitude;
				SwipeTime = endtime - starttime;

				if (SwipeTime < MaxtimeSwipe && SwipeDistance > MindistanceSwipe) {
					Swiper ();
				}

			}

		}

		grados = GameObject.FindGameObjectWithTag("MainCamera").transform.rotation.eulerAngles.z; // tomar valor de la rotacion en Z de la camara
		Vertical -= gravedad * Time.deltaTime;
		distancia = transform.position.z;
		donde = GameObject.FindGameObjectWithTag ("Respawn").transform.position.z;
		metros = (int) distancia;
		textometros.text= metros.ToString();
		textomonedas.text= monedas.ToString();
		Velocidad = VelocidadP;

		if (transform.position.y <-5) {   // morir si cae al vacio
			muerte ();
		}
			
	
		/// Y
		moveVector.y = Vertical;
		// transformación "manual" del collider dentro de palyercontroller

		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("SlideDown")) {
			controller.height = 8;
			controller.center = new Vector3 (0, 4, 0);
		} else {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Jump")) {
				controller.height = 12;
				controller.center = new Vector3 (0, 14, 0);
			}else {
				controller.height = 12;
				controller.center = new Vector3 (0, 6, 0);

			}
		}
		



		/// Z
		moveVector.z = Velocidad; // moviemiento constante en el eje z para que simpre vaya hacia el frente
		controller.Move (moveVector * Time.deltaTime);


		/// x

		Vector3 newposition = transform.position;  // posicionar el objeto segun el valor que tenga la variable "carril"
		newposition.x = Mathf.Lerp(newposition.x, 2 * carril, Time.deltaTime * 5);
		transform.position = newposition;


		/// aumento progresivo de la velocidad cada 100 metros
		while (donde < distancia) {
			donde += 200;
		}

		if (donde <= 200)
			VelocidadP = 10;
		if ((donde > 200) & (donde <=400))
			VelocidadP = 11;
		if ((donde > 400) & (donde <=600))
			VelocidadP = 12;
		if ((donde > 600) & (donde <=800))
			VelocidadP = 13;
		if ((donde > 800) & (donde <=1000))
			VelocidadP = 14;
		if ((donde > 1000) & (donde <=1200))
			VelocidadP = 15;
		if ((donde > 1200) & (donde <=1400))
			VelocidadP = 16;
		if ((donde > 1400) & (donde <=1600))
			VelocidadP = 17;
		if ((donde > 1600) & (donde <=1800))
			VelocidadP = 18;
		if ((donde > 1800) & (donde <=2000))
			VelocidadP = 19;

	}

	// si hay Swipe para que lado es

	void Swiper () {
		Vector2 distancia = Endposicion - Startposicion;  // posicon final menos posición inicial

		if (Mathf.Abs (distancia.x) > Mathf.Abs (distancia.y)) {  //valor absoluto para que ambos sean positivos

			if (distancia.x < 0) {
				if (grados < 10) {
					Izquierda ();
				}
				if (grados < 100 && grados > 80) {	
					SlideDown ();
				}
				if (grados < 190 && grados > 170) {	
					Derecha();
				}
				if (grados < 280 && grados > 260) {	
					Salto();
				}
			}else if (distancia.x > 0) {
				if (grados < 10) {
					Derecha ();
				}
				if (grados < 100 && grados > 80) {	
					Salto ();
				}
				if (grados < 190 && grados > 170) {	
					Izquierda();
				}
				if (grados < 280 && grados > 260) {	
					SlideDown();
				}
			}

		} else if (Mathf.Abs (distancia.x) < Mathf.Abs (distancia.y)) {

				if (distancia.y < 0) {
				if (grados < 10) {
					SlideDown ();
				}
				if (grados < 100 && grados > 80) {	
					Derecha ();
				}
				if (grados < 190 && grados > 170) {	
					Salto();
				}
				if (grados < 280 && grados > 260) {	
					Izquierda();
				}
			}else if (distancia.y > 0) {
				if (grados < 10) {
					Salto ();
				}
				if (grados < 100 && grados > 80) {	
					Izquierda ();
				}
				if (grados < 190 && grados > 170) {	
					SlideDown();
				}
				if (grados < 280 && grados > 260) {	
					Derecha();
				}
			}
		}
	}


	void Izquierda () {   
		if (carril > -1) {
			carril--;
			anim.Play ("Move_Iz", -1, 0f);
			AudioSource.PlayClipAtPoint (sonidolados, transform.position);
		}
		}

	void Derecha() { // moverse a la derecha
		if (carril < 1){
			carril ++;
			anim.Play ("Move_Der", -1, 0f);
			AudioSource.PlayClipAtPoint (sonidolados, transform.position);
		}
	}

	void SlideDown () {				// tirar animacion agacharse
			anim.Play ("SlideDown", -1, 0f);

		}
	void Salto() {
		if (transform.position.y < 1.9) { // codigo para el salto
			Vertical = 3.5f;
			anim.Play ("Jump", -1, 0f);
			AudioSource.PlayClipAtPoint (sonidosalto, transform.position);
		}
	
	}





	void OnTriggerEnter (Collider obj){
		if (obj.gameObject.tag == "coin"){
			obj.gameObject.SetActive (false);
			monedas += 1;
			AudioSource.PlayClipAtPoint (sonidoMoneda, transform.position);

		}
		if (obj.gameObject.tag == "Inmunidad") {
			obj.gameObject.SetActive (false);
			inmunidad= true;

			StartCoroutine (apagarpowerup ());

	}
	}
			
	
		IEnumerator apagarpowerup ()
			{
		yield return new WaitForSeconds(10);
		inmunidad = false;


				}
			
		

	void OnControllerColliderHit(ControllerColliderHit hit){
		if (inmunidad == false) {
		if (hit.gameObject.tag == "Obstaculo"){ 
			muerte();
	}
	}
	}

	void muerte() {
		estamuerto = true;
		gameover.toggleEndmenu (0.0f);
		textometrosfinal.text= metros.ToString();
		textomonedasfinal.text= monedas.ToString();
		if (PlayerPrefs.GetInt ("Top 1") < metros) {
			PlayerPrefs.SetInt ("Top 1", metros);
		}
		Record.text = (PlayerPrefs.GetInt ("Top 1").ToString ());


	}
}

///



