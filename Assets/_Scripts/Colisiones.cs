using UnityEngine;
using System.Collections;

public class Colisiones : MonoBehaviour {


	public Collider player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision){
		Destroy (collision.gameObject);
	}
}
