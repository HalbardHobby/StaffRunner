using UnityEngine;
using System.Collections;
using CSharpSynth.Effects;
using CSharpSynth.Sequencer;
using CSharpSynth.Synthesis;
using CSharpSynth.Midi;

[RequireComponent (typeof(AudioSource))]
public class TouchManager : MonoBehaviour {

	private HealthManager salud;
	private ScoreManager puntaje;

	private StreamSynthesizer midiStreamSynthesizer;// Permite sintetizar el MIDI.
	private float[] sampleBuffer;
	private float gain = 1f;
	public string bankFilePath = "GM Bank/gm";
	public int bufferSize = 1024;

	void Awake(){
		salud = GetComponent<HealthManager>();
		puntaje = GetComponent<ScoreManager>();
		InicilizarSintetizador ();
	}

	void InicilizarSintetizador(){
		midiStreamSynthesizer = new StreamSynthesizer (44100, 2, bufferSize, 40);
		sampleBuffer = new float[midiStreamSynthesizer.BufferSize];		
		
		midiStreamSynthesizer.LoadBank (bankFilePath);
	}

	// Update is called once per frame
	void Update () {//por el momento se utiliza toque unico

		if (Input.touchCount == 0)
			return;

		if (Input.touchCount == 2)
		{
			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);
			
			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
			
			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
			
			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			if (deltaMagnitudeDiff <=0){

				// HeroPower
				salud.HeroPowerActivate();

			}
		
		}

		Touch toque = Input.GetTouch (0);
		
		Vector3 objetivo = Camera.main.ScreenToWorldPoint (toque.position);
		RaycastHit2D hit = Physics2D.Raycast (objetivo, objetivo);

		if (hit.collider != null) {
			//TODO comportamiento de audio
			GameObject obj = hit.collider.gameObject;
			if(obj.tag.Equals("Good")){
				puntaje.IncreaseScore();
				salud.Recover();
			}
			else if(obj.tag.Equals("Bad")){
				puntaje.LoseStreak();
				salud.Damage();
			}
			Note nota = obj.GetComponent<Note>();

			if(nota != null){
				int note = nota.note;
				int vol = nota.volume;
				int inst = nota.instrument;
				float duration = nota.duration;

				StartCoroutine(PlayNote(note, vol, inst, duration));

				Destroy (obj);
			}

		}
	}

	IEnumerator PlayNote(int note, int vol, int inst, float duration){
		midiStreamSynthesizer.NoteOn (1, note, vol, inst);
		yield return new WaitForSeconds (duration);
		midiStreamSynthesizer.NoteOff (1, note);
	}

	// Metodo necesario para salida de audio.
	// NO RETIRAR!!!
	private void OnAudioFilterRead (float[] data, int channels)
	{
		
		//This uses the Unity specific float method we added to get the buffer
		midiStreamSynthesizer.GetNext (sampleBuffer);
		
		for (int i = 0; i < data.Length; i++) {
			data [i] = sampleBuffer [i] * gain;
		}
	}

}
