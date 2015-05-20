using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneDetector : MonoBehaviour {

	public static float SAMPLE_NORM = 0.0000843f;
	public static float EXPONENTIAL = 1.059463283f;
	public static float BASE_FREQUENCY = 16.35f;

	AudioSource audio;

	public float threshold = 0.15f;
	public int sampleSize = 8192;
	public int sampleRate = 48000;

	// Use this for initialization
	void Start () {
		// Anhade un audio source para almacenar la entrada
		audio = gameObject.GetComponent<AudioSource>();
		// Inicia la grabacion de audio en el dispositivo por defecto
		audio.clip = Microphone.Start (null, true, 1, sampleRate);
		audio.loop = true;
		// Evita reproduccion hasta que haya samples suficientes
		while (!(Microphone.GetPosition(null)>0));

		Debug.Log (AudioSettings.outputSampleRate);
		audio.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (getAverageVolume() >= threshold)
			Debug.Log (FrequencyToMidi(GetPrincipalFrecuency())%12);
	}

	float getAverageVolume(){
		float[] data = new float[sampleSize];
		float amplitude = 0;
		audio.GetOutputData (data, 0);

		foreach (float s in data)
			amplitude += Mathf.Abs (s);

		return amplitude / sampleSize;
	}

	//funcion que calcula la frecuencia predominante
	float GetPrincipalFrecuency(){
		float freq = 0.0f;
		float[] data = new float[sampleSize];
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

		freq = i * sampleRate / ((float)sampleSize*(sampleRate* SAMPLE_NORM));
		return freq;
	}

	//funcion que retorna una frecuencia dado un valor midi
	float MidiToFrequency(int midi){
		return Mathf.Pow (EXPONENTIAL, midi) * BASE_FREQUENCY;
	}
	
	//funcion que retorna un numeral midi dada una frecuencia
	int FrequencyToMidi(float freq){
		float temp = Mathf.Log ( freq/BASE_FREQUENCY,EXPONENTIAL );
		float fl_dist = Mathf.Abs (Mathf.Floor (temp) - temp);
		float cl_dist = Mathf.Abs (Mathf.Ceil (temp) - temp);
		
		return (fl_dist < cl_dist) ? Mathf.FloorToInt (temp) : Mathf.CeilToInt (temp);
	}
}