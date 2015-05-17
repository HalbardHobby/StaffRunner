using UnityEngine;
using System.Collections;

//Este objeto se encargara de detectar los objetos salidos del campo de juego y procesarlos debidamente
public class Cleaner : MonoBehaviour {

	private BoxCollider2D collide;

	public HealthManager health;

	// Use this for initialization
	void Awake() {
		collide = GetComponent<BoxCollider2D> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log (other.gameObject.tag);
		if (other.gameObject.CompareTag ("Good")) {
			Debug.Log("Danho");
			health.Damage();
		}
		Destroy (other.gameObject);
	}
}
