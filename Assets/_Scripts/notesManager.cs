using UnityEngine;
using System.Collections;

public class notesManager : MonoBehaviour {

	//public PlayerHealth playerHealth;       // Reference to the player's heatlh.
	public GameObject[] targets;                // The enemy prefab to be spawned.
	public float spawnTime = 2f;            // How long between each spawn.
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
