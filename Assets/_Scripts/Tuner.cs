using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tuner : MonoBehaviour {

	public static string[] notes = {"C", "C#/Db", "D", "D#/Eb", "E", "F", 
									"F#/Gb", "G", "G#/Ab", "A", "A#/Bb", "B"};

	MicrophoneDetector mic;

	public Text octave;
	public Text note;

	// Use this for initialization
	void Start () {
		mic = GetComponent<MicrophoneDetector> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (mic.passThreshold ()) {
			int mid = mic.FrequencyToMidi (mic.GetPrincipalFrecuency ());
			note.text = notes [mid % 12];
			octave.text = (mid / 12).ToString ();
		}
	}
}
