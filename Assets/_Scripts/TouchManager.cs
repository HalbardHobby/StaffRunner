using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour {

	private HealthManager salud;
	private ScoreManager puntaje;

	void Start(){
		salud = GetComponent<HealthManager>();
		puntaje = GetComponent<ScoreManager>();
	}

	// Update is called once per frame
	void Update () {//por el momento se utiliza toque unico

		if (Input.touchCount == 0)
			return;
		
		Touch toque = Input.GetTouch (0);
		
		Vector3 objetivo = Camera.main.ScreenToWorldPoint (toque.position);
		RaycastHit2D hit = Physics2D.Raycast (objetivo, objetivo);

		if(hit.collider != null)
			//TODO comportamiento de audio
			//TODO comportamiento de puntaje y salud
			Destroy (hit.collider.gameObject);
	}
}
