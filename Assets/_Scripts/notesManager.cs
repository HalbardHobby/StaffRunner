﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NotesManager : MonoBehaviour {
	
	public GameObject[] targets;                // The enemy prefab to be spawned.
	public float spawnTime = 2f;             // How long between each spawn.

	public HealthManager salud;				//Vida del jugador
	public Text Chord;						//Acorde que se esta generando actualmente
	public Text Scale;						//Escala que se esta utilizando para generar acordes.

	private Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
		
	void Start (){
		spawnPoints = GetComponentsInChildren<Transform> ();

		// Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}

	void Update(){
		//TODO construir logica de generacion
	}
	
	void Spawn (){
		// If the player has no health left...
		/*if(playerHealth.currentHealth <= 0f)
		{
			// ... exit the function.
			return;
		}*/
		
		// Find a random index between zero and one less than the number of spawn points.
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		int targetIndex=Random.Range (0, targets.Length);


		// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		Instantiate (targets[targetIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
	}
}
