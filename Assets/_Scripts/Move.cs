using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {


	public GameObject target;

	// Use this for initialization
	void Start () {
		Rigidbody r = target.GetComponent<Rigidbody>();
		r.velocity = new Vector3 (-5, 0, 0);
		Destroy (target, 15f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
