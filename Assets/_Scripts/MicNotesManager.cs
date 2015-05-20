using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class MicNotesManager : MonoBehaviour {

	public static float DoDistance = 6f;			// La distancia a la linea correspondiente a "Do" en clave de sol.

	public GameObject note;							// Prefab correspondiente a una nota correcta.

	public float spawnTime = 1f;            		// How long between each spawn.
	public float chordTime = 5f;					// Cada cuanto se cambia de acorde..

	public HealthManager salud;						// Vida del jugador
	public Text Chord;								// Acorde que se esta generando actualmente
	public Text Scale;								// Escala que se esta utilizando para generar acordes.

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
			notesInChord = ChordNames.minor;
		else
			notesInChord = ChordNames.mayor;

		for(int i=0;i<notesInChord.Length;i++)
			notesInChord[i] = (notesInChord[i]+ChordNames.Mayor[tone])%12;
	}

	void Spawn (){
		int line;
		line = notesInChord[Random.Range(0, notesInChord.Length-1)]%12;
		Debug.Log (line);
		Vector3 pos = transform.position;
		pos.y = (line) - DoDistance;

		GameObject nt = Instantiate (note, pos, Quaternion.identity) as GameObject;
		micNote mc = nt.GetComponent<micNote> ();

		mc.note = ChordNames.Mayor [line % ChordNames.Mayor.Length];
		mc.nota.text = Tuner.notes [mc.note%12];
		Debug.Log (line);
	}
}
