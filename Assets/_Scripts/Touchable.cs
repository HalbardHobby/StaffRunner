using UnityEngine;
using System.Collections;

public class Touchable : MonoBehaviour {
	
	void OnTriggerEnter2D( Collider2D other){
		other.gameObject.layer = 0;
	}
}
