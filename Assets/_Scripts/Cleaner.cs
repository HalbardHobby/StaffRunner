using UnityEngine;
using System.Collections;

//Este objeto se encargara de detectar los objetos salidos del campo de juego y procesarlos debidamente
public class Cleaner : MonoBehaviour {

	public HealthManager health;

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Good")) {
			health.Damage();
		}
		Destroy (other.gameObject);
	}
}
