using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneDetector : MonoBehaviour {

	AudioSource audio;

	// Use this for initialization
	void Start () {
		// Anhade un audio source para almacenar la entrada
		audio = gameObject.AddComponent<AudioSource> () as AudioSource;
		// Inicia la grabacion de audio en el dispositivo por defecto
		audio.clip = Microphone.Start (null, true, 1, AudioSettings.outputSampleRate);

		// Evita reproduccion hasta que haya samples suficientes
		while (!(Microphone.GetPosition(null)>0));

		audio.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	float getAverageVolume(){
		float[] data = new float[1024];
		float amplitude = 0;
		audio.GetOutputData (data, 0);

		foreach (float s in data)
			amplitude += Mathf.Abs (s);

		return amplitude / 1024;
	}

	float GetPrincipalFrecuency(){
		float freq = 0.0f;
		float[] data = new float[8192];
		// se hace fourier para poder encontrar las frecuencias
		audio.GetSpectrumData (data, 0, FFTWindow.BlackmanHarris);

		float max = 0f;
		int i = 0;
		// se busca el armonico mas alto
		for (int j=0; j<data.Length; j++)
			if (data [j] > max){
				i = j;
				max = data[j];
		}

		freq = i * AudioSettings.outputSampleRate / 8192;
		return freq;
	}
}