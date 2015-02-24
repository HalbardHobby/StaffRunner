using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float step = 1.0f;
	
	private Transform player;

	// Use this for initialization
	void Start () {
		player = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 pos = player.position;

		if (Input.GetKeyDown ("up"))
			pos.y = transform.position.y + step;
		else if (Input.GetKeyDown ("down"))
			pos.y = transform.position.y - step;

		transform.position = pos;
	}
}
