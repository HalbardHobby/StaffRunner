using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NotesManager : MonoBehaviour {

	public static float DoDistance = 6f;
	public static int maxLine=13;

	public GameObject good;
	public GameObject bad;

	public float spawnTime = 2f;            // How long between each spawn.
	public float chordTime = 5f;
	public float badNoteProb = 0.20f;
	public float upperNoteProb = 0.30f;

	public HealthManager salud;				//Vida del jugador
	public Text Chord;						//Acorde que se esta generando actualmente
	public Text Scale;						//Escala que se esta utilizando para generar acordes.
	
	private Animator progresion;			// Maquina de estados que colabora en la generacion de progresiones.
	private int[] notesInChord; 
		
	void Start (){
		progresion = GetComponent<Animator> ();

		Scale.text="CM";//TODO seleccion y generacion arbitraria de escalas

		// Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
		InvokeRepeating ("ChangeChord", 0, chordTime);

	}

	void Update(){
		//TODO construir logica de generacion
	}

	void ChangeChord(){
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

		Instantiate (target, pos, Quaternion.identity);
		//TODO asignar la nota
	}
}
