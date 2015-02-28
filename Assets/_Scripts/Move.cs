using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Rigidbody2D r = GetComponent<Rigidbody2D>();
		r.velocity = new Vector2 (-5, 0);
	}
}
