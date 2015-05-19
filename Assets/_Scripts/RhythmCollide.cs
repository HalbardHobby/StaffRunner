using UnityEngine;
using System.Collections;

public class RhythmCollide : MonoBehaviour {

	RhythmScore score;
	public int id;

	// Use this for initialization
	void Start () {
		score = GetComponentInParent<RhythmScore> ();
	}
	
	void OnTriggerEnter2D(Collider2D other){
		score.activate (id, other.gameObject);
	}

	void OnTriggerExit2D(Collider2D other){
		score.deactivate (id);
	}
}
