using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CSharpSynth.Effects;
using CSharpSynth.Sequencer;
using CSharpSynth.Synthesis;
using CSharpSynth.Midi;

[RequireComponent (typeof(AudioSource))]
public class NotesManager : MonoBehaviour {

	public static float DoDistance = 6f;			// La distancia a la linea correspondiente a "Do" en clave de sol.
	public static int maxLine=13;					// La maxima cantidad de lineas permitidas en el pentagrama.

	public GameObject good;							// Prefab correspondiente a una nota correcta.
	public GameObject bad;							// Prefab correspondiente a una mala nota.

	public float spawnTime = 2f;            		// How long between each spawn.
	public float chordTime = 5f;					// Cada cuanto se cambia de acorde.
	public float badNoteProb = 0.20f;				// La probabilidad de una nota fuera del acorde.
	public float upperNoteProb = 0.30f;				// La probabilidad de una nota en la siguiente octava.

	public HealthManager salud;						// Vida del jugador
	public Text Chord;								// Acorde que se esta generando actualmente
	public Text Scale;								// Escala que se esta utilizando para generar acordes.
	
	private StreamSynthesizer midiStreamSynthesizer;// Permite sintetizar el MIDI.
	private float[] sampleBuffer;
	private float gain = 1f;
	public string bankFilePath = "GM Bank/gm";
	public int bufferSize = 1024;

	private Animator progresion;					// Maquina de estados que colabora en la generacion de progresiones.
	private int[] notesInChord; 					// Arreglo con las notas que pertenecen al acorde.
			
	void Start (){


		progresion = GetComponent<Animator> ();

		Scale.text="CM";
		//TODO seleccion y generacion arbitraria de escalas

		// Llama a las funciones de Spawn y Cambio de acorde cada tiempo.
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
		InvokeRepeating ("ChangeChord", 0, chordTime);
	}

	void InicilizarSintetizador(){
		midiStreamSynthesizer = new StreamSynthesizer (44100, 2, bufferSize, 40);
		sampleBuffer = new float[midiStreamSynthesizer.BufferSize];		
		
		midiStreamSynthesizer.LoadBank (bankFilePath);
	}

	void ChangeChord(){
		// Se retira en estado actual dde la maquina.
		AnimatorStateInfo actual = progresion.GetCurrentAnimatorStateInfo (0);

		int top = 3;
		if (actual.IsName("I"))
			top = 8;
		float t = Random.Range (0f, 1f);

		int tr;
		if(t>0.8f)
			tr = Random.Range (-1, 0);
		else
			tr = Random.Range (1, top);

		// Activa la transicion.
		progresion.SetInteger ("ChordSel", tr);
		progresion.SetTrigger ("Transition");

		AnimatorStateInfo anim = progresion.GetCurrentAnimatorStateInfo (0);
		int tone = 0;
		for (tone=0; tone<ChordNames.MayorScale.Length; tone++)
			if(anim.IsName(ChordNames.MayorScale[tone]))
				break;

		Chord.text = ChordNames.MayorScale [tone];

		if (ChordNames.MayorScale [tone].ToLower ().Equals (ChordNames.MayorScale [tone]))
			notesInChord = ChordNames.mayor;
		else
			notesInChord = ChordNames.minor;

		for(int i=0;i<notesInChord.Length;i++)
			notesInChord[i]+=i;
	}

	void Spawn (){
		
		// Find a random index between zero and one less than the number of spawn points.
		float prob = Random.Range (0f, 1f);

		int line;
		GameObject target;
		if (prob < badNoteProb) {
			line = (int)Random.Range (0, maxLine);
			target = bad;
		} else {
			line = notesInChord[Random.Range(0,notesInChord.Length-1)];
			target = good;
		}

		Vector3 pos = transform.position;
		pos.y = line - DoDistance;

		prob = Random.Range (0f, 1f);
		if (prob < upperNoteProb) 
			pos.y += 7f;

		GameObject note = Instantiate (target, pos, Quaternion.identity) as GameObject;
		//TODO asignar la nota
	}
}
