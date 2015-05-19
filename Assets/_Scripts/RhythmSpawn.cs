using UnityEngine;
using System.Collections;

public class RhythmSpawn : MonoBehaviour {

	public int BPM = 60;
	public GameObject hit;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("spawn", 5.0f, BPM / 60.0f);
	}
	
	void spawn(){
		Debug.Log ("SPAWN");
		GameObject h = Instantiate (hit, transform.position, Quaternion.identity) as GameObject;
	}
}
