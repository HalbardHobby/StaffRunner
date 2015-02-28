using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour {

	// Update is called once per frame
	void Update () {//por el momento se utiliza toque unico

		if (Input.touchCount == 0)
			return;

		Touch toque = Input.GetTouch (0);

		Vector3 objetivo = Camera.main.ScreenToWorldPoint (toque.position);
		Debug.Log (objetivo);
	}
}
