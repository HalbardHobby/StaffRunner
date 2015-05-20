using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Playable : MonoBehaviour {

	MicrophoneDetector mic;
	GameObject [] notas;
	Text nota;

	// Use this for initialization
	void Start () {
		mic = GetComponent<MicrophoneDetector> ();
	}
	
	// Update is called once per frame
	void Update () {
		notas = GameObject.FindGameObjectsWithTag("Good");

		int mid = mic.FrequencyToMidi (mic.GetPrincipalFrecuency());
		nota.text = Tuner.notes [mid % 12];

		for (int i=0; i < notas.Length; i++) {
			micNote mn = notas[i].GetComponent<micNote>();
			if (mn.note == mid%12)
				Destroy(notas[i]);
		}
	}
}
