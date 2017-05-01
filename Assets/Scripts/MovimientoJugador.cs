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
	private bool inmunidad = false;
	public Texture[] texturas;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		carril = 0;
		controller = GetComponent<CharacterController> ();   // componente del personaje
		textometros.text= metros.ToString();
		textomonedas.text = monedas.ToString ();
		gameObject.GetComponent<Renderer>().material.mainTexture = texturas[0];

	}
	
	// Update is called once per frame
	void Update () {
		
		if (estamuerto) {
			anim.Stop ();
			return;
		}

			
		Vertical -= gravedad * Time.deltaTime;
		distancia = transform.position.z;
		donde = GameObject.FindGameObjectWithTag ("Respawn").transform.position.z;
		metros = (int) distancia;
		textometros.text= metros.ToString();
		textomonedas.text= monedas.ToString();
		Velocidad = VelocidadP;

		if (transform.position.y <-5) {
			muerte ();
		}

	

		if ((Input.GetKeyDown (KeyCode.UpArrow)) & (transform.position.y < 2.6)) { // codigo para el salto
			Vertical = 3.5f;
			anim.Play ("Jump", -1, 0f);
			AudioSource.PlayClipAtPoint (sonidosalto, transform.position);

		}

		
	
		/// Y
		moveVector.y = Vertical;
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			anim.Play ("SlideDown", -1, 0f);

		} 
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

		if( Input.GetKeyDown(KeyCode.LeftArrow))   // movimiento entre 3 carriles 
		{if (carril > -1)
			carril --;
			anim.Play ("Move_Iz", -1, 0f);
			AudioSource.PlayClipAtPoint (sonidolados, transform.position);
		}
		if( Input.GetKeyDown(KeyCode.RightArrow))
		{if (carril < 1)
			carril ++;
			anim.Play ("Move_Der", -1, 0f);
			AudioSource.PlayClipAtPoint (sonidolados, transform.position);
		}


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
			gameObject.GetComponent<Renderer>().material.mainTexture = texturas[1];
	}
	}
			
	
		IEnumerator apagarpowerup ()
			{
		yield return new WaitForSeconds(10);
		inmunidad = false;
		gameObject.GetComponent<Renderer>().material.mainTexture = texturas[0];

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


	}
}

///



