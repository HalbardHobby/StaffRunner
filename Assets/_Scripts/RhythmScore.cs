using UnityEngine;
using System.Collections;

public class RhythmScore : MonoBehaviour {

	ShakeDetector shake;
	BoxCollider2D [] colliders;


	// Use this for initialization
	void Start () {
		shake = GetComponent<ShakeDetector> ();

		colliders = GetComponentsInChildren<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
