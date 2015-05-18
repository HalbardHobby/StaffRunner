using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneDetector : MonoBehaviour {

	AudioSource audio;

	public float threshold = 0.15f;
	public int sampleSize = 8192;
	public int sampleRate = 20000;

	// Use this for initialization
	void Start () {
		// Anhade un audio source para almacenar la entrada
		audio = gameObject.GetComponent<AudioSource>();
		// Inicia la grabacion de audio en el dispositivo por defecto
		audio.clip = Microphone.Start (null, true, 1, sampleRate);
		audio.loop = true;
		// Evita reproduccion hasta que haya samples suficientes
		while (!(Microphone.GetPosition(null)>0));

		Debug.Log (sampleRate);
		audio.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (getAverageVolume() >= threshold)
			Debug.Log (GetPrincipalFrecuency());
	}

	float getAverageVolume(){
		float[] data = new float[sampleSize];
		float amplitude = 0;
		audio.GetOutputData (data, 0);

		foreach (float s in data)
			amplitude += Mathf.Abs (s);

		return amplitude / sampleSize;
	}

	float GetPrincipalFrecuency(){
		float freq = 0.0f;
		float[] data = new float[sampleSize];
		// se hace fourier para poder encontrar las frecuencias
		audio.GetSpectrumData (data, 0, FFTWindow.BlackmanHarris);

		float max = 0f;
		int i = 0;
		// se busca el armonico mas alto
		for (int j=0; j<data.Length/2; j++)
			if (data [j] > max){
				i = j;
				max = data[j];
		}

		freq = i * sampleRate / ((float)sampleSize*(sampleRate* 0.0000843f));
		return freq;
	}
}