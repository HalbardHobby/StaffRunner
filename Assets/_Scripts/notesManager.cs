using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NotesManager : MonoBehaviour {
	
	public GameObject[] targets;            // The enemy prefab to be spawned.
	public float spawnTime = 2f;            // How long between each spawn.
	public float chordTime = 5f;

	public HealthManager salud;				//Vida del jugador
	public Text Chord;						//Acorde que se esta generando actualmente
	public Text Scale;						//Escala que se esta utilizando para generar acordes.

	private Transform[] spawnPoints;        // An array of the spawn points this enemy can spawn from.
	private Animator progresion;			// Maquina de estados que colabora en la generacion de progresiones.
	public bool newChord;					// 
		
	void Start (){
		spawnPoints = GetComponentsInChildren<Transform> ();
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
	}

	void Spawn (){
		
		// Find a random index between zero and one less than the number of spawn points.
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		int targetIndex=Random.Range (0, targets.Length);


		// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		Instantiate (targets[targetIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		//TODO asignar la nota
	}
}
